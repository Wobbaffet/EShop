using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Model.Migrations
{
    public partial class RemovedAppartmentNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppartmentNumber",
                table: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppartmentNumber",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
