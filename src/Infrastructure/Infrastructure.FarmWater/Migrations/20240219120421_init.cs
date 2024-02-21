using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Water.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    SiteCode = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    FromSource = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Acreage = table.Column<double>(type: "double precision", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SiteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: true),
                    IsConsumable = table.Column<bool>(type: "boolean", nullable: false),
                    MeasureUnit = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: true),
                    Resource = table.Column<string>(type: "text", maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaterSources_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    Require = table.Column<double>(type: "double precision", nullable: false),
                    Unit = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    FarmWaterId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_WaterSources_FarmWaterId",
                        column: x => x.FarmWaterId,
                        principalTable: "WaterSources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Data = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    FarmWaterId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_WaterSources_FarmWaterId",
                        column: x => x.FarmWaterId,
                        principalTable: "WaterSources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_FarmWaterId",
                table: "Properties",
                column: "FarmWaterId");

            migrationBuilder.CreateIndex(
                name: "IX_States_FarmWaterId",
                table: "States",
                column: "FarmWaterId");

            migrationBuilder.CreateIndex(
                name: "IX_WaterSources_SiteId",
                table: "WaterSources",
                column: "SiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "WaterSources");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
