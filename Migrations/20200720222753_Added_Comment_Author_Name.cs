using Microsoft.EntityFrameworkCore.Migrations;

namespace SueRecipes.Migrations
{
    public partial class Added_Comment_Author_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserName",
                table: "Comment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserName",
                table: "Comment");
        }
    }
}
