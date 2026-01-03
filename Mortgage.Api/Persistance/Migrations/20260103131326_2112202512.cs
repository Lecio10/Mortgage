using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mortgage.Migrations
{
    /// <inheritdoc />
    public partial class _2112202512 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "ScheduledPayments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "ScheduledPayments");
        }
    }
}
