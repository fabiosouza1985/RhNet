using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RhNetAPI.Entities.Adm;
using RhNetAPI.Entities.Shared;
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

        public DbSet<Municipio> Municipios { get; set; }

        public DbSet<Entidade> Entidades { get; set; }
        public DbSet<EntidadeSubordinacao> EntidadesSubordinacoes { get; set; }

        public DbSet<Tipo_de_Ato_Normativo> Tipos_de_Ato_Normativo { get; set; }

        public DbSet<Ato_Normativo> Atos_Normativos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EntidadeSubordinacao>()
                .HasKey(c => new { c.Entidade_Superior_Id, c.Entidade_Inferior_Id, c.Vigencia_Inicio });
            
            base.OnModelCreating(builder);
        }
    }
}
