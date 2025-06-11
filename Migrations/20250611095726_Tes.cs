using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConstructionOrganizations.Migrations
{
    /// <inheritdoc />
    public partial class Tes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrigadeMember_Brigades_BrigadesId",
                table: "BrigadeMember");

            migrationBuilder.DropForeignKey(
                name: "FK_BrigadeMember_Employees_MembersId",
                table: "BrigadeMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BrigadeMember",
                table: "BrigadeMember");

            migrationBuilder.RenameColumn(
                name: "MembersId",
                table: "BrigadeMember",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "BrigadesId",
                table: "BrigadeMember",
                newName: "BrigadeId");

            migrationBuilder.RenameIndex(
                name: "IX_BrigadeMember_MembersId",
                table: "BrigadeMember",
                newName: "IX_BrigadeMember_EmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BrigadeMember",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BrigadeMember",
                table: "BrigadeMember",
                column: "Id");

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
                name: "IX_EmployeeAssignment_EmployeeId",
                table: "EmployeeAssignment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAssignment_ProjectId",
                table: "EmployeeAssignment",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_BrigadeMember_Brigades_BrigadeId",
                table: "BrigadeMember",
                column: "BrigadeId",
                principalTable: "Brigades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrigadeMember_Employees_EmployeeId",
                table: "BrigadeMember",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrigadeMember_Brigades_BrigadeId",
                table: "BrigadeMember");

            migrationBuilder.DropForeignKey(
                name: "FK_BrigadeMember_Employees_EmployeeId",
                table: "BrigadeMember");

            migrationBuilder.DropTable(
                name: "EmployeeAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BrigadeMember",
                table: "BrigadeMember");

            migrationBuilder.DropIndex(
                name: "IX_BrigadeMember_BrigadeId",
                table: "BrigadeMember");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BrigadeMember");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "BrigadeMember",
                newName: "MembersId");

            migrationBuilder.RenameColumn(
                name: "BrigadeId",
                table: "BrigadeMember",
                newName: "BrigadesId");

            migrationBuilder.RenameIndex(
                name: "IX_BrigadeMember_EmployeeId",
                table: "BrigadeMember",
                newName: "IX_BrigadeMember_MembersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BrigadeMember",
                table: "BrigadeMember",
                columns: new[] { "BrigadesId", "MembersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BrigadeMember_Brigades_BrigadesId",
                table: "BrigadeMember",
                column: "BrigadesId",
                principalTable: "Brigades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrigadeMember_Employees_MembersId",
                table: "BrigadeMember",
                column: "MembersId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
