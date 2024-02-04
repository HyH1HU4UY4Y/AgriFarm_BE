using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Registration.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackageSolutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: true),
                    Description = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DurationHour = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModify = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageSolutions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarmRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    Content = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: true),
                    Phone = table.Column<string>(type: "varchar(150)", nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false),
                    Address = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: true),
                    SiteKey = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    SiteName = table.Column<string>(type: "varchar(150)", nullable: false),
                    SolutionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDetail = table.Column<string>(type: "varchar(150)", nullable: true),
                    IsApprove = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModify = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FarmRegistrations_PackageSolutions_SolutionId",
                        column: x => x.SolutionId,
                        principalTable: "PackageSolutions",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "PackageSolutions",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "DurationHour", "IsDeleted", "LastModify", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("2feec9a3-1685-4220-80f3-435dcce6a99a"), new DateTime(2024, 1, 12, 5, 50, 22, 591, DateTimeKind.Local).AddTicks(5508), null, "This is vip solution", 79000L, false, new DateTime(2024, 1, 12, 5, 50, 22, 591, DateTimeKind.Local).AddTicks(5509), "Solution 3", 1000m },
                    { new Guid("c5e6a8eb-ac87-453d-a8e5-098cd119ec04"), new DateTime(2024, 1, 12, 5, 50, 22, 591, DateTimeKind.Local).AddTicks(5505), null, "This is medium solution", 7800L, false, new DateTime(2024, 1, 12, 5, 50, 22, 591, DateTimeKind.Local).AddTicks(5506), "Solution 2", 100m },
                    { new Guid("e89cf903-ef59-4098-a0aa-17869be91538"), new DateTime(2024, 1, 12, 5, 50, 22, 591, DateTimeKind.Local).AddTicks(5420), null, "This is cheapest solution", 750L, false, new DateTime(2024, 1, 12, 5, 50, 22, 591, DateTimeKind.Local).AddTicks(5429), "Solution 1", 10m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FarmRegistrations_SolutionId",
                table: "FarmRegistrations",
                column: "SolutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FarmRegistrations");

            migrationBuilder.DropTable(
                name: "PackageSolutions");
        }
    }
}
