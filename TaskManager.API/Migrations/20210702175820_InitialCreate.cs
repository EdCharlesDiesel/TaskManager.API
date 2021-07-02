using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeamSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "DateOfStart", "ProjectName", "TeamSize" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2021, 7, 2, 19, 58, 20, 144, DateTimeKind.Local).AddTicks(7114), "Project A", 5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
