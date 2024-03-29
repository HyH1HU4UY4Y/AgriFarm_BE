﻿// <auto-generated />
using System;
using Infrastructure.FarmRegistry.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Registration.Migrations
{
    [DbContext(typeof(RegistrationContext))]
    [Migration("20240303091149_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Site", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("SiteCode")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.ToTable("SiteInfos");
                });

            modelBuilder.Entity("SharedDomain.Entities.Subscribe.FarmRegistration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int?>("IsApprove")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("PaymentDetail")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Phone")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("SiteCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("SiteName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid>("SolutionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SolutionId");

                    b.ToTable("FarmRegistrations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("28e01676-47ff-4ae4-b6da-670d42cc3c73"),
                            Address = "USA",
                            Cost = 10m,
                            CreatedDate = new DateTime(2024, 3, 3, 16, 11, 49, 282, DateTimeKind.Local).AddTicks(684),
                            Email = "owner01@test.com",
                            FirstName = "User",
                            IsApprove = 0,
                            IsDeleted = false,
                            LastModify = new DateTime(2024, 3, 3, 16, 11, 49, 282, DateTimeKind.Local).AddTicks(698),
                            LastName = "Owner 01",
                            PaymentDetail = "test detail",
                            Phone = "0132302225",
                            SiteCode = "test.agri.01",
                            SiteName = "Farm 01 test",
                            SolutionId = new Guid("3c9cca4d-0899-45de-951e-8a3e8364758c")
                        });
                });

            modelBuilder.Entity("SharedDomain.Entities.Subscribe.PackageSolution", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<long?>("DurationInMonth")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("PackageSolutions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e43d372f-1ad5-46bd-b950-a95419211c0e"),
                            CreatedDate = new DateTime(2024, 3, 3, 16, 11, 49, 283, DateTimeKind.Local).AddTicks(5163),
                            Description = "This is cheapest solution",
                            DurationInMonth = 6L,
                            IsDeleted = false,
                            LastModify = new DateTime(2024, 3, 3, 16, 11, 49, 283, DateTimeKind.Local).AddTicks(5169),
                            Name = "Solution 1",
                            Price = 10m
                        },
                        new
                        {
                            Id = new Guid("a9f2a93d-987a-446c-b5c4-ae3dcb16cd29"),
                            CreatedDate = new DateTime(2024, 3, 3, 16, 11, 49, 283, DateTimeKind.Local).AddTicks(5185),
                            Description = "This is medium solution",
                            DurationInMonth = 12L,
                            IsDeleted = false,
                            LastModify = new DateTime(2024, 3, 3, 16, 11, 49, 283, DateTimeKind.Local).AddTicks(5187),
                            Name = "Solution 2",
                            Price = 100m
                        },
                        new
                        {
                            Id = new Guid("b24b90ea-4d1e-434a-a22d-9f8df08cfcd5"),
                            CreatedDate = new DateTime(2024, 3, 3, 16, 11, 49, 283, DateTimeKind.Local).AddTicks(5189),
                            Description = "This is vip solution",
                            DurationInMonth = 24L,
                            IsDeleted = false,
                            LastModify = new DateTime(2024, 3, 3, 16, 11, 49, 283, DateTimeKind.Local).AddTicks(5190),
                            Name = "Solution 3",
                            Price = 1000m
                        });
                });

            modelBuilder.Entity("SharedDomain.Entities.Users.MinimalUserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarImg")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FullName")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("SiteId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("SharedDomain.Entities.Subscribe.FarmRegistration", b =>
                {
                    b.HasOne("SharedDomain.Entities.Subscribe.PackageSolution", "Solution")
                        .WithMany()
                        .HasForeignKey("SolutionId")
                        .IsRequired();

                    b.Navigation("Solution");
                });

            modelBuilder.Entity("SharedDomain.Entities.Users.MinimalUserInfo", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId");

                    b.Navigation("Site");
                });
#pragma warning restore 612, 618
        }
    }
}
