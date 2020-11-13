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
                             select new RoleModel()
                             {
                                 Name = x.Name,
                                 Description = x.Description,
                                 id = x.Id
                             }).ToList();

            return userRoles;
        }

        public async Task<List<RoleModel>> GetAllRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
           
            var userRoles =await (from x in roleManager.Roles
                             select new RoleModel()
                             {
                                 Name = x.Name,
                                 Description = x.Description,
                                 id = x.Id
                             }).ToListAsync();

            return userRoles;
        }
    }
}
