using Microsoft.EntityFrameworkCore.Migrations;

namespace BiddingEngineAPI.EFCore.Migrations
{
    public partial class Add_UserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserType_Id",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "RequestTypes",
                columns: table => new
                {
                    RequestType_Id = table.Column<int>(nullable: false),
                    RequestType_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTypes", x => x.RequestType_Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    UserType_Id = table.Column<int>(nullable: false),
                    UserType_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.UserType_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserType_Id",
                table: "Users",
                column: "UserType_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_UserType_Id",
                table: "Users",
                column: "UserType_Id",
                principalTable: "UserTypes",
                principalColumn: "UserType_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_UserType_Id",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RequestTypes");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserType_Id",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "UserType_Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
