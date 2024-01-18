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
                    DurationInMonth = table.Column<long>(type: "bigint", nullable: true),
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
                    FirstName = table.Column<string>(type: "varchar(150)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(150)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(150)", nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false),
                    Address = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: true),
                    SiteCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
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
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "DurationInMonth", "IsDeleted", "LastModify", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("3c9cca4d-0899-45de-951e-8a3e8364758c"), new DateTime(2024, 1, 18, 16, 57, 18, 871, DateTimeKind.Local).AddTicks(9281), null, "This is cheapest solution", 6L, false, new DateTime(2024, 1, 18, 16, 57, 18, 871, DateTimeKind.Local).AddTicks(9289), "Solution 1", 10m },
                    { new Guid("6b9af8b1-8658-4840-b062-3d772658b66b"), new DateTime(2024, 1, 18, 16, 57, 18, 871, DateTimeKind.Local).AddTicks(9299), null, "This is medium solution", 12L, false, new DateTime(2024, 1, 18, 16, 57, 18, 871, DateTimeKind.Local).AddTicks(9299), "Solution 2", 100m },
                    { new Guid("a8fb146b-ea75-48de-9921-b171b946a82d"), new DateTime(2024, 1, 18, 16, 57, 18, 871, DateTimeKind.Local).AddTicks(9300), null, "This is vip solution", 24L, false, new DateTime(2024, 1, 18, 16, 57, 18, 871, DateTimeKind.Local).AddTicks(9301), "Solution 3", 1000m }
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
