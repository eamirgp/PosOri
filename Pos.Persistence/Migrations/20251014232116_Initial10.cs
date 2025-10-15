using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pos.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_IGVTypes_IGVTypeId",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_Products_ProductId",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_Purchases_PurchaseId",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_UnitOfMeasures_UnitOfMeasureId",
                table: "PurchaseDetail");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse_Name_Unique",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_VoucherType_Code_Unique",
                table: "VoucherTypes");

            migrationBuilder.DropIndex(
                name: "IX_UnitOfMeasure_Code_Unique",
                table: "UnitOfMeasures");

            migrationBuilder.DropIndex(
                name: "IX_Product_Code_Unique",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Person_DocumentNumber_Unique",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_IGVType_Code_Unique",
                table: "IGVTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentType_Code_Unique",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Currency_Code_Unique",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Category_Name_Unique",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseDetail",
                table: "PurchaseDetail");

            migrationBuilder.RenameTable(
                name: "PurchaseDetail",
                newName: "PurchaseDetails");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_UnitOfMeasureId",
                table: "PurchaseDetails",
                newName: "IX_PurchaseDetails_UnitOfMeasureId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_PurchaseId",
                table: "PurchaseDetails",
                newName: "IX_PurchaseDetails_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_ProductId",
                table: "PurchaseDetails",
                newName: "IX_PurchaseDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_IGVTypeId",
                table: "PurchaseDetails",
                newName: "IX_PurchaseDetails_IGVTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseDetails",
                table: "PurchaseDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoucherTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Serie = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_VoucherTypes_VoucherTypeId",
                        column: x => x.VoucherTypeId,
                        principalTable: "VoucherTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherSeries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoucherTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Serie = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    CurrentNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherSeries_VoucherTypes_VoucherTypeId",
                        column: x => x.VoucherTypeId,
                        principalTable: "VoucherTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitOfMeasureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IGVTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleDetails_IGVTypes_IGVTypeId",
                        column: x => x.IGVTypeId,
                        principalTable: "IGVTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleDetails_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleDetails_UnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VoucherSeries",
                columns: new[] { "Id", "CreatedDate", "CurrentNumber", "Serie", "VoucherTypeId" },
                values: new object[,]
                {
                    { new Guid("323dedd6-1dbe-4958-bcec-470c1e6adbd9"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "F001", new Guid("9e991aec-8d56-438e-966f-e5d8079d2ab7") },
                    { new Guid("6a661525-7402-470d-9251-d14d1cc105cf"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "B001", new Guid("85b8d448-a864-4f46-8ff1-4dec57d30d23") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_IGVTypeId",
                table: "SaleDetails",
                column: "IGVTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_ProductId",
                table: "SaleDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_SaleId",
                table: "SaleDetails",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_UnitOfMeasureId",
                table: "SaleDetails",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CurrencyId",
                table: "Sales",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PersonId",
                table: "Sales",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_VoucherTypeId",
                table: "Sales",
                column: "VoucherTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_WarehouseId",
                table: "Sales",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherSeries_VoucherTypeId",
                table: "VoucherSeries",
                column: "VoucherTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_IGVTypes_IGVTypeId",
                table: "PurchaseDetails",
                column: "IGVTypeId",
                principalTable: "IGVTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_Products_ProductId",
                table: "PurchaseDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_Purchases_PurchaseId",
                table: "PurchaseDetails",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_UnitOfMeasures_UnitOfMeasureId",
                table: "PurchaseDetails",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_IGVTypes_IGVTypeId",
                table: "PurchaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_Products_ProductId",
                table: "PurchaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_Purchases_PurchaseId",
                table: "PurchaseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_UnitOfMeasures_UnitOfMeasureId",
                table: "PurchaseDetails");

            migrationBuilder.DropTable(
                name: "SaleDetails");

            migrationBuilder.DropTable(
                name: "VoucherSeries");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseDetails",
                table: "PurchaseDetails");

            migrationBuilder.RenameTable(
                name: "PurchaseDetails",
                newName: "PurchaseDetail");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetails_UnitOfMeasureId",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_UnitOfMeasureId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetails_PurchaseId",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetails_ProductId",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetails_IGVTypeId",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_IGVTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseDetail",
                table: "PurchaseDetail",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_Name_Unique",
                table: "Warehouses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherType_Code_Unique",
                table: "VoucherTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasure_Code_Unique",
                table: "UnitOfMeasures",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Code_Unique",
                table: "Products",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_DocumentNumber_Unique",
                table: "Persons",
                column: "DocumentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IGVType_Code_Unique",
                table: "IGVTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_Code_Unique",
                table: "DocumentTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Code_Unique",
                table: "Currencies",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name_Unique",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_IGVTypes_IGVTypeId",
                table: "PurchaseDetail",
                column: "IGVTypeId",
                principalTable: "IGVTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_Products_ProductId",
                table: "PurchaseDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_Purchases_PurchaseId",
                table: "PurchaseDetail",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_UnitOfMeasures_UnitOfMeasureId",
                table: "PurchaseDetail",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
