using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GhostQA_API.Migrations
{
    public partial class testcase_tale_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_SuiteScheduleInfo");

            migrationBuilder.DropColumn(
                name: "IsCustomSuite",
                table: "tbl_FunctionalSuiteRelation");

            migrationBuilder.AddColumn<int>(
                name: "TestCaseId",
                table: "tbl_TestCase",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1000, 1");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_TestCase",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_TestCase",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_TestCase",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_TestCase",
                table: "tbl_TestCase",
                column: "TestCaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_TestCase",
                table: "tbl_TestCase");

            migrationBuilder.DropColumn(
                name: "TestCaseId",
                table: "tbl_TestCase");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_TestCase");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_TestCase");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_TestCase");

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomSuite",
                table: "tbl_FunctionalSuiteRelation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "tbl_SuiteScheduleInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CroneExpression = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecurringInterval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuiteName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SuiteScheduleInfo", x => x.Id);
                });
        }
    }
}
