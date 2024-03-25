using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.FarmCultivation.Migrations
{
    /// <inheritdoc />
    public partial class update_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "SeedInfos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Resource",
                table: "SeedInfos",
                type: "text",
                maxLength: 2147483647,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Locations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Resource",
                table: "Locations",
                type: "text",
                maxLength: 2147483647,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "BaseComponent",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Resource",
                table: "BaseComponent",
                type: "text",
                maxLength: 2147483647,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "SeedInfos");

            migrationBuilder.DropColumn(
                name: "Resource",
                table: "SeedInfos");

            migrationBuilder.DropColumn(
                name: "Available",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Resource",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Available",
                table: "BaseComponent");

            migrationBuilder.DropColumn(
                name: "Resource",
                table: "BaseComponent");
        }
    }
}
