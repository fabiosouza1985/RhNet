namespace RhNetServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m00001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false, maxLength: 100),
                        Path = c.String(nullable: false, maxLength: 100),
                        Role_Name = c.String(nullable: false, maxLength: 256),
                        Permission_Name = c.String(nullable: false, maxLength: 100),
                        Quick_Access = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Atos_Normativos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 500),
                        Numero = c.Int(nullable: false),
                        Ano = c.Int(nullable: false),
                        Vigencia_Data = c.DateTime(nullable: false),
                        Publicacao_Data = c.DateTime(),
                        Tipo_de_Ato_Normativo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tipos_de_Ato_Normativo", t => t.Tipo_de_Ato_Normativo_Id, cascadeDelete: true)
                .Index(t => t.Tipo_de_Ato_Normativo_Id);
            
            CreateTable(
                "dbo.Tipos_de_Ato_Normativo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cnpj = c.String(nullable: false, maxLength: 14, fixedLength: true, unicode: false),
                        Description = c.String(nullable: false, maxLength: 100),
                        Situation = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Entidades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 300),
                        Municipio_Id = c.Int(),
                        Codigo_Audesp = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Municipios", t => t.Municipio_Id)
                .Index(t => t.Municipio_Id);
            
            CreateTable(
                "dbo.Municipios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 300),
                        Codigo_Audesp = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EntidadesSubordinacoes",
                c => new
                    {
                        Entidade_Superior_Id = c.Int(nullable: false),
                        Entidade_Inferior_Id = c.Int(nullable: false),
                        Vigencia_Inicio = c.DateTime(nullable: false),
                        Vigencia_Termino = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.Entidade_Superior_Id, t.Entidade_Inferior_Id, t.Vigencia_Inicio })
                .ForeignKey("dbo.Entidades", t => t.Entidade_Inferior_Id, cascadeDelete: true)
                .ForeignKey("dbo.Entidades", t => t.Entidade_Superior_Id, cascadeDelete: false)
                .Index(t => t.Entidade_Superior_Id)
                .Index(t => t.Entidade_Inferior_Id);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        MenuId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationMenus", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Cpf = c.String(maxLength: 11, fixedLength: true, unicode: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Table = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quadros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 100),
                        Sigla = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 200),
                        Level = c.Int(),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Subquadros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 100),
                        Sigla = c.String(nullable: false, maxLength: 10),
                        Quadro_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quadros", t => t.Quadro_Id, cascadeDelete: true)
                .Index(t => t.Quadro_Id);
            
            CreateTable(
                "dbo.UserClients",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClientId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserClients", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.UserClients", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subquadros", "Quadro_Id", "dbo.Quadros");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Favorites", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Favorites", "MenuId", "dbo.ApplicationMenus");
            DropForeignKey("dbo.EntidadesSubordinacoes", "Entidade_Superior_Id", "dbo.Entidades");
            DropForeignKey("dbo.EntidadesSubordinacoes", "Entidade_Inferior_Id", "dbo.Entidades");
            DropForeignKey("dbo.Entidades", "Municipio_Id", "dbo.Municipios");
            DropForeignKey("dbo.Atos_Normativos", "Tipo_de_Ato_Normativo_Id", "dbo.Tipos_de_Ato_Normativo");
            DropIndex("dbo.UserClients", new[] { "ClientId" });
            DropIndex("dbo.UserClients", new[] { "UserId" });
            DropIndex("dbo.Subquadros", new[] { "Quadro_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Favorites", new[] { "MenuId" });
            DropIndex("dbo.Favorites", new[] { "UserId" });
            DropIndex("dbo.EntidadesSubordinacoes", new[] { "Entidade_Inferior_Id" });
            DropIndex("dbo.EntidadesSubordinacoes", new[] { "Entidade_Superior_Id" });
            DropIndex("dbo.Entidades", new[] { "Municipio_Id" });
            DropIndex("dbo.Atos_Normativos", new[] { "Tipo_de_Ato_Normativo_Id" });
            DropTable("dbo.UserClients");
            DropTable("dbo.Subquadros");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Quadros");
            DropTable("dbo.Permissions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Favorites");
            DropTable("dbo.EntidadesSubordinacoes");
            DropTable("dbo.Municipios");
            DropTable("dbo.Entidades");
            DropTable("dbo.Clients");
            DropTable("dbo.Tipos_de_Ato_Normativo");
            DropTable("dbo.Atos_Normativos");
            DropTable("dbo.ApplicationMenus");
        }
    }
}
