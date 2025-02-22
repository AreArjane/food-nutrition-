﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodRegisterationToolSub1.Migrations
{
    /// <inheritdoc />
    public partial class data3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodCategories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Nutrient",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    unit_name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    nutrient_nbr = table.Column<string>(type: "text", nullable: true),
                    rank = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrient", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    PhoneNr = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    UserType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    fdc_id = table.Column<int>(type: "integer", nullable: false),
                    data_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    food_category_id = table.Column<int>(type: "integer", nullable: true),
                    publication_date = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.fdc_id);
                    table.ForeignKey(
                        name: "FK_Food_FoodCategories_food_category_id",
                        column: x => x.food_category_id,
                        principalTable: "FoodCategories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    NationalIdenityNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    OfficeAddress = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    WorkPhoneNr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AdminUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    meal_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.meal_id);
                    table.ForeignKey(
                        name: "FK_Meal_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NormalUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    HomeAddress = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    PostalCode = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NormalUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_NormalUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PendingSuperUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DateOfBirth = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    isApproved = table.Column<bool>(type: "boolean", nullable: false),
                    verificationcode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingSuperUser", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_PendingSuperUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PermissionType = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_Permissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "SuperUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DateOfBirth = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_SuperUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodNutrient",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    fdc_id = table.Column<int>(type: "integer", nullable: false),
                    nutrient_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: true),
                    data_points = table.Column<string>(type: "text", nullable: true),
                    derivation_id = table.Column<string>(type: "text", nullable: true),
                    min = table.Column<string>(type: "text", nullable: true),
                    max = table.Column<string>(type: "text", nullable: true),
                    median = table.Column<string>(type: "text", nullable: true),
                    footnote = table.Column<string>(type: "character varying(2555)", maxLength: 2555, nullable: true),
                    min_year_acquired = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodNutrient", x => x.id);
                    table.ForeignKey(
                        name: "FK_FoodNutrient_Food_fdc_id",
                        column: x => x.fdc_id,
                        principalTable: "Food",
                        principalColumn: "fdc_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodNutrient_Nutrient_nutrient_id",
                        column: x => x.nutrient_id,
                        principalTable: "Nutrient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealFood",
                columns: table => new
                {
                    meal_food_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    food_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<double>(type: "double precision", nullable: false),
                    MealId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealFood", x => x.meal_food_id);
                    table.ForeignKey(
                        name: "FK_MealFood_Food_food_id",
                        column: x => x.food_id,
                        principalTable: "Food",
                        principalColumn: "fdc_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealFood_Meal_MealId",
                        column: x => x.MealId,
                        principalTable: "Meal",
                        principalColumn: "meal_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_food_category_id",
                table: "Food",
                column: "food_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_FoodNutrient_fdc_id",
                table: "FoodNutrient",
                column: "fdc_id");

            migrationBuilder.CreateIndex(
                name: "IX_FoodNutrient_nutrient_id",
                table: "FoodNutrient",
                column: "nutrient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_user_id",
                table: "Meal",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_MealFood_food_id",
                table: "MealFood",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_MealFood_MealId",
                table: "MealFood",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserId",
                table: "Permissions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "FoodNutrient");

            migrationBuilder.DropTable(
                name: "MealFood");

            migrationBuilder.DropTable(
                name: "NormalUsers");

            migrationBuilder.DropTable(
                name: "PendingSuperUser");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "SuperUsers");

            migrationBuilder.DropTable(
                name: "Nutrient");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "Meal");

            migrationBuilder.DropTable(
                name: "FoodCategories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
