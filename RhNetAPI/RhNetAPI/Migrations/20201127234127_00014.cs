using Microsoft.EntityFrameworkCore.Migrations;

namespace RhNetAPI.Migrations
{
    public partial class _00014 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "Atos_Normativos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Atos_Normativos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ano",
                table: "Atos_Normativos");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Atos_Normativos");
        }
    }
}
