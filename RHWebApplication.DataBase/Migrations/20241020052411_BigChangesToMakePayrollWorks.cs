using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RHWebApplication.Database.Migrations
{
    /// <inheritdoc />
    public partial class BigChangesToMakePayrollWorks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUnhealthy",
                table: "Job");

            migrationBuilder.AddColumn<decimal>(
                name: "INSSDeduction",
                table: "PayrollHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IRRFDeduction",
                table: "PayrollHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OverTimeAditionals",
                table: "PayrollHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PericulosityValue",
                table: "PayrollHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnhealthyValue",
                table: "PayrollHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OverTimeValue",
                table: "Job",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnhealthyLevel",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dependents",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "INSSDeduction",
                table: "PayrollHistory");

            migrationBuilder.DropColumn(
                name: "IRRFDeduction",
                table: "PayrollHistory");

            migrationBuilder.DropColumn(
                name: "OverTimeAditionals",
                table: "PayrollHistory");

            migrationBuilder.DropColumn(
                name: "PericulosityValue",
                table: "PayrollHistory");

            migrationBuilder.DropColumn(
                name: "UnhealthyValue",
                table: "PayrollHistory");

            migrationBuilder.DropColumn(
                name: "OverTimeValue",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "UnhealthyLevel",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Dependents",
                table: "Employees");

            migrationBuilder.AddColumn<bool>(
                name: "IsUnhealthy",
                table: "Job",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
