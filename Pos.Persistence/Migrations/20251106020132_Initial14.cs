using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pos.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryAdjustmentDetails_InventoryAdjustmentTypes_InventoryAdjustmentId",
                table: "InventoryAdjustmentDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_InventoryAdjustmentDetails_InventoryAdjustmentTypes_InventoryAdjustmentId",
                table: "InventoryAdjustmentDetails",
                column: "InventoryAdjustmentId",
                principalTable: "InventoryAdjustmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
