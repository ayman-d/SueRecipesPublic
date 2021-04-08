using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SueRecipes.Migrations
{
    public partial class Added_Like_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    RecipeId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => new { x.ApplicationUserId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_Like_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_RecipeId",
                table: "Like",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Like");
        }
    }
}
