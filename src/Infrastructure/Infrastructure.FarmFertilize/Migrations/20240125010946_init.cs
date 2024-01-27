using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Fertilize.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FertilizeInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", nullable: true),
                    Details = table.Column<string>(type: "varchar(150)", nullable: true),
                    Notes = table.Column<string>(type: "varchar(150)", nullable: true),
                    Resources = table.Column<string>(type: "varchar(150)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FertilizeInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    SiteCode = table.Column<string>(type: "varchar(150)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    AvatarImg = table.Column<string>(type: "varchar(150)", nullable: true),
                    LogoImg = table.Column<string>(type: "varchar(150)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseComponent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SiteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", nullable: false),
                    IsConsumable = table.Column<bool>(type: "boolean", nullable: false),
                    MeasureUnit = table.Column<string>(name: "Measure Unit", type: "varchar(150)", nullable: false),
                    Notes = table.Column<string>(type: "varchar(150)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseComponent_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FarmFertilizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    InfoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmFertilizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FarmFertilizes_BaseComponent_Id",
                        column: x => x.Id,
                        principalTable: "BaseComponent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarmFertilizes_FertilizeInfos_InfoId",
                        column: x => x.InfoId,
                        principalTable: "FertilizeInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    Require = table.Column<double>(type: "double precision", nullable: false),
                    Unit = table.Column<string>(type: "varchar(150)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_BaseComponent_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "BaseComponent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsedRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdditionType = table.Column<int>(type: "integer", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Instructions = table.Column<string>(type: "varchar(150)", nullable: false),
                    Resources = table.Column<string>(type: "varchar(150)", nullable: false),
                    UseValue = table.Column<double>(type: "double precision", nullable: false),
                    Unit = table.Column<string>(type: "varchar(150)", nullable: false),
                    Notes = table.Column<string>(type: "varchar(150)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsedRecords_BaseComponent_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "BaseComponent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseComponent_SiteId",
                table: "BaseComponent",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmFertilizes_InfoId",
                table: "FarmFertilizes",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ComponentId",
                table: "Properties",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedRecords_ComponentId",
                table: "UsedRecords",
                column: "ComponentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FarmFertilizes");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "UsedRecords");

            migrationBuilder.DropTable(
                name: "FertilizeInfos");

            migrationBuilder.DropTable(
                name: "BaseComponent");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
