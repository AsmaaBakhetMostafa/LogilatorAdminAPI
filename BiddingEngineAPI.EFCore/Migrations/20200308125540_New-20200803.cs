using Microsoft.EntityFrameworkCore.Migrations;

namespace BiddingEngineAPI.EFCore.Migrations
{
    public partial class New20200803 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FixedFiledDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedFiledDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedFiledDetails_FixedFiledDetails_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FixedFiledDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FixedFileds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedFileds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FixedFiledDetails_ParentId",
                table: "FixedFiledDetails",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FixedFiledDetails");

            migrationBuilder.DropTable(
                name: "FixedFileds");
        }
    }
}
