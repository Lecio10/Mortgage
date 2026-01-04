using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mortgage.Migrations
{
    /// <inheritdoc />
    public partial class _2112202513 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instalments",
                table: "Mortgages");

            migrationBuilder.DropColumn(
                name: "Interest_Sum",
                table: "Mortgages");

            migrationBuilder.RenameColumn(
                name: "Total_Sum",
                table: "Mortgages",
                newName: "Number_Of_Instalments");

            migrationBuilder.AddColumn<string>(
                name: "Next_Instalment_Date",
                table: "Mortgages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Remainig_Loan",
                table: "Mortgages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Remaining_Instalments",
                table: "Mortgages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Schedule_Based_Interest_Sum",
                table: "Mortgages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Schedule_Based_Total_Sum",
                table: "Mortgages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Next_Instalment_Date",
                table: "Mortgages");

            migrationBuilder.DropColumn(
                name: "Remainig_Loan",
                table: "Mortgages");

            migrationBuilder.DropColumn(
                name: "Remaining_Instalments",
                table: "Mortgages");

            migrationBuilder.DropColumn(
                name: "Schedule_Based_Interest_Sum",
                table: "Mortgages");

            migrationBuilder.DropColumn(
                name: "Schedule_Based_Total_Sum",
                table: "Mortgages");

            migrationBuilder.RenameColumn(
                name: "Number_Of_Instalments",
                table: "Mortgages",
                newName: "Total_Sum");

            migrationBuilder.AddColumn<double>(
                name: "Instalments",
                table: "Mortgages",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Interest_Sum",
                table: "Mortgages",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
