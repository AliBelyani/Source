using Microsoft.EntityFrameworkCore.Migrations;

namespace DK.Data.EF.Migrations
{
    public partial class changeRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "xIsAdmin",
                schema: "Security",
                table: "Role",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xIsAdmin",
                schema: "Security",
                table: "Role");
        }
    }
}
