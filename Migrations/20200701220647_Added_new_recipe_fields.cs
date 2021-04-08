using Microsoft.EntityFrameworkCore.Migrations;

namespace SueRecipes.Migrations
{
    public partial class Added_new_recipe_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Recipe",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CookTime",
                table: "Recipe",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MealType",
                table: "Recipe",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrepTime",
                table: "Recipe",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Servings",
                table: "Recipe",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpiceLevel",
                table: "Recipe",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "CookTime",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "MealType",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "PrepTime",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "Servings",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "SpiceLevel",
                table: "Recipe");
        }
    }
}
