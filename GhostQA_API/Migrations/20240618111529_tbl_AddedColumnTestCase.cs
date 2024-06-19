using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GhostQA_API.Migrations
{
    public partial class tbl_AddedColumnTestCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RootId",
                table: "tbl_TestCase",
                type: "int",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RootId",
                table: "tbl_TestCase");
        }
    }
}
