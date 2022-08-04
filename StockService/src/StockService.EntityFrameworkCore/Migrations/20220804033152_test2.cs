using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockService.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "DuplicateOrderChecks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DuplicateOrderChecks",
                table: "DuplicateOrderChecks",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DuplicateOrderChecks",
                table: "DuplicateOrderChecks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DuplicateOrderChecks");
        }
    }
}
