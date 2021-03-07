using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Model.Migrations
{
    public partial class addedBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "OrderItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_BookId",
                table: "OrderItem",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Book_BookId",
                table: "OrderItem",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Book_BookId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_BookId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "OrderItem");
        }
    }
}
