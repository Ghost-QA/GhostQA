using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GhostQA_API.Migrations
{
    public partial class change_location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "tbl_Location",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "tbl_Location");
        }
    }
}
