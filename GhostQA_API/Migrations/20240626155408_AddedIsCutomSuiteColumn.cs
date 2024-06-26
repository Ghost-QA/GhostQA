using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GhostQA_API.Migrations
{
    public partial class AddedIsCutomSuiteColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Parent",
                table: "tbl_FunctionalSuiteRelation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomSuite",
                table: "tbl_FunctionalSuiteRelation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "tbl_Environments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomSuite",
                table: "tbl_FunctionalSuiteRelation");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tbl_Environments");

            migrationBuilder.AlterColumn<int>(
                name: "Parent",
                table: "tbl_FunctionalSuiteRelation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
