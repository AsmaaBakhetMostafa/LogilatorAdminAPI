using Microsoft.EntityFrameworkCore.Migrations;

namespace BiddingEngineAPI.EFCore.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRelated",
                table: "FormFileds");

            migrationBuilder.DropColumn(
                name: "IsRelated",
                table: "FiledOptions");

            migrationBuilder.AddColumn<int>(
                name: "RequestType",
                table: "Forms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasChild",
                table: "FormFileds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasParent",
                table: "FormFileds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasChild",
                table: "FiledOptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasParent",
                table: "FiledOptions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestType",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "HasChild",
                table: "FormFileds");

            migrationBuilder.DropColumn(
                name: "HasParent",
                table: "FormFileds");

            migrationBuilder.DropColumn(
                name: "HasChild",
                table: "FiledOptions");

            migrationBuilder.DropColumn(
                name: "HasParent",
                table: "FiledOptions");

            migrationBuilder.AddColumn<bool>(
                name: "IsRelated",
                table: "FormFileds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRelated",
                table: "FiledOptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
