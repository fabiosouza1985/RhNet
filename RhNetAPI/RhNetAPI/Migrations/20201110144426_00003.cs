﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace RhNetAPI.Migrations
{
    public partial class _00003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationMenus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(maxLength: 100, nullable: false),
                    Path = table.Column<string>(maxLength: 100, nullable: false),
                    Role_Name = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Permission_Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuickAccess",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(maxLength: 100, nullable: false),
                    Path = table.Column<string>(maxLength: 100, nullable: false),
                    Role_Name = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Permission_Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuickAccess", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationMenus");

            migrationBuilder.DropTable(
                name: "QuickAccess");
        }
    }
}
