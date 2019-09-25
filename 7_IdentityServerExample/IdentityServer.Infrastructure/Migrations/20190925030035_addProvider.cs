using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Infrastructure.Migrations
{
    public partial class addProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identity",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Users");
        }
    }
}
