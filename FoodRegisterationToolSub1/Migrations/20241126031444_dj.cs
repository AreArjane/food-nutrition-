using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRegisterationToolSub1.Migrations
{
    /// <inheritdoc />
    public partial class dj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_FoodCategories_food_category_id",
                table: "Food");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_FoodCategories_food_category_id",
                table: "Food",
                column: "food_category_id",
                principalTable: "FoodCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_FoodCategories_food_category_id",
                table: "Food");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_FoodCategories_food_category_id",
                table: "Food",
                column: "food_category_id",
                principalTable: "FoodCategories",
                principalColumn: "id");
        }
    }
}
