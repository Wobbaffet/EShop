using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Model.Migrations
{
    public partial class AddedConstraintOnBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "NotLessThenZero",
                table: "Book",
                sql: "[Supplies]>=0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "NotLessThenZero",
                table: "Book");
        }
    }
}
