using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class CreatePermission4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Permission_PermissionId",
                table: "Account");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Permission_PermissionId",
                table: "Account",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Permission_PermissionId",
                table: "Account");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Permission_PermissionId",
                table: "Account",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
