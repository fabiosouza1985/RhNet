using Microsoft.AspNetCore.Identity;
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
                                 Description = x.Description
                             }).ToList();

            return userRoles;
        }
    }
}
