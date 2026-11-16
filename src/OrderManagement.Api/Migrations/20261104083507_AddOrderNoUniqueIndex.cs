using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderNoUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNo",
                table: "Orders",
                column: "OrderNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderNo",
                table: "Orders");
        }
    }
}
