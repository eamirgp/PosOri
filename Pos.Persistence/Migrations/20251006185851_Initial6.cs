using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pos.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Name_Warehouse_Unique",
                table: "Warehouses",
                newName: "IX_Warehouse_Name_Unique");

            migrationBuilder.RenameIndex(
                name: "IX_Code_VoucherType_Unique",
                table: "VoucherTypes",
                newName: "IX_VoucherType_Code_Unique");

            migrationBuilder.RenameIndex(
                name: "IX_Code_Product_Unique",
                table: "Products",
                newName: "IX_Product_Code_Unique");

            migrationBuilder.RenameIndex(
                name: "IX_Code_DocumentType_Unique",
                table: "DocumentTypes",
                newName: "IX_DocumentType_Code_Unique");

            migrationBuilder.RenameIndex(
                name: "IX_Name_Category_Unique",
                table: "Categories",
                newName: "IX_Category_Name_Unique");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_DocumentNumber_Unique",
                table: "Persons",
                column: "DocumentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DocumentTypeId",
                table: "Persons",
                column: "DocumentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_Name_Unique",
                table: "Warehouses",
                newName: "IX_Name_Warehouse_Unique");

            migrationBuilder.RenameIndex(
                name: "IX_VoucherType_Code_Unique",
                table: "VoucherTypes",
                newName: "IX_Code_VoucherType_Unique");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Code_Unique",
                table: "Products",
                newName: "IX_Code_Product_Unique");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentType_Code_Unique",
                table: "DocumentTypes",
                newName: "IX_Code_DocumentType_Unique");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Name_Unique",
                table: "Categories",
                newName: "IX_Name_Category_Unique");
        }
    }
}
