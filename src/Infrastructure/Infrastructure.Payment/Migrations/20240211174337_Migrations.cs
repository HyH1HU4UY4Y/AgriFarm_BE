using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Payment.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MerchantName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    MerchantWebLink = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    MerchantIpnUrl = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    MerchantReturnUrl = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    SecretKey = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDestinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DesName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DesShortName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DesParentId = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DesLogo = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    SortIndex = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDestinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paymentts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentContent = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    PaymentCurrency = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    PaymentRefId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequiredAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentLanguage = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: true),
                    PaymentDestinationId = table.Column<Guid>(type: "uuid", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    PaymentStatus = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    PaymentLastMessage = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paymentts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paymentts_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Paymentts_PaymentDestinations_PaymentDestinationId",
                        column: x => x.PaymentDestinationId,
                        principalTable: "PaymentDestinations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentRefId = table.Column<Guid>(type: "uuid", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NotiContent = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    NotiAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    NotiSignature = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    NotiPaymentId = table.Column<Guid>(type: "uuid", nullable: true),
                    PaymenttId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotiMerchantId = table.Column<Guid>(type: "uuid", nullable: true),
                    NotiStatus = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    NotiResDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NotiResMessage = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    NotiResHttpCode = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentNotifications_Paymentts_PaymenttId",
                        column: x => x.PaymenttId,
                        principalTable: "Paymentts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentSignatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: true),
                    PaymenttId = table.Column<Guid>(type: "uuid", nullable: true),
                    SignValue = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    SignAlgo = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    SignOwn = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    SignDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsValid = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSignatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentSignatures_Paymentts_PaymenttId",
                        column: x => x.PaymenttId,
                        principalTable: "Paymentts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TranMessage = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    TranPayload = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    TranStatus = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    TranAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    TranDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: true),
                    TranRefId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModify = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Paymentts_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Paymentts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentNotifications_PaymenttId",
                table: "PaymentNotifications",
                column: "PaymenttId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSignatures_PaymenttId",
                table: "PaymentSignatures",
                column: "PaymenttId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PaymentId",
                table: "PaymentTransactions",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Paymentts_MerchantId",
                table: "Paymentts",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Paymentts_PaymentDestinationId",
                table: "Paymentts",
                column: "PaymentDestinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentNotifications");

            migrationBuilder.DropTable(
                name: "PaymentSignatures");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "Paymentts");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "PaymentDestinations");
        }
    }
}
