using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstructionOrganizations.Migrations
{
    /// <inheritdoc />
    public partial class Te : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrigadeMember");

            migrationBuilder.DropTable(
                name: "EmployeeAssignment");

            migrationBuilder.AddColumn<int>(
                name: "BrigadeId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BrigadeWorkAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrigadeId = table.Column<int>(type: "integer", nullable: false),
                    WorkScheduleId = table.Column<int>(type: "integer", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrigadeWorkAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrigadeWorkAssignment_Brigades_BrigadeId",
                        column: x => x.BrigadeId,
                        principalTable: "Brigades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrigadeWorkAssignment_WorkSchedules_WorkScheduleId",
                        column: x => x.WorkScheduleId,
                        principalTable: "WorkSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentObjectAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EquipmentId = table.Column<int>(type: "integer", nullable: false),
                    ObjectId = table.Column<int>(type: "integer", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentObjectAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentObjectAssignment_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentObjectAssignment_Objects_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BrigadeId",
                table: "Employees",
                column: "BrigadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProjectId",
                table: "Employees",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BrigadeWorkAssignment_BrigadeId",
                table: "BrigadeWorkAssignment",
                column: "BrigadeId");

            migrationBuilder.CreateIndex(
                name: "IX_BrigadeWorkAssignment_WorkScheduleId",
                table: "BrigadeWorkAssignment",
                column: "WorkScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentObjectAssignment_EquipmentId",
                table: "EquipmentObjectAssignment",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentObjectAssignment_ObjectId",
                table: "EquipmentObjectAssignment",
                column: "ObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Brigades_BrigadeId",
                table: "Employees",
                column: "BrigadeId",
                principalTable: "Brigades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ConstructionProject_ProjectId",
                table: "Employees",
                column: "ProjectId",
                principalTable: "ConstructionProject",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Brigades_BrigadeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ConstructionProject_ProjectId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "BrigadeWorkAssignment");

            migrationBuilder.DropTable(
                name: "EquipmentObjectAssignment");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BrigadeId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ProjectId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BrigadeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "BrigadeMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrigadeId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrigadeMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrigadeMember_Brigades_BrigadeId",
                        column: x => x.BrigadeId,
                        principalTable: "Brigades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrigadeMember_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAssignment_ConstructionProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ConstructionProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAssignment_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrigadeMember_BrigadeId",
                table: "BrigadeMember",
                column: "BrigadeId");

            migrationBuilder.CreateIndex(
                name: "IX_BrigadeMember_EmployeeId",
                table: "BrigadeMember",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAssignment_EmployeeId",
                table: "EmployeeAssignment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAssignment_ProjectId",
                table: "EmployeeAssignment",
                column: "ProjectId");
        }
    }
}
