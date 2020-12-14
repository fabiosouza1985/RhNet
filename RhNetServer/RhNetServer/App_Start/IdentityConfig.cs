using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using RhNetServer.Contexts;
using RhNetServer.Entities.Adm;
using RhNetServer.Models.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Security.Claims;

namespace RhNetServer.App_Start
{
   
    public class ApplicationUserManager: UserManager<ApplicationUser, string>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, string> userStore) : base(userStore) { }


        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context.Get<RhNetContext>()));

            manager.UserValidator = new UserValidator<ApplicationUser>(manager) 
            {
            AllowOnlyAlphanumericUserNames = false,
            RequireUniqueEmail = false
          };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            return manager;

        }

        public  async Task<List<RoleModel>> GetRoleByClientAsync(string userName, string clientCnpj)
        {
            var rhnetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if (userName == "master")
            {
                return await (from y in rhnetContext.Roles
                              select new RoleModel()
                              {
                                  Id = y.Id,
                                  Description = y.Description,
                                  Level = y.Level,
                                  Name = y.Name
                              }).ToListAsync();
            }
            var user = await userManager.FindByNameAsync(userName);

            Client client = await rhnetContext.Clients.Where(e => e.Cnpj == clientCnpj).FirstOrDefaultAsync();

            if (client == null)
            {
                return null;
            }
            List<RoleModel> roleModels = await (from x in rhnetContext.UserRoles
                                                from y in rhnetContext.Roles.Where(e => e.Id == x.RoleId)
                                                where x.UserId == user.Id && x.ClientId == client.Id
                                                select new RoleModel()
                                                {
                                                    Id = y.Id,
                                                    Description = y.Description,
                                                    Level = y.Level,
                                                    Name = y.Name
                                                }).ToListAsync();

            return roleModels;

        }

        public async Task<List<ApplicationUserModel.UserRoleModel>> GetAllRolesAsync(string userName)
        {
            var rhnetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var user = await userManager.FindByNameAsync(userName);
           
            List<ApplicationUserModel.UserRoleModel> applicationUserRoles = await (from x in rhnetContext.UserRoles
                                                from y in rhnetContext.Roles.Where(e => e.Id == x.RoleId)
                                                where x.UserId == user.Id
                                                select new ApplicationUserModel.UserRoleModel()
                                                {
                                                    ClientId = x.ClientId,
                                                    RoleId = x.RoleId,
                                                    UserId = x.UserId
                                                }).ToListAsync();

            return applicationUserRoles;

        }

        public async Task<List<Claim>> GetClaimsAsync( string username, string clientCnpj)
        {
            var rhnetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            if (username == "master")
            {
                ApplicationUser user = await FindByNameAsync(username);

                List<Claim> claims = new List<Claim>();

                var userClaims = await (from x in rhnetContext.Permissions
                                        select  x.Description
                                     ).ToListAsync();

                for (var i = 0; i < userClaims.Count; i++)
                {
                    claims.Add(new Claim("permission", userClaims.ElementAt(i)));
                }
                return claims;
            }
            else
            {
                ApplicationUser user = await FindByNameAsync(username);

                List<Claim> claims = new List<Claim>();

                var userClaims = await (from x in rhnetContext.UserClaims
                                        from c in rhnetContext.Clients.Where(e => e.Id == x.ClientId)
                                        where c.Cnpj == clientCnpj && x.UserId == user.Id
                                        select new { x.ClaimType, x.ClaimValue }
                                     ).ToListAsync();


                for (var i = 0; i < userClaims.Count; i++)
                {
                    claims.Add(new Claim(userClaims.ElementAt(i).ClaimType, userClaims.ElementAt(i).ClaimValue));
                }
                return claims;
            }

        }

        public async Task<List<ApplicationUserModel.UserPermissionModel>> GetAllClaimsAsync(string username, string type = "")
        {
            var rhnetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();
            ApplicationUser user = await FindByNameAsync(username);

            List<ApplicationUserModel.UserPermissionModel> claims = new List<ApplicationUserModel.UserPermissionModel>();

            var userClaims = await (from x in rhnetContext.UserClaims
                                    from c in rhnetContext.Clients.Where(e => e.Id == x.ClientId)
                                    where x.UserId == user.Id
                                    select new ApplicationUserModel.UserPermissionModel
                                    { 
                                        ClientId = x.ClientId,
                                        UserId = x.UserId,
                                        Description = x.ClaimValue
                                    }
                                 ).ToListAsync();


            for (var i = 0; i < userClaims.Count; i++)
            {
                if (type == "" || type == userClaims.ElementAt(i).Description)
                {
                    claims.Add(userClaims.ElementAt(i));
                }

            }
            return claims;

        }
        public async Task<List<ClientModel>> GetClientsAsync( string username)
        {
            var rhnetContext = HttpContext.Current.GetOwinContext().GetUserManager<RhNetContext>();

            if (username == "master")
            {
                return await (from y in rhnetContext.Clients
                              select new ClientModel()
                              {
                                  Id = y.Id,
                                  Cnpj = y.Cnpj,
                                  Description = y.Description,
                                  Situation = y.Situation
                              }).ToListAsync();
            }
            var user = await FindByNameAsync(username);           
            List<ClientModel> clients = await (from x in rhnetContext.UserClients
                                               from y in rhnetContext.Clients.Where(e => e.Id == x.ClientId)
                                               where x.UserId == user.Id
                                               select new ClientModel()
                                               {
                                                   Id = y.Id,
                                                   Cnpj = y.Cnpj,
                                                   Description = y.Description,
                                                   Situation = y.Situation
                                               }).ToListAsync();

            return clients;
        }

        public async Task CreateUser(ApplicationUser applicationUser, string password)
        {
            

            await this.CreateAsync(applicationUser, password);
        }
    }

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            ///It is based on the same context as the ApplicationUserManager
            var appRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole, string, ApplicationUserRole>(context.Get<RhNetContext>()));

            return appRoleManager;
        }
    }
}