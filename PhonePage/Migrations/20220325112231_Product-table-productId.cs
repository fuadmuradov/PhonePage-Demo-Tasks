using Microsoft.EntityFrameworkCore.Migrations;

namespace PhonePage.Migrations
{
    public partial class ProducttableproductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "productid",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productid",
                table: "Products");
        }
    }
}
