using Microsoft.EntityFrameworkCore.Migrations;

namespace BiddingEngineAPI.EFCore.Migrations
{
    public partial class beww : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPredefined",
                table: "FormFileds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PredfinedID",
                table: "FormFileds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPredefined",
                table: "FormFileds");

            migrationBuilder.DropColumn(
                name: "PredfinedID",
                table: "FormFileds");
        }
    }
}
