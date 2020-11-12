﻿using RhNetAPI.Models.Adm;
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
    public class MenuRepository
    {

        public async Task<List<MenuModel>> GetAllMenus(RhNetContext context)
        {
         
            return await (from x in context.ApplicationMenus
                          select new MenuModel()
                          {
                              Role_Name = x.Role_Name,
                              Header = x.Header,
                              Id = x.Id,
                              Path = x.Path,
                              Permission_Name = x.Permission_Name
                          }).ToListAsync();
            ;

        }
        public async Task<List<MenuModel>> GetMenus(string username, string profile, RhNetContext context,  UserManager<ApplicationUser> userManager)
        {
            List<MenuModel> menus = new List<MenuModel>();

            if (username == "master")
            {          

                menus = await (from x in context.ApplicationMenus
                               where x.Role_Name == profile
                               select new MenuModel()
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

                menus = await (from x in context.ApplicationMenus
                               where x.Role_Name == profile && claims.Contains( x.Permission_Name )
                               select new MenuModel()
                               {
                                   Role_Name = x.Role_Name,
                                   Header = x.Header,
                                   Id = x.Id,
                                   Path = x.Path,
                                   Permission_Name = x.Permission_Name
                               }).ToListAsync();

            }

            return menus;

        }

        public async Task<MenuModel> AddMenu(MenuModel menuModel, RhNetContext context)
        {

            ApplicationMenu menu = new ApplicationMenu()
            {
                Header = menuModel.Header,
                Path = menuModel.Path,
                Permission_Name = menuModel.Permission_Name,
                Role_Name = menuModel.Role_Name
            };

            context.Entry(menu).State = EntityState.Added;
            await context.SaveChangesAsync();
            await context.Entry(menu).ReloadAsync();

            menuModel.Id = menu.Id;

            return menuModel;
            
        }

         public async Task<MenuModel> UpdateMenu(MenuModel menuModel, RhNetContext context)
        {
            ApplicationMenu menu = await context.ApplicationMenus.FindAsync(menuModel.Id);

             menu.Header = menuModel.Header;
             menu.Path = menuModel.Path;
             menu.Permission_Name = menuModel.Permission_Name;
             menu.Role_Name = menuModel.Role_Name;
           

            context.Entry(menu).State = EntityState.Modified;
            await context.SaveChangesAsync();
           
            return menuModel;
            
        }

        public async Task<MenuModel> RemoveMenu(MenuModel menuModel, RhNetContext context)
        {
            ApplicationMenu menu = await context.ApplicationMenus.FindAsync(menuModel.Id);

            context.Entry(menu).State = EntityState.Deleted;
            await context.SaveChangesAsync();
           
            return menuModel;
            
        }
    }
}
