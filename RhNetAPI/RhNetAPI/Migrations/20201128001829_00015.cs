using Microsoft.EntityFrameworkCore.Migrations;

namespace RhNetAPI.Migrations
{
    public partial class _00015 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atos_Normativos_Tipos_de_Ato_Normativo_Tipo_de_Ato_Normativo_Id",
                table: "Atos_Normativos");

            migrationBuilder.AlterColumn<int>(
                name: "Tipo_de_Ato_Normativo_Id",
                table: "Atos_Normativos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Atos_Normativos_Tipos_de_Ato_Normativo_Tipo_de_Ato_Normativo_Id",
                table: "Atos_Normativos",
                column: "Tipo_de_Ato_Normativo_Id",
                principalTable: "Tipos_de_Ato_Normativo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atos_Normativos_Tipos_de_Ato_Normativo_Tipo_de_Ato_Normativo_Id",
                table: "Atos_Normativos");

            migrationBuilder.AlterColumn<int>(
                name: "Tipo_de_Ato_Normativo_Id",
                table: "Atos_Normativos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Atos_Normativos_Tipos_de_Ato_Normativo_Tipo_de_Ato_Normativo_Id",
                table: "Atos_Normativos",
                column: "Tipo_de_Ato_Normativo_Id",
                principalTable: "Tipos_de_Ato_Normativo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
