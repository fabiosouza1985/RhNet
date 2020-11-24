﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RhNetAPI.Entities.Adm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetAPI.Contexts
{
    public class RhNetContext : IdentityDbContext
        <ApplicationUser, 
        ApplicationRole, 
        string, 
        ApplicationUserClaim, 
        ApplicationUserRole, 
        IdentityUserLogin<string>, 
        IdentityRoleClaim<string>, 
        IdentityUserToken<string>>
    {
       public DbSet<ApplicationMenu> ApplicationMenus { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<UserClient> UserClients { get; set; }
        public RhNetContext(DbContextOptions <RhNetContext> options): base(options) { }
    }
}
