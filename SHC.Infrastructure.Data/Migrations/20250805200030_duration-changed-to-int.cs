using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SHC.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class durationchangedtoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "DBAppointment");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMin",
                table: "DBAppointment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMin",
                table: "DBAppointment");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "DBAppointment",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
