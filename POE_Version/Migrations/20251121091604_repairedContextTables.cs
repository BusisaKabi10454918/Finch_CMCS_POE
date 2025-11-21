using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG.Migrations
{
    /// <inheritdoc />
    public partial class repairedContextTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicManagers",
                columns: table => new
                {
                    ManagerID = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicManagers", x => x.ManagerID);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssignedModules = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ContractedLecturer = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MaximumClaimableAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractID);
                });

            migrationBuilder.CreateTable(
                name: "HRAdministrators",
                columns: table => new
                {
                    HumanResourcesID = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRAdministrators", x => x.HumanResourcesID);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModuleCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ModuleName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ModuleRatePerHour = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.ModuleID);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammeCoordinators",
                columns: table => new
                {
                    CoordinatorID = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeCoordinators", x => x.CoordinatorID);
                });

            migrationBuilder.CreateTable(
                name: "IndependentLecturers",
                columns: table => new
                {
                    LecturerID = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NumberOfClaims = table.Column<int>(type: "INTEGER", nullable: false),
                    LecturerContractContractID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndependentLecturers", x => x.LecturerID);
                    table.ForeignKey(
                        name: "FK_IndependentLecturers_Contracts_LecturerContractContractID",
                        column: x => x.LecturerContractContractID,
                        principalTable: "Contracts",
                        principalColumn: "ContractID");
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ClaimID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimReadID = table.Column<string>(type: "TEXT", nullable: false),
                    ModuleCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LecturerID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimedHours = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClaimPeriodStart = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ClaimPeriodEnd = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    SupportingDocuments = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimStatus = table.Column<string>(type: "TEXT", nullable: false),
                    AdminComments = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ClaimID);
                    table.ForeignKey(
                        name: "FK_Claims_IndependentLecturers_LecturerID",
                        column: x => x.LecturerID,
                        principalTable: "IndependentLecturers",
                        principalColumn: "LecturerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_LecturerID",
                table: "Claims",
                column: "LecturerID");

            migrationBuilder.CreateIndex(
                name: "IX_IndependentLecturers_LecturerContractContractID",
                table: "IndependentLecturers",
                column: "LecturerContractContractID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicManagers");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "HRAdministrators");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "ProgrammeCoordinators");

            migrationBuilder.DropTable(
                name: "IndependentLecturers");

            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
