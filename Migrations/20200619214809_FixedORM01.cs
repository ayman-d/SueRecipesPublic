using Microsoft.EntityFrameworkCore.Migrations;

namespace SueRecipes.Migrations
{
    public partial class FixedORM01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeTag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RecipeTag",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
