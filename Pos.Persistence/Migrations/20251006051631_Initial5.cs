using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pos.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherType",
                table: "VoucherType");

            migrationBuilder.RenameTable(
                name: "VoucherType",
                newName: "VoucherTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherTypes",
                table: "VoucherTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "Code", "CreatedDate", "Description", "Length" },
                values: new object[,]
                {
                    { new Guid("2a5c854a-cb59-4a18-9c30-ba30b045af0f"), "1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "D.N.I.", 8 },
                    { new Guid("48b2bc94-18da-4c63-b577-ebd242448600"), "6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "R.U.C.", 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Code_DocumentType_Unique",
                table: "DocumentTypes",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherTypes",
                table: "VoucherTypes");

            migrationBuilder.RenameTable(
                name: "VoucherTypes",
                newName: "VoucherType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherType",
                table: "VoucherType",
                column: "Id");
        }
    }
}
