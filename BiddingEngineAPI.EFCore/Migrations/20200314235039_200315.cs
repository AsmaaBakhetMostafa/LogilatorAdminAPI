using Microsoft.EntityFrameworkCore.Migrations;

namespace BiddingEngineAPI.EFCore.Migrations
{
    public partial class _200315 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hint",
                table: "FormFileds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hint",
                table: "FormFileds");
        }
    }
}
