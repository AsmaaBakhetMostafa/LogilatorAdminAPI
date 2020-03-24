using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BiddingEngineAPI.EFCore.Migrations
{
    public partial class Bid_BidDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Requests");

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    FormId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    UserTypeId = table.Column<int>(nullable: false),
                    RequestId = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bids_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BidDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BidId = table.Column<int>(nullable: false),
                    FormFieldId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidDetails_Bids_BidId",
                        column: x => x.BidId,
                        principalTable: "Bids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidDetails_FormFileds_FormFieldId",
                        column: x => x.FormFieldId,
                        principalTable: "FormFileds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidDetails_BidId",
                table: "BidDetails",
                column: "BidId");

            migrationBuilder.CreateIndex(
                name: "IX_BidDetails_FormFieldId",
                table: "BidDetails",
                column: "FormFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_FormId",
                table: "Bids",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_RequestId",
                table: "Bids",
                column: "RequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidDetails");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Requests",
                type: "bit",
                nullable: true);
        }
    }
}
