using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DK.Data.EF.Migrations
{
    public partial class addUserLoginHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLoginHistories",
                schema: "Security",
                columns: table => new
                {
                    xID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    xUserId = table.Column<long>(nullable: false),
                    xRegisterDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginHistories", x => x.xID);
                    table.ForeignKey(
                        name: "FK_UserLoginHistories_Users_xUserId",
                        column: x => x.xUserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "xID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginHistories_xUserId",
                schema: "Security",
                table: "UserLoginHistories",
                column: "xUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLoginHistories",
                schema: "Security");
        }
    }
}
