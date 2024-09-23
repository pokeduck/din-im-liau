using Common.Extensions;
using Common.Helper;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class AdminDefaultPwd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var now = DateTime.UtcNow.ToTimestamp();

            var adminSalt = SaltHelper.GenerateN();
            var user01Salt = SaltHelper.GenerateN();
            var adminPwd = HashHelper.Argon2Id("Admin", adminSalt);
            var user01Pwd = HashHelper.Argon2Id("user01", user01Salt);

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Id", "Nickname", "Email", "ThumbnailUrl", "GoogleOpenId", "AccessToken", "Salt", "HashPassword", "UpdateTime", "CreateTime", "PermissionId" },
                values: new object[]
                {
                    1, "Admin", "Admin@bd.tw", "https://google.com", "", null, adminSalt,adminPwd, now, now, 1
                });


            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Id", "Nickname", "Email", "ThumbnailUrl", "GoogleOpenId", "AccessToken", "Salt", "HashPassword", "UpdateTime", "CreateTime", "PermissionId" },
                values: new object[]
                {
                    2, "user01", "User01@bd.tw", "https://google.com", "", null,user01Salt,user01Pwd, now, now, 2
                }
                );





        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
