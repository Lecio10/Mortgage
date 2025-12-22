using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mortgage.Migrations
{
    /// <inheritdoc />
    public partial class _2112202504 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledPayments_Schedules_ScheduleId",
                table: "ScheduledPayments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ScheduleId",
                table: "ScheduledPayments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledPayments_Schedules_ScheduleId",
                table: "ScheduledPayments",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledPayments_Schedules_ScheduleId",
                table: "ScheduledPayments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ScheduleId",
                table: "ScheduledPayments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledPayments_Schedules_ScheduleId",
                table: "ScheduledPayments",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }
    }
}
