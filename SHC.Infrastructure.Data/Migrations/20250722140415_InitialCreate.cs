using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SHC.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DBPatient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContactPhone = table.Column<int>(type: "int", nullable: true),
                    BloodType = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    Height = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBPatient", x => x.Id);
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
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    AssignedDoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    MedicalPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBMedicationIntake", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DBMedicationIntake_DBMedicalPlan_MedicalPlanId",
                        column: x => x.MedicalPlanId,
                        principalTable: "DBMedicalPlan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DBAllergy_PatientId",
                table: "DBAllergy",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DBAppointment_PatientId",
                table: "DBAppointment",
                column: "PatientId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DBAllergy");

            migrationBuilder.DropTable(
                name: "DBAppointment");

            migrationBuilder.DropTable(
                name: "DBMedicalCondition");

            migrationBuilder.DropTable(
                name: "DBMedicationIntake");

            migrationBuilder.DropTable(
                name: "DBMedicalPlan");

            migrationBuilder.DropTable(
                name: "DBPatient");
        }
    }
}
