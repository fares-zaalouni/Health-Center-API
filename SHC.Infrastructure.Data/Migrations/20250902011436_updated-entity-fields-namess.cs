using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SHC.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedentityfieldsnamess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DBUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DBAdmin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBAdmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DBAdmin_DBUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DBUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBDoctor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBDoctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DBDoctor_DBUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DBUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBPatient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodType = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    Height = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBPatient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DBPatient_DBUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DBUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBRefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReplacedByToken = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBRefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DBRefreshToken_DBRefreshToken_ReplacedByToken",
                        column: x => x.ReplacedByToken,
                        principalTable: "DBRefreshToken",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DBRefreshToken_DBUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DBUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBSecretary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBSecretary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DBSecretary_DBUser_UserId",
                        column: x => x.UserId,
                        principalTable: "DBUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBAllergy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allergent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllergySeverity = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBAllergy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DBAllergy_DBPatient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "DBPatient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBAppointment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUrgent = table.Column<bool>(type: "bit", nullable: false),
                    DurationInMin = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBAppointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DBAppointment_DBPatient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "DBPatient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBMedicalCondition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBMedicalCondition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DBMedicalCondition_DBPatient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "DBPatient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBMedicalPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailyDoze = table.Column<float>(type: "real", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MedicationType = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBMedicalPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DBMedicalPlan_DBPatient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "DBPatient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBMedicationIntake",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Doze = table.Column<float>(type: "real", nullable: false),
                    IntakeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBMedicationIntake", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DBMedicationIntake_DBMedicalPlan_MedicalPlanId",
                        column: x => x.MedicalPlanId,
                        principalTable: "DBMedicalPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DBAdmin_UserId",
                table: "DBAdmin",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DBAllergy_PatientId",
                table: "DBAllergy",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DBAppointment_PatientId",
                table: "DBAppointment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DBDoctor_UserId",
                table: "DBDoctor",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DBMedicalCondition_PatientId",
                table: "DBMedicalCondition",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DBMedicalPlan_PatientId",
                table: "DBMedicalPlan",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DBMedicationIntake_MedicalPlanId",
                table: "DBMedicationIntake",
                column: "MedicalPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DBPatient_UserId",
                table: "DBPatient",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DBRefreshToken_ReplacedByToken",
                table: "DBRefreshToken",
                column: "ReplacedByToken",
                unique: true,
                filter: "[ReplacedByToken] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DBRefreshToken_UserId",
                table: "DBRefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DBSecretary_UserId",
                table: "DBSecretary",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DBAdmin");

            migrationBuilder.DropTable(
                name: "DBAllergy");

            migrationBuilder.DropTable(
                name: "DBAppointment");

            migrationBuilder.DropTable(
                name: "DBDoctor");

            migrationBuilder.DropTable(
                name: "DBMedicalCondition");

            migrationBuilder.DropTable(
                name: "DBMedicationIntake");

            migrationBuilder.DropTable(
                name: "DBRefreshToken");

            migrationBuilder.DropTable(
                name: "DBSecretary");

            migrationBuilder.DropTable(
                name: "DBMedicalPlan");

            migrationBuilder.DropTable(
                name: "DBPatient");

            migrationBuilder.DropTable(
                name: "DBUser");
        }
    }
}
