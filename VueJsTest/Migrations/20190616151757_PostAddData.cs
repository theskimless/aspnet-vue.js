using Microsoft.EntityFrameworkCore.Migrations;

namespace VueJsTest.Migrations
{
    public partial class PostAddData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Posts");
        }
    }
}
