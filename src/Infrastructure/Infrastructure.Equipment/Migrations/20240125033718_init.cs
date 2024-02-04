using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Equipment.Migrations
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
                    Discriminator = table.Column<string>(type: "varchar(150)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: true),
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
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Data = table.Column<string>(type: "varchar(150)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_BaseComponent_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "BaseComponent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseComponent_SiteId",
                table: "BaseComponent",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ComponentId",
                table: "Properties",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_States_ComponentId",
                table: "States",
                column: "ComponentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "BaseComponent");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
