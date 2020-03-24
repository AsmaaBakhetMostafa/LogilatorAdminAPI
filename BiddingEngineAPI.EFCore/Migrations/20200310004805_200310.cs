using Microsoft.EntityFrameworkCore.Migrations;

namespace BiddingEngineAPI.EFCore.Migrations
{
    public partial class _200310 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FixedFiledId",
                table: "FixedFiledDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FixedFiledDetails_FixedFiledId",
                table: "FixedFiledDetails",
                column: "FixedFiledId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixedFiledDetails_FixedFileds_FixedFiledId",
                table: "FixedFiledDetails",
                column: "FixedFiledId",
                principalTable: "FixedFileds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedFiledDetails_FixedFileds_FixedFiledId",
                table: "FixedFiledDetails");

            migrationBuilder.DropIndex(
                name: "IX_FixedFiledDetails_FixedFiledId",
                table: "FixedFiledDetails");

            migrationBuilder.DropColumn(
                name: "FixedFiledId",
                table: "FixedFiledDetails");
        }
    }
}
