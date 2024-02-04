using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Disease.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiseaseInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiseaseName = table.Column<string>(type: "varchar(150)", nullable: false),
                    Symptoms = table.Column<string>(type: "varchar(150)", maxLength: 8000, nullable: false),
                    Cause = table.Column<string>(type: "varchar(150)", maxLength: 8000, nullable: false),
                    PreventiveMeasures = table.Column<string>(type: "varchar(150)", nullable: false),
                    Suggest = table.Column<string>(type: "varchar(150)", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseDiagnoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantDiseaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", nullable: true),
                    Feedback = table.Column<string>(type: "varchar(150)", nullable: true),
                    FeedbackStatus = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<string>(type: "varchar(150)", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LandId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseDiagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiseaseDiagnoses_DiseaseInfos_PlantDiseaseId",
                        column: x => x.PlantDiseaseId,
                        principalTable: "DiseaseInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseDiagnoses_PlantDiseaseId",
                table: "DiseaseDiagnoses",
                column: "PlantDiseaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiseaseDiagnoses");

            migrationBuilder.DropTable(
                name: "DiseaseInfos");
        }
    }
}
