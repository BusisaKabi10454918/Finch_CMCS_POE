using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG_PART2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicManagers",
                columns: table => new
                {
                    ManagerId = table.Column<int>(type: "INTEGER", maxLength: 10, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicManagers", x => x.ManagerId);
                });

            migrationBuilder.CreateTable(
                name: "IndependentLecturers",
                columns: table => new
                {
                    LecturerID = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndependentLecturers", x => x.LecturerID);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammeCoordinators",
                columns: table => new
                {
                    CoordinatorId = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeCoordinators", x => x.CoordinatorId);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    ModuleName = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    LecturerRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    LecturerID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.ModuleCode);
                    table.ForeignKey(
                        name: "FK_Modules_IndependentLecturers_LecturerID",
                        column: x => x.LecturerID,
                        principalTable: "IndependentLecturers",
                        principalColumn: "LecturerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ClaimID = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimantLecturerID = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimedModuleModuleCode = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimedHours = table.Column<int>(type: "INTEGER", nullable: false),
                    SuppDocs = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimPeriodStart = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ClaimPeriodEnd = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ClaimID);
                    table.ForeignKey(
                        name: "FK_Claims_IndependentLecturers_ClaimantLecturerID",
                        column: x => x.ClaimantLecturerID,
                        principalTable: "IndependentLecturers",
                        principalColumn: "LecturerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claims_Modules_ClaimedModuleModuleCode",
                        column: x => x.ClaimedModuleModuleCode,
                        principalTable: "Modules",
                        principalColumn: "ModuleCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimantLecturerID",
                table: "Claims",
                column: "ClaimantLecturerID");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimedModuleModuleCode",
                table: "Claims",
                column: "ClaimedModuleModuleCode");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_LecturerID",
                table: "Modules",
                column: "LecturerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicManagers");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "ProgrammeCoordinators");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "IndependentLecturers");
        }
    }
}
