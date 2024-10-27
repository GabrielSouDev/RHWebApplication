using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RHWebApplication.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CompanyName", "CreationDate" },
                values: new object[] { "Staff", new DateTime(2024, 10, 27, 1, 13, 0, 862, DateTimeKind.Local).AddTicks(5293) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Company",
                value: "Staff");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 10, 26, 22, 19, 18, 839, DateTimeKind.Local).AddTicks(7448));
        }
    }
}
