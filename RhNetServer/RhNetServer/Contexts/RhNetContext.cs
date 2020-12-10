using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using RhNetServer.Entities.Adm;
using RhNetServer.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhNetServer.Contexts
{  
    public class RhNetContext : IdentityDbContext
        <ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
         public DbSet<ApplicationUserRole> UserRoles { get; set; }

        public DbSet<ApplicationUserClaim> UserClaims { get; set; }
        public DbSet<ApplicationMenu> ApplicationMenus { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<UserClient> UserClients { get; set; }
                
        public DbSet<Municipio> Municipios { get; set; }

        public DbSet<Entidade> Entidades { get; set; }
        public DbSet<EntidadeSubordinacao> EntidadesSubordinacoes { get; set; }

        public DbSet<Tipo_de_Ato_Normativo> Tipos_de_Ato_Normativo { get; set; }

        public DbSet<Ato_Normativo> Atos_Normativos { get; set; }

        public DbSet<Quadro> Quadros { get; set; }

        public DbSet<Subquadro> Subquadros { get; set; }
              
        public RhNetContext() : base("DefaultConnection") { }

        public static RhNetContext Create()
        {
            return new RhNetContext();
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EntidadeSubordinacao>()
                .HasKey(c => new { c.Entidade_Superior_Id, c.Entidade_Inferior_Id, c.Vigencia_Inicio });

          
            builder.Entity<ApplicationUserRole>()
               .HasKey(c => new { c.RoleId, c.UserId, c.ClientId });
                      
           
        }
    }
}
