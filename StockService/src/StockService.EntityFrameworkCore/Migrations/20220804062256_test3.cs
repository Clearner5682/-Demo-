using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockService.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DuplicateOrderChecks");

            migrationBuilder.CreateTable(
                name: "OrderChecks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessResult = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderChecks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderChecks_OrderId",
                table: "OrderChecks",
                column: "OrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderChecks");

            migrationBuilder.CreateTable(
                name: "DuplicateOrderChecks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuplicateOrderChecks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateOrderChecks_OrderId",
                table: "DuplicateOrderChecks",
                column: "OrderId",
                unique: true);
        }
    }
}
