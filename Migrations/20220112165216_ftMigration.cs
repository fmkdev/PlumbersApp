using Microsoft.EntityFrameworkCore.Migrations;

namespace PlumbingService.Migrations
{
    public partial class ftMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerReport",
                table: "Jobs",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerReport",
                table: "Jobs");
        }
    }
}
