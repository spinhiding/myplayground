using Microsoft.EntityFrameworkCore.Migrations;

namespace Playground2.Data.Migrations
{
    public partial class CartItem2Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartItemsId",
                table: "CartItems",
                newName: "CartItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartItemId",
                table: "CartItems",
                newName: "CartItemsId");
        }
    }
}
