using Microsoft.EntityFrameworkCore;
using RhNetAPI.Contexts;
using RhNetAPI.Entities.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Repositories.Adm
{
    public class FavoriteRepository
    {
        public async Task<Boolean> IsFavorite(RhNetContext context, string userName, string path)
        {
            return await (from x in context.Favorites
                                from y in context.ApplicationMenus.Where(e => e.Id == x.MenuId)
                                from z in context.Users.Where(e => e.Id == x.UserId)
                                where y.Path == path && z.UserName == userName
                                select x.Id).CountAsync() > 0;
        }

        public async Task<String> AddFavorite(RhNetContext context, string userName, string path)
        {
            string userId = await (from x in context.Users
                                   where x.UserName == userName
                                   select x.Id).FirstOrDefaultAsync();
           
            int menuId = await (from x in context.ApplicationMenus
                                where x.Path == path
                                select x.Id).FirstOrDefaultAsync();

            if(menuId == 0)
            {
                return path;
            }
            Favorite favorite = new Favorite();
            favorite.UserId = userId;
            favorite.MenuId = menuId;
            context.Entry(favorite).State = EntityState.Added;
            await context.SaveChangesAsync();

            return "teste";
        }

    }
}
