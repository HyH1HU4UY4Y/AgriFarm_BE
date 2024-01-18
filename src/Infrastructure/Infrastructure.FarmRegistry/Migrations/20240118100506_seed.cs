using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Registration.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PackageSolutions",
                keyColumn: "Id",
                keyValue: new Guid("3c9cca4d-0899-45de-951e-8a3e8364758c"));

            migrationBuilder.DeleteData(
                table: "PackageSolutions",
                keyColumn: "Id",
                keyValue: new Guid("6b9af8b1-8658-4840-b062-3d772658b66b"));

            migrationBuilder.DeleteData(
                table: "PackageSolutions",
                keyColumn: "Id",
                keyValue: new Guid("a8fb146b-ea75-48de-9921-b171b946a82d"));

            
            migrationBuilder.InsertData(
                table: "PackageSolutions",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "DurationInMonth", "IsDeleted", "LastModify", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("4d3cf28e-b1c4-479c-8ccb-3b5539075507"), new DateTime(2024, 1, 18, 17, 5, 6, 810, DateTimeKind.Local).AddTicks(3850), null, "This is cheapest solution", 6L, false, new DateTime(2024, 1, 18, 17, 5, 6, 810, DateTimeKind.Local).AddTicks(3853), "Solution 1", 10m },
                    { new Guid("76929f49-e67b-4037-93ee-6a5f9618bbd3"), new DateTime(2024, 1, 18, 17, 5, 6, 810, DateTimeKind.Local).AddTicks(3863), null, "This is vip solution", 24L, false, new DateTime(2024, 1, 18, 17, 5, 6, 810, DateTimeKind.Local).AddTicks(3864), "Solution 3", 1000m },
                    { new Guid("91502f31-d06d-46d9-8dc1-5a657510ad90"), new DateTime(2024, 1, 18, 17, 5, 6, 810, DateTimeKind.Local).AddTicks(3861), null, "This is medium solution", 12L, false, new DateTime(2024, 1, 18, 17, 5, 6, 810, DateTimeKind.Local).AddTicks(3862), "Solution 2", 100m }
                });

            migrationBuilder.InsertData(
                table: "FarmRegistrations",
                columns: new[] { "Id", "Address", "Cost", "CreatedDate", "DeletedDate", "Email", "FirstName", "IsApprove", "IsDeleted", "LastModify", "LastName", "PaymentDetail", "Phone", "SiteCode", "SiteName", "SolutionId" },
                values: new object[] { new Guid("6bdb6df1-e30a-4572-9308-13bf8be8715d"), "USA", 10m, new DateTime(2024, 1, 18, 17, 5, 6, 809, DateTimeKind.Local).AddTicks(4062), null, "owner01@test.com", "User", 0, false, new DateTime(2024, 1, 18, 17, 5, 6, 809, DateTimeKind.Local).AddTicks(4071), "Owner 01", "test detail", "0132302225", "test.agri.01", "Farm 01 test", new Guid("4d3cf28e-b1c4-479c-8ccb-3b5539075507") });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FarmRegistrations",
                keyColumn: "Id",
                keyValue: new Guid("6bdb6df1-e30a-4572-9308-13bf8be8715d"));

            migrationBuilder.DeleteData(
                table: "PackageSolutions",
                keyColumn: "Id",
                keyValue: new Guid("4d3cf28e-b1c4-479c-8ccb-3b5539075507"));

            migrationBuilder.DeleteData(
                table: "PackageSolutions",
                keyColumn: "Id",
                keyValue: new Guid("76929f49-e67b-4037-93ee-6a5f9618bbd3"));

            migrationBuilder.DeleteData(
                table: "PackageSolutions",
                keyColumn: "Id",
                keyValue: new Guid("91502f31-d06d-46d9-8dc1-5a657510ad90"));

            migrationBuilder.InsertData(
                table: "PackageSolutions",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "DurationInMonth", "IsDeleted", "LastModify", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("3c9cca4d-0899-45de-951e-8a3e8364758c"), new DateTime(2024, 1, 18, 16, 57, 18, 871, DateTimeKind.Local).AddTicks(9281), null, "This is cheapest solution", 6L, false, new DateTime(2024, 1, 18, 16, 57, 18, 871, DateTimeKind.Local).AddTicks(9289), "Solution 1", 10m },
                    { new Guid("6b9af8b1-8658-4840-b062-3d772658b66b"), new DateTime(2024, 1, 18, 16, 57, 18, 871, DateTimeKind.Local).AddTicks(9299), null, "This is medium solution", 12L, false, new DateTime(2024, 1, 18, 16, 57, 18, 871, DateTimeKind.Local).AddTicks(9299), "Solution 2", 100m },
                    { new Guid("a8fb146b-ea75-48de-9921-b171b946a82d"), new DateTime(2024, 1, 18, 16, 57, 18, 871, DateTimeKind.Local).AddTicks(9300), null, "This is vip solution", 24L, false, new DateTime(2024, 1, 18, 16, 57, 18, 871, DateTimeKind.Local).AddTicks(9301), "Solution 3", 1000m }
                });
        }
    }
}
