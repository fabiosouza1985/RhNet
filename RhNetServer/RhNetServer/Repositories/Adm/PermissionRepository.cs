using Microsoft.AspNet.Identity;
using System.Data.Entity;
using RhNetServer.Contexts;
using RhNetServer.Entities.Adm;
using RhNetServer.Models.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RhNetServer.Repositories.Adm
{
    public class PermissionRepository
    {
        public async Task<List<PermissionModel>> GetAllPermissions(RhNetContext context)
        {
           
            return await (from x in context.Permissions
                          orderby x.Table, x.Description
                          select new PermissionModel()
                          {
                              Description = x.Description,
                              Id = x.Id,
                              Table = x.Table
                          }).ToListAsync();

        }

        public async Task<List<PermissionModel>> GetPermissions(RhNetContext context, UserManager<ApplicationUser> userManager, string userName)
        {
            if (userName == "master")
            {
                return await GetAllPermissions(context);
            }
            ApplicationUser user = await userManager.FindByNameAsync(userName);
            List<Claim> claims = (await userManager.GetClaimsAsync(user.Id)).ToList();
            List<string> permissions = claims.Where(e => e.Type == "permission").Select(e => e.Value).ToList();

            return await (from x in context.Permissions
                          where permissions.Contains(x.Description)
                          orderby x.Table, x.Description
                          select new PermissionModel()
                          {
                              Description = x.Description,
                              Id = x.Id,
                              Table = x.Table
                          }).ToListAsync();

        }

        public async Task<PermissionModel> AddPermission(PermissionModel permissionModel, RhNetContext context)
        {

            Permission permission = new Permission()
            {
                Description = permissionModel.Description,
                Table = permissionModel.Table
            };

            context.Entry(permission).State = EntityState.Added;
            await context.SaveChangesAsync();
            await context.Entry(permission).ReloadAsync();

            permissionModel.Id = permission.Id;

            return permissionModel;

        }

        public async Task<PermissionModel> UpdatePermission(PermissionModel permissionModel, RhNetContext context)
        {
            Permission permission = await context.Permissions.FindAsync(permissionModel.Id);

            permission.Description = permissionModel.Description;
            permission.Table = permissionModel.Table;

            context.Entry(permission).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return permissionModel;

        }

        public async Task<PermissionModel> RemovePermission(PermissionModel permissionModel, RhNetContext context)
        {
            Permission permission = await context.Permissions.FindAsync(permissionModel.Id);

            context.Entry(permission).State = EntityState.Deleted;
            await context.SaveChangesAsync();

            return permissionModel;

        }
    }
}
