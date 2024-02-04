using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.RiskAssessment.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskMasters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskName = table.Column<string>(type: "varchar(150)", nullable: true),
                    RiskDescription = table.Column<string>(type: "varchar(150)", maxLength: 8000, nullable: true),
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
                    table.PrimaryKey("PK_RiskMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskMasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskItemTitle = table.Column<string>(type: "varchar(150)", nullable: true),
                    RiskItemDiv = table.Column<string>(type: "varchar(150)", nullable: true),
                    RiskItemType = table.Column<string>(type: "varchar(150)", nullable: true),
                    Must = table.Column<string>(type: "varchar(150)", nullable: true),
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
                        name: "FK_RiskItems_RiskMasters_RiskMasterId",
                        column: x => x.RiskMasterId,
                        principalTable: "RiskMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiskItemContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskItemContentTitle = table.Column<string>(type: "varchar(150)", nullable: true),
                    OrderBy = table.Column<int>(type: "integer", nullable: true),
                    Anwser = table.Column<string>(type: "varchar(150)", maxLength: 8000, nullable: true),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uuid", nullable: true),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiskItemContents_RiskItemId",
                table: "RiskItemContents",
                column: "RiskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskItems_RiskMasterId",
                table: "RiskItems",
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
                name: "RiskMasters");
        }
    }
}
