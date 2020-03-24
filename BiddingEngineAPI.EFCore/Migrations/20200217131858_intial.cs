using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BiddingEngineAPI.EFCore.Migrations
{
    public partial class intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FiledOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(nullable: false),
                    NameEn = table.Column<string>(nullable: false),
                    IsRelated = table.Column<bool>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiledOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiledOptions_FiledOptions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FiledOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(nullable: false),
                    NameEn = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    FormType = table.Column<int>(nullable: false),
                    UserType = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    ActivationCode = table.Column<string>(nullable: true),
                    UserType_Id = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false),
                    Is_Online = table.Column<bool>(nullable: false),
                    Is_Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "FormFileds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(nullable: false),
                    NameEn = table.Column<string>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    IsRelated = table.Column<bool>(nullable: false),
                    FiledType = table.Column<int>(nullable: false),
                    FormId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFileds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormFileds_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormFileds_FormFileds_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FormFileds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FiledOptions_ParentId",
                table: "FiledOptions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFileds_FormId",
                table: "FormFileds",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFileds_ParentId",
                table: "FormFileds",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiledOptions");

            migrationBuilder.DropTable(
                name: "FormFileds");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Forms");
        }
    }
}
