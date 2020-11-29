using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RhNetAPI.Migrations
{
    public partial class _00013 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipos_de_Ato_Normativo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_de_Ato_Normativo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Atos_Normativos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(maxLength: 500, nullable: false),
                    Vigencia_Data = table.Column<DateTime>(nullable: false),
                    Publicacao_Data = table.Column<DateTime>(nullable: true),
                    Tipo_de_Ato_Normativo_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atos_Normativos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atos_Normativos_Tipos_de_Ato_Normativo_Tipo_de_Ato_Normativo_Id",
                        column: x => x.Tipo_de_Ato_Normativo_Id,
                        principalTable: "Tipos_de_Ato_Normativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atos_Normativos_Tipo_de_Ato_Normativo_Id",
                table: "Atos_Normativos",
                column: "Tipo_de_Ato_Normativo_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atos_Normativos");

            migrationBuilder.DropTable(
                name: "Tipos_de_Ato_Normativo");
        }
    }
}
