using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodRegisterationToolSub1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodCategories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "NormalUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastName = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    HomeAddress = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    PostalCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    PhoneNr = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NormalUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Nutrient",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UnitName = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    nutrient_nbr = table.Column<int>(type: "integer", nullable: false),
                    rank = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrient", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SuperUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOfBirth = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    PhoneNr = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperUser", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    fdc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    data_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    food_category_id = table.Column<int>(type: "integer", nullable: false),
                    publication_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.fdc_id);
                    table.ForeignKey(
                        name: "FK_Food_FoodCategories_food_category_id",
                        column: x => x.food_category_id,
                        principalTable: "FoodCategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PermissionType = table.Column<int>(type: "integer", nullable: false),
                    NormalUserUserId = table.Column<int>(type: "integer", nullable: true),
                    SuperUserUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_Permissions_NormalUsers_NormalUserUserId",
                        column: x => x.NormalUserUserId,
                        principalTable: "NormalUsers",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Permissions_SuperUser_SuperUserUserId",
                        column: x => x.SuperUserUserId,
                        principalTable: "SuperUser",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "FoodNutrient",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fdc_id = table.Column<int>(type: "integer", nullable: false),
                    data_points = table.Column<int>(type: "integer", nullable: false),
                    derivation_id = table.Column<int>(type: "integer", nullable: false),
                    min = table.Column<float>(type: "real", nullable: true),
                    max = table.Column<float>(type: "real", nullable: true),
                    median = table.Column<float>(type: "real", nullable: true),
                    footnote = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    min_year_acquired = table.Column<int>(type: "integer", nullable: true),
                    ntrient_id = table.Column<int>(type: "integer", nullable: false)
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
                        name: "FK_FoodNutrient_Nutrient_ntrient_id",
                        column: x => x.ntrient_id,
                        principalTable: "Nutrient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_FoodNutrient_ntrient_id",
                table: "FoodNutrient",
                column: "ntrient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_NormalUserUserId",
                table: "Permissions",
                column: "NormalUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_SuperUserUserId",
                table: "Permissions",
                column: "SuperUserUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodNutrient");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "Nutrient");

            migrationBuilder.DropTable(
                name: "NormalUsers");

            migrationBuilder.DropTable(
                name: "SuperUser");

            migrationBuilder.DropTable(
                name: "FoodCategories");
        }
    }
}
