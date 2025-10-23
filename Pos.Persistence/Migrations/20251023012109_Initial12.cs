using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pos.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_UnitOfMeasures_UnitOfMeasureId",
                table: "PurchaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_UnitOfMeasures_UnitOfMeasureId",
                table: "SaleDetails");

            migrationBuilder.DropIndex(
                name: "IX_SaleDetails_UnitOfMeasureId",
                table: "SaleDetails");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseDetails_UnitOfMeasureId",
                table: "PurchaseDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "IGVTypeId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Products_IGVTypeId",
                table: "Products",
                column: "IGVTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_IGVTypes_IGVTypeId",
                table: "Products",
                column: "IGVTypeId",
                principalTable: "IGVTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_IGVTypes_IGVTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_IGVTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IGVTypeId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_UnitOfMeasureId",
                table: "SaleDetails",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_UnitOfMeasureId",
                table: "PurchaseDetails",
                column: "UnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_UnitOfMeasures_UnitOfMeasureId",
                table: "PurchaseDetails",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_UnitOfMeasures_UnitOfMeasureId",
                table: "SaleDetails",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
