using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mortgage.Migrations
{
    /// <inheritdoc />
    public partial class _2112202515 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "First_Interest_Amount",
                table: "Mortgages",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interest_Rate_Type",
                table: "Mortgages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "First_Interest_Amount",
                table: "Mortgages");

            migrationBuilder.DropColumn(
                name: "Interest_Rate_Type",
                table: "Mortgages");
        }
    }
}
