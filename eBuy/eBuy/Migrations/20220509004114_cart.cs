using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eBuy.Migrations
{
    public partial class cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "710bd7f0-f600-4b5b-9833-4adaeff1d97c", "AQAAAAEAACcQAAAAEHCmHdu/e7izZdQNvKR66ZvCCTHCpUQoyX4RrOPLAGduLPXmVjCHbaeM/V1kh6aYLg==", "c693a658-7a50-4f79-ac2e-7c625d0347ce" });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductId",
                table: "Cart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2b87489-711a-450c-8be6-2174168796c6", "AQAAAAEAACcQAAAAEOpbQ8jMpbNqnmc0qo1g8ERZ2ep3Yh3Q7iR1glkd1/mhWadSR+7AvbYRMxCofV7lcQ==", "eca0a511-de38-4b21-b705-78ff4bbf309b" });
        }
    }
}
