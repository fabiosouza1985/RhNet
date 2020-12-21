namespace RhNetServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m00002 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quadros_Atos_Normativos",
                c => new
                    {
                        Quadro_Id = c.Int(nullable: false),
                        Ato_Normativo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Quadro_Id, t.Ato_Normativo_Id })
                .ForeignKey("dbo.Atos_Normativos", t => t.Ato_Normativo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Quadros", t => t.Quadro_Id, cascadeDelete: true)
                .Index(t => t.Quadro_Id)
                .Index(t => t.Ato_Normativo_Id);
            
            DropColumn("dbo.Quadros", "Sigla");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quadros", "Sigla", c => c.String(nullable: false, maxLength: 10));
            DropForeignKey("dbo.Quadros_Atos_Normativos", "Quadro_Id", "dbo.Quadros");
            DropForeignKey("dbo.Quadros_Atos_Normativos", "Ato_Normativo_Id", "dbo.Atos_Normativos");
            DropIndex("dbo.Quadros_Atos_Normativos", new[] { "Ato_Normativo_Id" });
            DropIndex("dbo.Quadros_Atos_Normativos", new[] { "Quadro_Id" });
            DropTable("dbo.Quadros_Atos_Normativos");
        }
    }
}
