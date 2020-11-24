using Microsoft.EntityFrameworkCore.Migrations;

namespace RhNetAPI.Migrations
{
    public partial class _00010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "AspNetUserClaims",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_ClientId",
                table: "AspNetUserRoles",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_ClientId",
                table: "AspNetUserClaims",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Clients_ClientId",
                table: "AspNetUserClaims",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Clients_ClientId",
                table: "AspNetUserRoles",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_Clients_ClientId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Clients_ClientId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_ClientId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_ClientId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "AspNetUserClaims");
        }
    }
}
