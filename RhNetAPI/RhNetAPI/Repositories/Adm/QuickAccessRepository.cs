using RhNetAPI.Models.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RhNetAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RhNetAPI.Entities.Adm;

namespace RhNetAPI.Repositories.Adm
{
    public class QuickAccessRepository
    {

        public async Task<List<QuickAccessModel>> GetAllQuickAccess(RhNetContext context)
        {
         
            return await (from x in context.QuickAccess
                          select new QuickAccessModel()
                          {
                              Role_Name = x.Role_Name,
                              Header = x.Header,
                              Id = x.Id,
                              Path = x.Path,
                              Permission_Name = x.Permission_Name
                          }).ToListAsync();
            ;

        }
        public async Task<List<QuickAccessModel>> GetQuickAccess(string username, string profile, RhNetContext context,  UserManager<ApplicationUser> userManager)
        {
            List<QuickAccessModel> quickAccess = new List<QuickAccessModel>();

            if (username == "master")
            {          

                quickAccess = await (from x in context.QuickAccess
                               where x.Role_Name == profile
                               select new QuickAccessModel()
                               {
                                   Role_Name = x.Role_Name,
                                   Header = x.Header,
                                   Id = x.Id,
                                   Path = x.Path,
                                   Permission_Name = x.Permission_Name
                               }).ToListAsync();
            }
            else
            {
                var user = await userManager.FindByNameAsync(username);
                var claims = (await userManager.GetClaimsAsync(user)).Where(e => e.Type == "Permission").Select(e => e.Value).ToList();

                quickAccess = await (from x in context.QuickAccess
                               where x.Role_Name == profile && claims.Contains( x.Permission_Name )
                               select new QuickAccessModel()
                               {
                                   Role_Name = x.Role_Name,
                                   Header = x.Header,
                                   Id = x.Id,
                                   Path = x.Path,
                                   Permission_Name = x.Permission_Name
                               }).ToListAsync();

            }

            return quickAccess;

        }

        public async Task<QuickAccessModel> AddQuickAccess(QuickAccessModel quickAccessModel, RhNetContext context)
        {

            QuickAccess quickAccess = new QuickAccess()
            {
                Header = quickAccessModel.Header,
                Path = quickAccessModel.Path,
                Permission_Name = quickAccessModel.Permission_Name,
                Role_Name = quickAccessModel.Role_Name
            };

            context.Entry(quickAccess).State = EntityState.Added;
            await context.SaveChangesAsync();
            await context.Entry(quickAccess).ReloadAsync();

            quickAccessModel.Id = quickAccess.Id;

            return quickAccessModel;
            
        }

         public async Task<QuickAccessModel> UpdateQuickAccess(QuickAccessModel quickAccessModel, RhNetContext context)
        {
            QuickAccess quickAccess = await context.QuickAccess.FindAsync(quickAccessModel.Id);

             quickAccess.Header = quickAccessModel.Header;
             quickAccess.Path = quickAccessModel.Path;
             quickAccess.Permission_Name = quickAccessModel.Permission_Name;
             quickAccess.Role_Name = quickAccessModel.Role_Name;
           

            context.Entry(quickAccess).State = EntityState.Modified;
            await context.SaveChangesAsync();
           
            return quickAccessModel;
            
        }

        public async Task<QuickAccessModel> RemoveQuickAccess(QuickAccessModel quickAccessModel, RhNetContext context)
        {
            QuickAccess quickAccess = await context.QuickAccess.FindAsync(quickAccessModel.Id);

            context.Entry(quickAccess).State = EntityState.Deleted;
            await context.SaveChangesAsync();
           
            return quickAccessModel;
            
        }
    }
}
