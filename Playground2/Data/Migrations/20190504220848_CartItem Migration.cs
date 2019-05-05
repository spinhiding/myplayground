using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Playground2.Data.Migrations
{
    public partial class CartItemMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Icecreams",
                newName: "IcecreamId");

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IcecreamId = table.Column<int>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    CartId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemsId);
                    table.ForeignKey(
                        name: "FK_CartItems_Icecreams_IcecreamId",
                        column: x => x.IcecreamId,
                        principalTable: "Icecreams",
                        principalColumn: "IcecreamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_IcecreamId",
                table: "CartItems",
                column: "IcecreamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.RenameColumn(
                name: "IcecreamId",
                table: "Icecreams",
                newName: "Id");
        }
    }
}
