using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GhostQA_API.Migrations
{
    public partial class Remove_Application_FRM_ENV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_Environments");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbl_Environments");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "tbl_Environments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_Environments",
                type: "INT",
                nullable: true);

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
        }
    }
}
