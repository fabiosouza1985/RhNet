using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RhNetAPI.Contexts;
using RhNetAPI.Entities.Adm;
using RhNetAPI.Models.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Repositories.Adm
{
    public class UserRepository
    {
        public async Task<List<ApplicationUserModel>> GetUsers(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, RhNetContext rhNetContext, string username)
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
                               from y in rhNetContext.UserClients.Where(e => client_ids.Contains(e.ClientId))
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
        public async Task<List<RoleModel>> GetRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, string username)
        {
            ApplicationUser user = await userManager.FindByNameAsync(username);
            List<String> roles = new List<String>();                      
           
            roles = (await userManager.GetRolesAsync(user)).ToList();

            var userRoles = (from x in roleManager.Roles
                             where roles.Contains(x.Name)
                             orderby x.Level
                             select new RoleModel()
                             {
                                 Name = x.Name,
                                 Description = x.Description,
                                 id = x.Id,
                                 Level = x.Level
                             }).ToList();

            return userRoles;
        }

        public async Task<List<RoleModel>> GetAllRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, string username)
        {
           if(username == "master")
            {
                var userRoles = await (from x in roleManager.Roles
                                       orderby x.Level
                                       select new RoleModel()
                                       {
                                           Name = x.Name,
                                           Description = x.Description,
                                           id = x.Id,
                                           Level = x.Level
                                       }).ToListAsync();

                return userRoles;
            }
            else
            {
                ApplicationUser user = await userManager.FindByNameAsync(username);
                List<String> roles = new List<String>();

                roles = (await userManager.GetRolesAsync(user)).ToList();

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
                                           id = x.Id,
                                           Level = x.Level
                                       }).ToListAsync();

                return userRoles;
            }
           
        }

        public async Task<Object> AddUserAsync(UserManager<ApplicationUser> userManager, ApplicationUserModel applicationUserModel)
        {
            
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = applicationUserModel.UserName,
                Email = applicationUserModel.Email,
                Cpf = applicationUserModel.Cpf
            };

            List<Client> clients = applicationUserModel.clients;
            List<ApplicationRole> roles = applicationUserModel.applicationRoles;
            List<Permission> permissions = applicationUserModel.permissions;


          var result =   await userManager.CreateAsync(applicationUser);

            if (result.Succeeded)
            {
                return applicationUserModel;
            }
            else
            {
                var errorList = result.Errors.ToList();
                string errors = "";

                for(var i = 0; i < errorList.Count; i++)
                {
                    IdentityError erro = errorList.ElementAt(i);
                    if(erro.Code == "DuplicateUserName")
                    {
                        errors += "Usuário já existe";
                    }
                    else
                    {
                        errors += erro.Description + " ";
                    }
                    
                }
                return errors;
            }


           
        }
        public async Task<RoleModel> AddRoleAsync(RoleManager<ApplicationRole> roleManager, RoleModel role)
        {
            ApplicationRole newRole = new ApplicationRole() { 
                Description = role.Description,
                Level = role.Level,
                Name = role.Name
            };

            await roleManager.CreateAsync(newRole);
            newRole = await roleManager.FindByNameAsync(role.Name);
            role.id = newRole.Id;
            return role;
        }

        public async Task<RoleModel> UpdateRoleAsync(RoleManager<ApplicationRole> roleManager, RoleModel role)
        {
            ApplicationRole applicationRole =await roleManager.FindByIdAsync(role.id);

            applicationRole.Name = role.Name;
            applicationRole.Description = role.Description;
            applicationRole.Level = role.Level;
            

             await roleManager.UpdateAsync(applicationRole);
            return role;
        }
    }
}
