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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: true),
                    Description = table.Column<string>(type: "varchar(150)", maxLength: 5000, nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    DurationInMonth = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageSolutions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarmRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(150)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(150)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(150)", nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false),
                    Address = table.Column<string>(type: "varchar(150)", maxLength: 5000, nullable: true),
                    SiteCode = table.Column<string>(type: "varchar(150)", maxLength: 20, nullable: false),
                    SiteName = table.Column<string>(type: "varchar(150)", nullable: false),
                    SolutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentDetail = table.Column<string>(type: "varchar(150)", nullable: true),
                    IsApprove = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                    { new Guid("28eb1cab-70d3-4282-917d-feafb145a5e9"), new DateTime(2024, 1, 20, 15, 4, 25, 887, DateTimeKind.Local).AddTicks(1432), null, "This is vip solution", 24L, false, new DateTime(2024, 1, 20, 15, 4, 25, 887, DateTimeKind.Local).AddTicks(1433), "Solution 3", 1000m },
                    { new Guid("45aa6629-5e67-4c70-aa9c-eed4e82e7da6"), new DateTime(2024, 1, 20, 15, 4, 25, 887, DateTimeKind.Local).AddTicks(1405), null, "This is cheapest solution", 6L, false, new DateTime(2024, 1, 20, 15, 4, 25, 887, DateTimeKind.Local).AddTicks(1410), "Solution 1", 10m },
                    { new Guid("af09cd31-97fd-466b-bb4d-7666d953724a"), new DateTime(2024, 1, 20, 15, 4, 25, 887, DateTimeKind.Local).AddTicks(1430), null, "This is medium solution", 12L, false, new DateTime(2024, 1, 20, 15, 4, 25, 887, DateTimeKind.Local).AddTicks(1430), "Solution 2", 100m }
                });

            migrationBuilder.InsertData(
                table: "FarmRegistrations",
                columns: new[] { "Id", "Address", "Cost", "CreatedDate", "DeletedDate", "Email", "FirstName", "IsApprove", "IsDeleted", "LastModify", "LastName", "PaymentDetail", "Phone", "SiteCode", "SiteName", "SolutionId" },
                values: new object[] { new Guid("02f16f00-4d51-45c6-adea-f53638bad2ba"), "USA", 10m, new DateTime(2024, 1, 20, 15, 4, 25, 886, DateTimeKind.Local).AddTicks(2719), null, "owner01@test.com", "User", 0, false, new DateTime(2024, 1, 20, 15, 4, 25, 886, DateTimeKind.Local).AddTicks(2727), "Owner 01", "test detail", "0132302225", "test.agri.01", "Farm 01 test", new Guid("45aa6629-5e67-4c70-aa9c-eed4e82e7da6") });

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
