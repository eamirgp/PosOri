using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pos.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VoucherType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VoucherType",
                columns: new[] { "Id", "Code", "CreatedDate", "Description" },
                values: new object[,]
                {
                    { new Guid("85b8d448-a864-4f46-8ff1-4dec57d30d23"), "03", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Boleta de venta" },
                    { new Guid("9e991aec-8d56-438e-966f-e5d8079d2ab7"), "01", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Factura" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Code_VoucherType_Unique",
                table: "VoucherType",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherType");
        }
    }
}
