using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRegisterationToolSub1.Migrations
{
    /// <inheritdoc />
    public partial class data3datecreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "datacreated",
                table: "PendingSuperUser",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datacreated",
                table: "PendingSuperUser");
        }
    }
}
