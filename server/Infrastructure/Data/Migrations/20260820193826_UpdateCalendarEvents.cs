using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCalendarEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEvents_Users_ScheduledByUserId",
                table: "CalendarEvents");

            migrationBuilder.RenameColumn(
                name: "ScheduledByUserId",
                table: "CalendarEvents",
                newName: "HostUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CalendarEvents_ScheduledByUserId",
                table: "CalendarEvents",
                newName: "IX_CalendarEvents_HostUserId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "CalendarEvents",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEvents_Users_HostUserId",
                table: "CalendarEvents",
                column: "HostUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEvents_Users_HostUserId",
                table: "CalendarEvents");

            migrationBuilder.RenameColumn(
                name: "HostUserId",
                table: "CalendarEvents",
                newName: "ScheduledByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CalendarEvents_HostUserId",
                table: "CalendarEvents",
                newName: "IX_CalendarEvents_ScheduledByUserId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "CalendarEvents",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEvents_Users_ScheduledByUserId",
                table: "CalendarEvents",
                column: "ScheduledByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
