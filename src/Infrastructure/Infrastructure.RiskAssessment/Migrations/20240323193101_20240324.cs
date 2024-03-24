using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.RiskAssessment.Migrations
{
    /// <inheritdoc />
    public partial class _20240324 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskMaster",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    RiskDescription = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    IsDraft = table.Column<bool>(type: "boolean", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskMasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskItemTitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    RiskItemDiv = table.Column<int>(type: "integer", nullable: true),
                    RiskItemType = table.Column<int>(type: "integer", nullable: true),
                    RiskItemContent = table.Column<string>(type: "text", maxLength: 2147483647, nullable: true),
                    Must = table.Column<int>(type: "integer", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskItems_RiskMaster_RiskMasterId",
                        column: x => x.RiskMasterId,
                        principalTable: "RiskMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiskMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskMasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskMapping_RiskMaster_RiskMasterId",
                        column: x => x.RiskMasterId,
                        principalTable: "RiskMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiskItemContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskMappingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Anwser = table.Column<string>(type: "text", maxLength: 2147483647, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskItemContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskItemContents_RiskItems_RiskItemId",
                        column: x => x.RiskItemId,
                        principalTable: "RiskItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiskItemContents_RiskMapping_RiskMappingId",
                        column: x => x.RiskMappingId,
                        principalTable: "RiskMapping",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiskItemContents_RiskItemId",
                table: "RiskItemContents",
                column: "RiskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskItemContents_RiskMappingId",
                table: "RiskItemContents",
                column: "RiskMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskItems_RiskMasterId",
                table: "RiskItems",
                column: "RiskMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskMapping_RiskMasterId",
                table: "RiskMapping",
                column: "RiskMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiskItemContents");

            migrationBuilder.DropTable(
                name: "RiskItems");

            migrationBuilder.DropTable(
                name: "RiskMapping");

            migrationBuilder.DropTable(
                name: "RiskMaster");
        }
    }
}
