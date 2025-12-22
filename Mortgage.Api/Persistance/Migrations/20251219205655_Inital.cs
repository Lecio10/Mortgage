using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mortgage.Migrations
{
    /// <inheritdoc />
    public partial class Inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mortgages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    First_Instalment_Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Loan_Ammount = table.Column<double>(type: "float", nullable: false),
                    Instalments = table.Column<double>(type: "float", nullable: false),
                    Interest_Rate_In_Percent = table.Column<double>(type: "float", nullable: false),
                    Interest_Sum = table.Column<double>(type: "float", nullable: false),
                    Total_Sum = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mortgages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Generation_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number_Of_Payments = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numer_Raty = table.Column<int>(type: "int", nullable: false),
                    Data_Płatności = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wysokość_Raty = table.Column<double>(type: "float", nullable: false),
                    Kwota_Odsetek = table.Column<double>(type: "float", nullable: false),
                    Kwota_Kapitału = table.Column<double>(type: "float", nullable: false),
                    Pozostało_Do_Spłaty = table.Column<double>(type: "float", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ScheduleId",
                table: "Payments",
                column: "ScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mortgages");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Schedules");
        }
    }
}
