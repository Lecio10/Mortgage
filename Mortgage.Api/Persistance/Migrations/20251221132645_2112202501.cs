using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mortgage.Migrations
{
    /// <inheritdoc />
    public partial class _2112202501 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mortgageRateHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MortgageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mortgageRateHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mortgageRateHistories_Mortgages_MortgageId",
                        column: x => x.MortgageId,
                        principalTable: "Mortgages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mortgageSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MortgageeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemainingCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidPrincipal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingInstalments = table.Column<int>(type: "int", nullable: false),
                    SnapshotDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mortgageSnapshots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Overpayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Overpayment_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Overpayments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mortgageRateHistories_MortgageId",
                table: "mortgageRateHistories",
                column: "MortgageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mortgageRateHistories");

            migrationBuilder.DropTable(
                name: "mortgageSnapshots");

            migrationBuilder.DropTable(
                name: "Overpayments");
        }
    }
}
