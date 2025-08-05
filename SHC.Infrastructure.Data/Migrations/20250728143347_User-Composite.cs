using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SHC.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserComposite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "DBPatient");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "DBPatient");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "DBPatient",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DBUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cin = table.Column<long>(type: "bigint", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBUser", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DBUser");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DBPatient");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "DBPatient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "DBPatient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
