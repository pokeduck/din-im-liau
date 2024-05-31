using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class CreatePermission3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Permission_Account",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "Account",
                table: "Account",
                newName: "PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Account_Account",
                table: "Account",
                newName: "IX_Account_PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Permission_PermissionId",
                table: "Account",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Permission_PermissionId",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "Account",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_Account_PermissionId",
                table: "Account",
                newName: "IX_Account_Account");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Permission_Account",
                table: "Account",
                column: "Account",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
