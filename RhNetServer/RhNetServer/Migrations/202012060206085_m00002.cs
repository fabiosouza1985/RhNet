namespace RhNetServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m00002 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUserClaims", "ClientId", c => c.Int());
            AddColumn("dbo.AspNetUserRoles", "ClientId", c => c.Int());
            CreateIndex("dbo.AspNetUserClaims", "ClientId");
            CreateIndex("dbo.AspNetUserRoles", "ClientId");
            AddForeignKey("dbo.AspNetUserClaims", "ClientId", "dbo.Clients", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "ClientId", "dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.AspNetUserClaims", "ClientId", "dbo.Clients");
            DropIndex("dbo.AspNetUserRoles", new[] { "ClientId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "ClientId" });
            DropColumn("dbo.AspNetUserRoles", "ClientId");
            DropColumn("dbo.AspNetUserClaims", "ClientId");
        }
    }
}
