using Microsoft.AspNet.Identity;
using RhNetServer.Contexts;
using RhNetServer.Entities.Adm;
using RhNetServer.Models.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Data.Entity;
using RhNetServer.App_Start;

namespace RhNetServer.Repositories.Adm
{
    public class UserRepository
    {
        public async Task<List<ApplicationUserModel>> GetUsers(ApplicationUserManager userManager, RhNetContext rhNetContext, string username)
        {
            ApplicationUser user = await userManager.FindByNameAsync(username);

            
            List<ApplicationUserModel> users = new List<ApplicationUserModel>();

            if (user.UserName == "master")
            {
                users = await (from x in userManager.Users
                               select new ApplicationUserModel()
                               {
                                   Cpf = x.Cpf,
                                   Email = x.Email,
                                   UserId = x.Id,
                                   UserName = x.UserName
                               }).ToListAsync();
            }
            else
            {
                List<int> client_ids = await (from x in rhNetContext.UserClients
                                              where x.UserId == user.Id
                                              select x.ClientId).ToListAsync();

                users = await (from x in userManager.Users
                               where (from y in rhNetContext.UserClients where client_ids.Contains( y.ClientId) select y.UserId).Contains(x.Id)
                               select new ApplicationUserModel()
                               {
                                   Cpf = x.Cpf,
                                   Email = x.Email,
                                   UserId = x.Id,
                                   UserName = x.UserName
                               }).ToListAsync();
            }

            return users;
        }


        public async Task<ApplicationUserModel> GetUser(ApplicationUserManager userManager, RhNetContext rhNetContext, string userId)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userId);


            List<ApplicationUserModel> users = new List<ApplicationUserModel>();

            ApplicationUserModel applicationUserModel = await (from x in rhNetContext.Users
                                                               where x.Id == userId
                                                               select new ApplicationUserModel()
                                                               {
                                                                   Cpf = x.Cpf,
                                                                   Email = x.Email,
                                                                   UserId = x.Id,
                                                                   UserName = x.UserName
                                                               }).FirstOrDefaultAsync();

            if(applicationUserModel == null)
            {
                return null;
            }

            applicationUserModel.clients = await (from x in rhNetContext.UserClients
                                                  from y in rhNetContext.Clients.Where(e => e.Id == x.ClientId)
                                                  where x.UserId == applicationUserModel.UserId
                                                  select new ClientModel()
                                                  {
                                                      Id = y.Id,
                                                      Cnpj = y.Cnpj,
                                                      Description = y.Description,
                                                      Situation = y.Situation
                                                  }).ToListAsync();

            applicationUserModel.applicationRoles = await (from x in rhNetContext.UserRoles
                                                           where x.UserId == applicationUserModel.UserId
                                                           select new ApplicationUserModel.UserRoleModel()
                                                           {
                                                               UserId = x.UserId,
                                                               ClientId = x.ClientId,
                                                               RoleId = x.RoleId
                                                           }).ToListAsync();

            applicationUserModel.permissions = await (from x in rhNetContext.UserClaims
                                                           where x.UserId == applicationUserModel.UserId &&
                                                           x.ClaimType == "permission"
                                                           select new ApplicationUserModel.UserPermissionModel()
                                                           {
                                                               UserId = x.UserId,
                                                               ClientId = x.ClientId,
                                                               Description = x.ClaimValue
                                                           }).ToListAsync();

            return applicationUserModel;
        }

        public async Task<List<ClientModel>> GetClientsAsync(RhNetContext rhNetContext, string userId)
        {
            List<ClientModel> clients = await (from x in rhNetContext.UserClients
                                               from y in rhNetContext.Clients.Where(e=> e.Id == x.ClientId)
                                               where x.UserId == userId
                                               select new ClientModel()
                                               {
                                                   Id = y.Id,
                                                   Cnpj = y.Cnpj,
                                                   Description = y.Description,
                                                   Situation = y.Situation
                                               }).ToListAsync();

            return clients;
        }
        public async Task<List<RoleModel>> GetRolesAsync(ApplicationUserManager userManager, RhNetContext rhNetContext, string username, int clientId)
        {
            if (username == "master")
            {
                var userRoles = await (from x in rhNetContext.Roles
                                       orderby x.Level
                                       select new RoleModel()
                                       {
                                           Name = x.Name,
                                           Description = x.Description,
                                           Id = x.Id,
                                           Level = x.Level
                                       }).ToListAsync();

                return userRoles;
            }
            else
            {
                ApplicationUser user = await userManager.FindByNameAsync(username);
                var userRoles = await (from x in rhNetContext.UserRoles
                                       from y in rhNetContext.Roles.Where(e => e.Id == x.RoleId)
                                       where x.ClientId == clientId && x.UserId == user.Id
                                       orderby y.Level
                                       select new RoleModel()
                                       {
                                           Name = y.Name,
                                           Description = y.Description,
                                           Id = y.Id,
                                           Level = y.Level
                                       }).ToListAsync();

                return userRoles;
            }
            
        }

        public async Task<List<Claim>> GetClaimsAsync(ApplicationUserManager userManager, RhNetContext rhNetContext, string username, int clientId)
        {
            if(username == "master")
            {
                ApplicationUser user = await userManager.FindByNameAsync(username);

                List<Claim> userClaims = new List<Claim>();


                var _claims = await (from x in rhNetContext.Permissions                                        
                                        select  x.Description
                                     ).ToListAsync();

                for(var i = 0; i < _claims.Count; i++)
                {
                    userClaims.Add(new Claim("permission", _claims.ElementAt(i)));
                }
                return userClaims;
            }
            else
            {
                ApplicationUser user = await userManager.FindByNameAsync(username);

                List<Claim> userClaims = new List<Claim>();

                var _claims = await (from x in rhNetContext.UserClaims
                                        where x.ClientId == clientId && x.UserId == user.Id
                                        select  x.ClaimValue
                                     ).ToListAsync();

                for (var i = 0; i < _claims.Count; i++)
                {
                    userClaims.Add(new Claim("permission", _claims.ElementAt(i)));
                }

                return userClaims;
            }
            
        }

        public async Task<List<RoleModel>> GetAllRolesAsync(ApplicationUserManager userManager, ApplicationRoleManager roleManager, string username)
        {
           if(username == "master")
            {
                var userRoles = await (from x in roleManager.Roles
                                       orderby x.Level
                                       select new RoleModel()
                                       {
                                           Name = x.Name,
                                           Description = x.Description,
                                           Id = x.Id,
                                           Level = x.Level
                                       }).ToListAsync();

                return userRoles;
            }
            else
            {
                ApplicationUser user = await userManager.FindByNameAsync(username);
                List<String> roles = new List<String>();

                roles = (await userManager.GetRolesAsync(user.Id)).ToList();

                int? MinLevel = await (from x in roleManager.Roles
                                      where roles.Contains(x.Name)
                                      select x.Level).MinAsync();

                var userRoles = await (from x in roleManager.Roles
                                       where x.Level >= MinLevel
                                       orderby x.Level
                                       select new RoleModel()
                                       {
                                           Name = x.Name,
                                           Description = x.Description,
                                           Id = x.Id,
                                           Level = x.Level
                                       }).ToListAsync();

                return userRoles;
            }
           
        }

        public async Task<object> AddUserAsync(ApplicationUserManager userManager, RhNetContext rhNetContext, ApplicationUserModel applicationUserModel)
        {
            
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = applicationUserModel.UserName,
                Email = applicationUserModel.Email,
                Cpf = applicationUserModel.Cpf
            };

            List<ClientModel> clients = applicationUserModel.clients;
            List<ApplicationUserModel.UserRoleModel> roles = applicationUserModel.applicationRoles;
            List<ApplicationUserModel.UserPermissionModel> permissions = applicationUserModel.permissions;


          var result =   await userManager.CreateAsync(applicationUser, "Mm123456*");

            if (result.Succeeded)
            {
                applicationUser = await userManager.FindByNameAsync(applicationUser.UserName);

                for (var i = 0; i < roles.Count() ; i++)
                {
                    ApplicationUserRole applicationUserRole = new ApplicationUserRole()
                    {
                        ClientId = roles[i].ClientId,
                        UserId = applicationUser.Id,
                        RoleId = roles[i].RoleId
                    };
                    rhNetContext.Entry(applicationUserRole).State = EntityState.Added;
                  

                }

                for (var i = 0; i < permissions.Count(); i++)
                {
                    ApplicationUserClaim applicationUserClaim = new ApplicationUserClaim()
                    {
                        ClientId = permissions[i].ClientId,
                        UserId = applicationUser.Id,
                        ClaimType = "permission",
                        ClaimValue = permissions[i].Description
                    };
                    rhNetContext.Entry(applicationUserClaim).State = EntityState.Added;
                    

                }

                for (var i = 0; i < clients.Count(); i++)
                {
                    UserClient userClient = new UserClient()
                    {
                        ApplicationUser = applicationUser,
                        ClientId = clients[i].Id
                    };
                    rhNetContext.Entry(userClient).State = EntityState.Added;

                }

                await rhNetContext.SaveChangesAsync();
                return applicationUserModel;
            } else
            {
                var errorList = result.Errors.ToList();
                string errors = "";

               
                for(var i = 0; i < errorList.Count; i++)
                {
                    errors += errorList.ElementAt(i) + " ";

                }
                return errors;
            }


           
        }

        public async Task<object> UpdateUserAsync(ApplicationUserManager userManager, RhNetContext rhNetContext, ApplicationUserModel applicationUserModel)
        {

            ApplicationUser applicationUser =await userManager.FindByNameAsync(applicationUserModel.UserName);

            if (applicationUser == null)
            {
                return "Usuário não encontrado.";
            }

            applicationUser.Email = applicationUserModel.Email;
            applicationUser.Cpf = applicationUserModel.Cpf;

            List<ClientModel> clients = applicationUserModel.clients;
            List<ApplicationUserModel.UserRoleModel> roles = applicationUserModel.applicationRoles;
            List<ApplicationUserModel.UserPermissionModel> permissions = applicationUserModel.permissions;


            var result = await userManager.UpdateAsync(applicationUser);

            if (result.Succeeded)
            {

                var current_roles = await userManager.GetAllRolesAsync(applicationUserModel.UserName);
                var current_permissions = await userManager.GetAllClaimsAsync(applicationUserModel.UserName, "permission");
                var current_clients = await userManager.GetClientsAsync(applicationUserModel.UserName);

                for (var i = 0; i < current_roles.Count(); i++)
                {
                    if(roles.Where(e => e.ClientId == current_roles.ElementAt(i).ClientId && e.RoleId == current_roles.ElementAt(i).RoleId).Count() == 0)
                    {
                        ApplicationUserRole applicationUserRole = await (from x in rhNetContext.UserRoles
                                                                         where x.ClientId == current_roles.ElementAt(i).ClientId &&
                                                                         x.RoleId == current_roles.ElementAt(i).RoleId &&
                                                                         x.UserId == applicationUserModel.UserId
                                                                         select x).FirstOrDefaultAsync();

                        if(applicationUserRole != null)
                        {
                            rhNetContext.Entry(applicationUserRole).State = EntityState.Deleted;
                        }
                    }
                }

                for (var i = 0; i < current_permissions.Count(); i++)
                {
                    if (permissions.Where(e => e.ClientId == current_permissions.ElementAt(i).ClientId).Count() == 0)
                    {
                        ApplicationUserClaim applicationUserClaim = await (from x in rhNetContext.UserClaims
                                                                         where x.ClientId == current_permissions.ElementAt(i).ClientId &&
                                                                         x.ClaimType == current_permissions.ElementAt(i).ClaimType &&
                                                                         x.UserId == applicationUserModel.UserId
                                                                         select x).FirstOrDefaultAsync();

                        if (applicationUserClaim != null)
                        {
                            rhNetContext.Entry(applicationUserClaim).State = EntityState.Deleted;
                        }
                    }
                }

                for (var i = 0; i < current_clients.Count(); i++)
                {
                    if (clients.Where(e => e.Id == current_clients.ElementAt(i).Id).Count() == 0)
                    {
                        UserClient userClient = await (from x in rhNetContext.UserClients
                                                                         where x.ClientId == current_clients.ElementAt(i).Id &&
                                                                         x.UserId == applicationUserModel.UserId
                                                                         select x).FirstOrDefaultAsync();

                        if (userClient != null)
                        {
                            rhNetContext.Entry(userClient).State = EntityState.Deleted;
                        }
                    }
                }


                for (var i = 0; i < roles.Count(); i++)
                {
                    if(current_roles.Where(e => e.ClientId == roles.ElementAt(i).ClientId && e.RoleId == roles.ElementAt(i).RoleId).Count() == 0)
                    {
                        ApplicationUserRole applicationUserRole = new ApplicationUserRole()
                        {
                            ClientId = roles[i].ClientId,
                            UserId = applicationUser.Id,
                            RoleId = roles[i].RoleId
                        };
                        rhNetContext.Entry(applicationUserRole).State = EntityState.Added;
                    }
                }

                for (var i = 0; i < permissions.Count(); i++)
                {
                    ApplicationUserClaim applicationUserClaim = new ApplicationUserClaim()
                    {
                        ClientId = permissions[i].ClientId,
                        UserId = applicationUser.Id,
                        ClaimType = "permission",
                        ClaimValue = permissions[i].Description
                    };
                    rhNetContext.Entry(applicationUserClaim).State = EntityState.Added;


                }

                for (var i = 0; i < clients.Count(); i++)
                {
                    UserClient userClient = new UserClient()
                    {
                        ApplicationUser = applicationUser,
                        ClientId = clients[i].Id
                    };
                    rhNetContext.Entry(userClient).State = EntityState.Added;

                }

                await rhNetContext.SaveChangesAsync();
                return applicationUserModel;
            }
            else
            {
                var errorList = result.Errors.ToList();
                string errors = "";


                for (var i = 0; i < errorList.Count; i++)
                {
                    errors += errorList.ElementAt(i) + " ";

                }
                return errors;
            }



        }
        public async Task<RoleModel> AddRoleAsync(ApplicationRoleManager roleManager, RoleModel role)
        {
            ApplicationRole newRole = new ApplicationRole() { 
                Description = role.Description,
                Level = role.Level,
                Name = role.Name
            };

            await roleManager.CreateAsync(newRole);
            newRole = await roleManager.FindByNameAsync(role.Name);
            role.Id = newRole.Id;
            return role;
        }

        public async Task<RoleModel> UpdateRoleAsync(RoleManager<ApplicationRole> roleManager, RoleModel role)
        {
            ApplicationRole applicationRole =await roleManager.FindByIdAsync(role.Id);

            applicationRole.Name = role.Name;
            applicationRole.Description = role.Description;
            applicationRole.Level = role.Level;
            

             await roleManager.UpdateAsync(applicationRole);
            return role;
        }
    }
}
