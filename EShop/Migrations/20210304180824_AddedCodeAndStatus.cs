using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Model.Migrations
{
    public partial class AddedCodeAndStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "NotLessThenZero",
                table: "Book");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Customer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "VerificationCode",
                table: "Customer",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddCheckConstraint(
                name: "NotLessThenZero",
                table: "Book",
                sql: "[Supplies] >= 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "NotLessThenZero",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "Customer");

            migrationBuilder.AddCheckConstraint(
                name: "NotLessThenZero",
                table: "Book",
                sql: "[Supplies]>=0");
        }
    }
}
