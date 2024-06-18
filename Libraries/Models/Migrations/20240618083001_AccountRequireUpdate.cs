using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class AccountRequireUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccessToken",
                table: "Account",
                type: "LONGTEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "LONGTEXT"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "Account",
                type: "LONGTEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "LONGTEXT"
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                            name: "AccessToken",
                            table: "Account",
                            type: "LONGTEXT",
                            nullable: false,
                            defaultValue: "",
                            oldClrType: typeof(string),
                            oldType: "LONGTEXT",
                            oldNullable: true
            );
            migrationBuilder.AlterColumn<string>(
                            name: "Salt",
                            table: "Account",
                            type: "LONGTEXT",
                            nullable: false,
                            defaultValue: "",
                            oldClrType: typeof(string),
                            oldType: "LONGTEXT",
                            oldNullable: true
                        );

        }
    }
}
