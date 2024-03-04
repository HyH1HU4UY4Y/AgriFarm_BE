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
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
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
                name: "SiteInfos",
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
                    table.PrimaryKey("PK_SiteInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarmRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    LastName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Phone = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Address = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    SiteCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SiteName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    SolutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentDetail = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
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

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SiteId = table.Column<Guid>(type: "uuid", nullable: true),
                    FullName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    UserName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    AvatarImg = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfos_SiteInfos_SiteId",
                        column: x => x.SiteId,
                        principalTable: "SiteInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "PackageSolutions",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "DurationInMonth", "IsDeleted", "LastModify", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("a9f2a93d-987a-446c-b5c4-ae3dcb16cd29"), new DateTime(2024, 3, 3, 16, 11, 49, 283, DateTimeKind.Local).AddTicks(5185), null, "This is medium solution", 12L, false, new DateTime(2024, 3, 3, 16, 11, 49, 283, DateTimeKind.Local).AddTicks(5187), "Solution 2", 100m },
                    { new Guid("b24b90ea-4d1e-434a-a22d-9f8df08cfcd5"), new DateTime(2024, 3, 3, 16, 11, 49, 283, DateTimeKind.Local).AddTicks(5189), null, "This is vip solution", 24L, false, new DateTime(2024, 3, 3, 16, 11, 49, 283, DateTimeKind.Local).AddTicks(5190), "Solution 3", 1000m },
                    { new Guid("e43d372f-1ad5-46bd-b950-a95419211c0e"), new DateTime(2024, 3, 3, 16, 11, 49, 283, DateTimeKind.Local).AddTicks(5163), null, "This is cheapest solution", 6L, false, new DateTime(2024, 3, 3, 16, 11, 49, 283, DateTimeKind.Local).AddTicks(5169), "Solution 1", 10m }
                });

            migrationBuilder.InsertData(
                table: "FarmRegistrations",
                columns: new[] { "Id", "Address", "Cost", "CreatedDate", "DeletedDate", "Email", "FirstName", "IsApprove", "IsDeleted", "LastModify", "LastName", "PaymentDetail", "Phone", "SiteCode", "SiteName", "SolutionId" },
                values: new object[] { new Guid("28e01676-47ff-4ae4-b6da-670d42cc3c73"), "USA", 10m, new DateTime(2024, 3, 3, 16, 11, 49, 282, DateTimeKind.Local).AddTicks(684), null, "owner01@test.com", "User", 0, false, new DateTime(2024, 3, 3, 16, 11, 49, 282, DateTimeKind.Local).AddTicks(698), "Owner 01", "test detail", "0132302225", "test.agri.01", "Farm 01 test", new Guid("e43d372f-1ad5-46bd-b950-a95419211c0e") });

            migrationBuilder.CreateIndex(
                name: "IX_FarmRegistrations_SolutionId",
                table: "FarmRegistrations",
                column: "SolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_SiteId",
                table: "UserInfos",
                column: "SiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FarmRegistrations");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "PackageSolutions");

            migrationBuilder.DropTable(
                name: "SiteInfos");
        }
    }
}
