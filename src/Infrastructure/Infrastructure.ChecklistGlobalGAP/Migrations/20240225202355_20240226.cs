using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.ChecklistGlobalGAP.Migrations
{
    /// <inheritdoc />
    public partial class _20240226 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChecklistMasters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChecklistMasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderNo = table.Column<int>(type: "integer", nullable: false),
                    AfNum = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    LevelRoute = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Content = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    IsResponse = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistItems_ChecklistMasters_ChecklistMasterId",
                        column: x => x.ChecklistMasterId,
                        principalTable: "ChecklistMasters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChecklistMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChecklistMasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistMapping_ChecklistMasters_ChecklistMasterId",
                        column: x => x.ChecklistMasterId,
                        principalTable: "ChecklistMasters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItemResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChecklistItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChecklistMappingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Attachment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItemResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistItemResponses_ChecklistItems_ChecklistItemId",
                        column: x => x.ChecklistItemId,
                        principalTable: "ChecklistItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChecklistItemResponses_ChecklistMapping_ChecklistMappingId",
                        column: x => x.ChecklistMappingId,
                        principalTable: "ChecklistMapping",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItemResponses_ChecklistItemId",
                table: "ChecklistItemResponses",
                column: "ChecklistItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItemResponses_ChecklistMappingId",
                table: "ChecklistItemResponses",
                column: "ChecklistMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_ChecklistMasterId",
                table: "ChecklistItems",
                column: "ChecklistMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistMapping_ChecklistMasterId",
                table: "ChecklistMapping",
                column: "ChecklistMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistItemResponses");

            migrationBuilder.DropTable(
                name: "ChecklistItems");

            migrationBuilder.DropTable(
                name: "ChecklistMapping");

            migrationBuilder.DropTable(
                name: "ChecklistMasters");
        }
    }
}
