using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timesheets.Migrations
{
    /// <inheritdoc />
    public partial class Timesheet_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimesheetEntries_Timesheets_TimesheetId",
                table: "TimesheetEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Timesheets",
                table: "Timesheets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimesheetEntries",
                table: "TimesheetEntries");

            migrationBuilder.RenameTable(
                name: "Timesheets",
                newName: "timesheets");

            migrationBuilder.RenameTable(
                name: "TimesheetEntries",
                newName: "timesheet_entries");

            migrationBuilder.RenameIndex(
                name: "IX_TimesheetEntries_TimesheetId",
                table: "timesheet_entries",
                newName: "IX_timesheet_entries_TimesheetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_timesheets",
                table: "timesheets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_timesheet_entries",
                table: "timesheet_entries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_timesheet_entries_timesheets_TimesheetId",
                table: "timesheet_entries",
                column: "TimesheetId",
                principalTable: "timesheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timesheet_entries_timesheets_TimesheetId",
                table: "timesheet_entries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_timesheets",
                table: "timesheets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_timesheet_entries",
                table: "timesheet_entries");

            migrationBuilder.RenameTable(
                name: "timesheets",
                newName: "Timesheets");

            migrationBuilder.RenameTable(
                name: "timesheet_entries",
                newName: "TimesheetEntries");

            migrationBuilder.RenameIndex(
                name: "IX_timesheet_entries_TimesheetId",
                table: "TimesheetEntries",
                newName: "IX_TimesheetEntries_TimesheetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Timesheets",
                table: "Timesheets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimesheetEntries",
                table: "TimesheetEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimesheetEntries_Timesheets_TimesheetId",
                table: "TimesheetEntries",
                column: "TimesheetId",
                principalTable: "Timesheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
