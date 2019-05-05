using Microsoft.EntityFrameworkCore.Migrations;

namespace Playground2.Migrations
{
    public partial class OrderItemsMalfunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_IcecreamId",
                table: "OrderItems",
                column: "IcecreamId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Icecreams_IcecreamId",
                table: "OrderItems",
                column: "IcecreamId",
                principalTable: "Icecreams",
                principalColumn: "IcecreamId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Icecreams_IcecreamId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_IcecreamId",
                table: "OrderItems");
        }
    }
}
