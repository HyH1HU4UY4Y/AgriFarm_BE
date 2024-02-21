﻿// <auto-generated />
using System;
using Infrastructure.FarmCultivation.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.FarmCultivation.Migrations
{
    [DbContext(typeof(CultivationContext))]
    [Migration("20240221030837_init")]
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

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmSeed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<bool>("IsConsumable")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<string>("Resource")
                        .HasMaxLength(2147483647)
                        .HasColumnType("text");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uuid");

                    b.Property<string>("Unit")
                        .HasColumnType("text")
                        .HasColumnName("MeasureUnit");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("SeedInfos", (string)null);
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmSoil", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<bool>("IsConsumable")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<string>("Resource")
                        .HasMaxLength(2147483647)
                        .HasColumnType("text");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uuid");

                    b.Property<string>("Unit")
                        .HasColumnType("text")
                        .HasColumnName("MeasureUnit");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("Locations", (string)null);
                });

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

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("SharedDomain.Entities.Schedules.CultivationSeason", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<DateTime>("EndIn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartIn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("SharedDomain.Entities.Schedules.Cultivations.HarvestProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("HarvestTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LandId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<double?>("Quantity")
                        .HasColumnType("double precision");

                    b.Property<Guid>("SeasonId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SeedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uuid");

                    b.Property<double?>("TotalQuantity")
                        .HasColumnType("double precision");

                    b.Property<string>("Traceability")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.HasIndex("LandId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("SeedId");

                    b.HasIndex("SiteId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmSeed", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmSoil", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("SharedDomain.Entities.Schedules.CultivationSeason", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId")
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("SharedDomain.Entities.Schedules.Cultivations.HarvestProduct", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.FarmSoil", "Land")
                        .WithMany()
                        .HasForeignKey("LandId")
                        .IsRequired();

                    b.HasOne("SharedDomain.Entities.Schedules.CultivationSeason", "Season")
                        .WithMany("Products")
                        .HasForeignKey("SeasonId")
                        .IsRequired();

                    b.HasOne("SharedDomain.Entities.FarmComponents.FarmSeed", "Seed")
                        .WithMany()
                        .HasForeignKey("SeedId")
                        .IsRequired();

                    b.HasOne("SharedDomain.Entities.FarmComponents.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId")
                        .IsRequired();

                    b.Navigation("Land");

                    b.Navigation("Season");

                    b.Navigation("Seed");

                    b.Navigation("Site");
                });

            modelBuilder.Entity("SharedDomain.Entities.Schedules.CultivationSeason", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}