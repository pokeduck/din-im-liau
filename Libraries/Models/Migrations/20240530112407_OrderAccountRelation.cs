using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class OrderAccountRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_AccountId",
                table: "Order",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Account_AccountId",
                table: "Order",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Account_AccountId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_AccountId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Order");
        }
    }
}
