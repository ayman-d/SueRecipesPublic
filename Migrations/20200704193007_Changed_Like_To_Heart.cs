using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SueRecipes.Migrations
{
    public partial class Changed_Like_To_Heart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.CreateTable(
                name: "Heart",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    RecipeId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heart", x => new { x.ApplicationUserId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_Heart_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Heart_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Heart_RecipeId",
                table: "Heart",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Heart");

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
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
    }
}
