using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GhostQA_API.Migrations
{
    public partial class AddBaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "tbl_TestSuites",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_TestSuites",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_TestSuites",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_TestStepsDetails",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_TestStepsDetails",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_TestStepsDetails",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_TestExecution",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_TestExecution",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_TestExecution",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_TestData",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_TestData",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_TestData",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_TestCaseDetails",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_TestCaseDetails",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_TestCaseDetails",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "tbl_TestCase",
                type: "UNIQUEIDENTIFIER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_TestCase",
                type: "UNIQUEIDENTIFIER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "tbl_TestCase",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_RootRelation",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_RootRelation",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_RootRelation",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_ProjectRootRelation",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_ProjectRootRelation",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_ProjectRootRelation",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_PerformanceProperties",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_PerformanceProperties",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_PerformanceProperties",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_PerformanceLocation",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_PerformanceLocation",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_PerformanceLocation",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_PerformanceFile",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_PerformanceFile",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_PerformanceFile",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_Location",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_Location",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_Location",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_Load",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_Load",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_Load",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_InternalTestExecutions",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_InternalTestExecutions",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_InternalTestExecutions",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_FunctionalTestRun",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_FunctionalTestRun",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_FunctionalTestRun",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_FunctionalTestCase",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_FunctionalTestCase",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_FunctionalTestCase",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_FunctionalSuiteRelation",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_FunctionalSuiteRelation",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_FunctionalSuiteRelation",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_FuncationalTest",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_FuncationalTest",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_FuncationalTest",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "tbl_Environments",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_Environments",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_Environments",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_CypressTestExecution",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_CypressTestExecution",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_CypressTestExecution",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_CypressPerfomanceDetaills",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_CypressPerfomanceDetaills",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "tbl_CypressPerfomanceDetaills",
                type: "UNIQUEIDENTIFIER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_TestSuites");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_TestSuites");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_TestStepsDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_TestStepsDetails");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_TestStepsDetails");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_TestExecution");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_TestExecution");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_TestExecution");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_TestData");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_TestData");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_TestData");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_TestCaseDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_TestCaseDetails");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_TestCaseDetails");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_RootRelation");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_RootRelation");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_RootRelation");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_ProjectRootRelation");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_ProjectRootRelation");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_ProjectRootRelation");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_PerformanceProperties");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_PerformanceProperties");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_PerformanceProperties");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_PerformanceLocation");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_PerformanceLocation");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_PerformanceLocation");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_PerformanceFile");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_PerformanceFile");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_PerformanceFile");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_Location");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_Location");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_Location");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_Load");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_Load");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_Load");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_InternalTestExecutions");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_InternalTestExecutions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_InternalTestExecutions");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_FunctionalTestRun");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_FunctionalTestRun");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_FunctionalTestRun");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_FunctionalTestCase");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_FunctionalTestCase");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_FunctionalTestCase");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_FunctionalSuiteRelation");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_FunctionalSuiteRelation");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_FunctionalSuiteRelation");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_FuncationalTest");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_FuncationalTest");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_FuncationalTest");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_Environments");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_Environments");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_CypressTestExecution");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_CypressTestExecution");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_CypressTestExecution");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_CypressPerfomanceDetaills");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_CypressPerfomanceDetaills");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_CypressPerfomanceDetaills");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "tbl_TestSuites",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "tbl_TestCase",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "tbl_TestCase",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "UNIQUEIDENTIFIER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "tbl_TestCase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "tbl_Environments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);
        }
    }
}
