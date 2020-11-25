using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RhNetAPI.Migrations
{
    public partial class _00012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntidadesSubordinacoes",
                columns: table => new
                {
                    Entidade_Superior_Id = table.Column<int>(nullable: false),
                    Entidade_Inferior_Id = table.Column<int>(nullable: false),
                    Vigencia_Inicio = table.Column<DateTime>(nullable: false),
                    Vigencia_Termino = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadesSubordinacoes", x => new { x.Entidade_Superior_Id, x.Entidade_Inferior_Id, x.Vigencia_Inicio });
                    table.ForeignKey(
                        name: "FK_EntidadesSubordinacoes_Entidades_Entidade_Inferior_Id",
                        column: x => x.Entidade_Inferior_Id,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntidadesSubordinacoes_Entidades_Entidade_Superior_Id",
                        column: x => x.Entidade_Superior_Id,
                        principalTable: "Entidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntidadesSubordinacoes_Entidade_Inferior_Id",
                table: "EntidadesSubordinacoes",
                column: "Entidade_Inferior_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntidadesSubordinacoes");
        }
    }
}
