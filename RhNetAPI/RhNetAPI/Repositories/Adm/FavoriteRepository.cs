﻿using Microsoft.AspNetCore.Mvc;
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
    public class FavoriteRepository
    {
        public async Task<List<FavoriteModel>> GetFavorites(RhNetContext context, string userName, string profile)
        {
            return await (from x in context.Favorites
                          from y in context.ApplicationMenus.Where(e => e.Id == x.MenuId)
                          from z in context.Users.Where(e => e.Id == x.UserId)
                          where y.Role_Name == profile && z.UserName == userName
                          select new FavoriteModel { 
                              Header = y.Header,
                              Path = y.Path
                          }).ToListAsync() ;
        }
        public async Task<Boolean> IsFavorite(RhNetContext context, string userName, string path)
        {
            return await (from x in context.Favorites
                                from y in context.ApplicationMenus.Where(e => e.Id == x.MenuId)
                                from z in context.Users.Where(e => e.Id == x.UserId)
                                where y.Path == path && z.UserName == userName
                                select x.Id).CountAsync() > 0;
        }

        public async Task<Object> AddFavorite(RhNetContext context, string userName, FavoriteModel favoriteModel)
        {
            string userId = await (from x in context.Users
                                   where x.UserName == userName
                                   select x.Id).FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(userId))
            {
                return "Usuário não encontrado";
            }
            ApplicationMenu menu = await (from x in context.ApplicationMenus
                                where x.Path == favoriteModel.Path
                                select x).FirstOrDefaultAsync();

            if (menu == null)
            {
                return "Menu não encontrado";
            }

            Favorite favorite = new Favorite();
            favorite.UserId = userId;
            favorite.MenuId = menu.Id;
            context.Entry(favorite).State = EntityState.Added;
            await context.SaveChangesAsync();

            favoriteModel.Header = menu.Header;
            return favoriteModel;
        }

        public async Task<Object> RemoveFavorite(RhNetContext context, string userName, FavoriteModel favoriteModel)
        {
            Favorite favorite = await (from x in context.Favorites
                                       from y in context.Users.Where(e => e.UserName == userName)
                                       from z in context.ApplicationMenus.Where(e => e.Path == favoriteModel.Path)
                                       select x).FirstOrDefaultAsync();

            if(favorite == null)
            {
                return "Favorito não encontrado";
            }


          
            context.Entry(favorite).State = EntityState.Deleted;
            await context.SaveChangesAsync();

            return favoriteModel;
        }

    }
}
