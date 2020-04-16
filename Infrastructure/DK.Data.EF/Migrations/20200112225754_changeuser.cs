using Microsoft.EntityFrameworkCore.Migrations;

namespace DK.Data.EF.Migrations
{
    public partial class changeuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "xFirstName",
                schema: "Security",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "xLastName",
                schema: "Security",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xFirstName",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "xLastName",
                schema: "Security",
                table: "Users");
        }
    }
}
