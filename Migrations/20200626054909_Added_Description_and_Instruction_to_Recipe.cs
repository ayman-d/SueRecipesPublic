using Microsoft.EntityFrameworkCore.Migrations;

namespace SueRecipes.Migrations
{
    public partial class Added_Description_and_Instruction_to_Recipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Recipe",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "Recipe",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "Recipe");
        }
    }
}
