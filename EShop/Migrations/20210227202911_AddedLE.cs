using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Model.Migrations
{
    public partial class AddedLE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "AppartmentNumber", "CityName", "PTT", "StreetName", "StreetNumber" },
                values: new object[] { 1, null, null, 123, null, null });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "AddressId", "Email", "Password", "PhoneNumber" },
                values: new object[] { 1, 1, "asdfgg", "draga peder", "123" });

            migrationBuilder.InsertData(
                table: "Legal entity",
                columns: new[] { "CustomerId", "CompanyName", "CompanyNumber", "TIN" },
                values: new object[] { 1, "asd", "1234", 123 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Legal entity",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: 1);
        }
    }
}
