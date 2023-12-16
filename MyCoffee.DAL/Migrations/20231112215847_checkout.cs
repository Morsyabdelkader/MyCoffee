using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCoffee.DAL.Migrations
{
    /// <inheritdoc />
    public partial class checkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_HeaderOrder_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "HeaderOrder");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderDetails");

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_order_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_order_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "HeaderOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ordertotal = table.Column<double>(type: "float", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postalcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtotal = table.Column<double>(type: "float", nullable: false),
                    TransctionId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeaderOrder_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.id);
                    table.ForeignKey(
                        name: "FK_ShoppingCart_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCart_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeaderOrder_UserId",
                table: "HeaderOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ProductId",
                table: "ShoppingCart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_UserId",
                table: "ShoppingCart",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_HeaderOrder_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "HeaderOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
