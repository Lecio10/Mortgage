using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mortgage.Migrations
{
    /// <inheritdoc />
    public partial class _2112202502 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Schedules_ScheduleId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ScheduleId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Data_Płatności",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Kwota_Kapitału",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Kwota_Odsetek",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Numer_Raty",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Pozostało_Do_Spłaty",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Wysokość_Raty",
                table: "Payments");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduledPaymentId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ScheduledPayments",
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
                    table.PrimaryKey("PK_ScheduledPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledPayments_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledPayments_ScheduleId",
                table: "ScheduledPayments",
                column: "ScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledPayments");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ScheduledPaymentId",
                table: "Payments");

            migrationBuilder.AddColumn<string>(
                name: "Data_Płatności",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Kwota_Kapitału",
                table: "Payments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Kwota_Odsetek",
                table: "Payments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Numer_Raty",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Pozostało_Do_Spłaty",
                table: "Payments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Wysokość_Raty",
                table: "Payments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ScheduleId",
                table: "Payments",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Schedules_ScheduleId",
                table: "Payments",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }
    }
}
