﻿// <auto-generated />
using System;
using Infrastructure.Water.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Water.Migrations
{
    [DbContext(typeof(FarmWaterContext))]
    [Migration("20240219120421_init")]
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

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.ComponentProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ComponentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("FarmWaterId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<double>("Require")
                        .HasColumnType("double precision");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("FarmWaterId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.ComponentState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ActivityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ComponentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("FarmWaterId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("FarmWaterId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmWater", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double?>("Acreage")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<string>("FromSource")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

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

                    b.Property<string>("Position")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

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

                    b.ToTable("WaterSources", (string)null);
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

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.ComponentProperty", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.FarmWater", null)
                        .WithMany("Properties")
                        .HasForeignKey("FarmWaterId");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.ComponentState", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.FarmWater", null)
                        .WithMany("States")
                        .HasForeignKey("FarmWaterId");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmWater", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmWater", b =>
                {
                    b.Navigation("Properties");

                    b.Navigation("States");
                });
#pragma warning restore 612, 618
        }
    }
}
