using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<RoleModel>> GetAllRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
           
            var userRoles =await (from x in roleManager.Roles
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
