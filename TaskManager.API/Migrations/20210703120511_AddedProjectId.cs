using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.API.Migrations
{
    public partial class AddedProjectId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "DateOfStart", "ProjectName", "TeamSize" },
                values: new object[] { new Guid("922388fd-0461-4cb9-b9cb-a3a0cc21036c"), new DateTime(2021, 7, 3, 14, 5, 11, 48, DateTimeKind.Local).AddTicks(1528), "Project A", 5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("922388fd-0461-4cb9-b9cb-a3a0cc21036c"));

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "DateOfStart", "ProjectName", "TeamSize" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2021, 7, 2, 19, 58, 20, 144, DateTimeKind.Local).AddTicks(7114), "Project A", 5 });
        }
    }
}
