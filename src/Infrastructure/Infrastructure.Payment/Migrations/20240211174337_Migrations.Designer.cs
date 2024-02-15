﻿// <auto-generated />
using System;
using Infrastructure.Payment.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Payment.Migrations
{
    [DbContext(typeof(PaymentContext))]
    [Migration("20240211174337_Migrations")]
    partial class Migrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SharedDomain.Entities.Pay.Merchant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("LastUpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("MerchantIpnUrl")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("MerchantName")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("MerchantReturnUrl")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("MerchantWebLink")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("SecretKey")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.ToTable("Merchants");
                });

            modelBuilder.Entity("SharedDomain.Entities.Pay.PaymentDestination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DesLogo")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("DesName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("DesParentId")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("DesShortName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("SortIndex")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("PaymentDestinations");
                });

            modelBuilder.Entity("SharedDomain.Entities.Pay.PaymentNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("NotiAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("NotiContent")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid?>("NotiMerchantId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("NotiPaymentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("NotiResDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("NotiResHttpCode")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("NotiResMessage")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("NotiSignature")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("NotiStatus")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid?>("PaymentRefId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PaymenttId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PaymenttId");

                    b.ToTable("PaymentNotifications");
                });

            modelBuilder.Entity("SharedDomain.Entities.Pay.PaymentSignature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsValid")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("PaymentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PaymenttId")
                        .HasColumnType("uuid");

                    b.Property<string>("SignAlgo")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<DateTime?>("SignDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SignOwn")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("SignValue")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.HasIndex("PaymenttId");

                    b.ToTable("PaymentSignatures");
                });

            modelBuilder.Entity("SharedDomain.Entities.Pay.PaymentTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("PaymentId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("TranAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("TranDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("TranMessage")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("TranPayload")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid?>("TranRefId")
                        .HasColumnType("uuid");

                    b.Property<string>("TranStatus")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.ToTable("PaymentTransactions");
                });

            modelBuilder.Entity("SharedDomain.Entities.Pay.Paymentt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("ExpireDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("MerchantId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("PaidAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("PaymentContent")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("PaymentCurrency")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("PaymentDestinationId")
                        .HasColumnType("uuid");

                    b.Property<string>("PaymentLanguage")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("PaymentLastMessage")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid>("PaymentRefId")
                        .HasColumnType("uuid");

                    b.Property<string>("PaymentStatus")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<decimal?>("RequiredAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("MerchantId");

                    b.HasIndex("PaymentDestinationId");

                    b.ToTable("Paymentts");
                });

            modelBuilder.Entity("SharedDomain.Entities.Pay.PaymentNotification", b =>
                {
                    b.HasOne("SharedDomain.Entities.Pay.Paymentt", "Paymentt")
                        .WithMany()
                        .HasForeignKey("PaymenttId")
                        .IsRequired();

                    b.Navigation("Paymentt");
                });

            modelBuilder.Entity("SharedDomain.Entities.Pay.PaymentSignature", b =>
                {
                    b.HasOne("SharedDomain.Entities.Pay.Paymentt", "Paymentt")
                        .WithMany()
                        .HasForeignKey("PaymenttId");

                    b.Navigation("Paymentt");
                });

            modelBuilder.Entity("SharedDomain.Entities.Pay.PaymentTransaction", b =>
                {
                    b.HasOne("SharedDomain.Entities.Pay.Paymentt", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("SharedDomain.Entities.Pay.Paymentt", b =>
                {
                    b.HasOne("SharedDomain.Entities.Pay.Merchant", "Merchant")
                        .WithMany()
                        .HasForeignKey("MerchantId");

                    b.HasOne("SharedDomain.Entities.Pay.PaymentDestination", "PaymentDestination")
                        .WithMany()
                        .HasForeignKey("PaymentDestinationId");

                    b.Navigation("Merchant");

                    b.Navigation("PaymentDestination");
                });
#pragma warning restore 612, 618
        }
    }
}
