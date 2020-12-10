namespace RhNetServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m00003 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "LockoutEndDateUtc", newName: "LockoutEnd");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "LockoutEnd", newName: "LockoutEndDateUtc");
        }
    }
}
