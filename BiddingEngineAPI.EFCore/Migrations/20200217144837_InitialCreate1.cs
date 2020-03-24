using Microsoft.EntityFrameworkCore.Migrations;

namespace BiddingEngineAPI.EFCore.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "Forms",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "FormFiledId",
                table: "FiledOptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FiledOptions_FormFiledId",
                table: "FiledOptions",
                column: "FormFiledId");

            migrationBuilder.AddForeignKey(
                name: "FK_FiledOptions_FormFileds_FormFiledId",
                table: "FiledOptions",
                column: "FormFiledId",
                principalTable: "FormFileds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FiledOptions_FormFileds_FormFiledId",
                table: "FiledOptions");

            migrationBuilder.DropIndex(
                name: "IX_FiledOptions_FormFiledId",
                table: "FiledOptions");

            migrationBuilder.DropColumn(
                name: "FormFiledId",
                table: "FiledOptions");

            migrationBuilder.AlterColumn<bool>(
                name: "UserType",
                table: "Forms",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
