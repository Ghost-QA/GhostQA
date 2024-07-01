using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GhostQA_API.Migrations
{
    public partial class CreateSuiteScheduleInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_SuiteScheduleInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Interval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuiteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RootId = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaysOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayOfMonth = table.Column<int>(type: "int", nullable: true),
                    RepeatEvery = table.Column<int>(type: "int", nullable: true),
                    JobId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationId = table.Column<int>(type: "INT", nullable: true),
                    TenantId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SuiteScheduleInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_SuiteScheduleInfo");
        }
    }
}
