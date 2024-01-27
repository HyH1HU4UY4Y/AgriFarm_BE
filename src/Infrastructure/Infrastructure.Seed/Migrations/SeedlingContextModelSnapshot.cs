﻿// <auto-generated />
using System;
using Infrastructure.Seed.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Seed.Migrations
{
    [DbContext(typeof(SeedlingContext))]
    partial class SeedlingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.BaseComponent", b =>
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
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("IsConsumable")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uuid");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasColumnName("Measure Unit");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("BaseComponent");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Common.SeedInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Details")
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Notes")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Resources")
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("SeedInfos", (string)null);
                });

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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<double>("Require")
                        .HasColumnType("double precision");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Site", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarImg")
                        .HasColumnType("varchar(150)");

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

                    b.Property<string>("LogoImg")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("SiteCode")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("SharedDomain.Entities.Schedules.AdditionOfActivity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActivityId")
                        .HasColumnType("uuid");

                    b.Property<int>("AdditionType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmSeed", b =>
                {
                    b.HasBaseType("SharedDomain.Entities.FarmComponents.BaseComponent");

                    b.Property<Guid?>("SeedInfoId")
                        .HasColumnType("uuid");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("numeric");

                    b.HasIndex("SeedInfoId");

                    b.ToTable("FarmSeeds", (string)null);
                });

            modelBuilder.Entity("SharedDomain.Entities.Schedules.Cultivations.ConsumeCultivation", b =>
                {
                    b.HasBaseType("SharedDomain.Entities.Schedules.AdditionOfActivity");

                    b.Property<Guid>("ComponentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Notes")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Resources")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<double>("UseValue")
                        .HasColumnType("double precision");

                    b.HasIndex("ComponentId");

                    b.ToTable("UsedRecords");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.BaseComponent", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.Site", "Site")
                        .WithMany("Components")
                        .HasForeignKey("SiteId")
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.ComponentProperty", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.BaseComponent", "Component")
                        .WithMany("Properties")
                        .HasForeignKey("ComponentId")
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmSeed", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.BaseComponent", null)
                        .WithOne()
                        .HasForeignKey("SharedDomain.Entities.FarmComponents.FarmSeed", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SharedDomain.Entities.FarmComponents.Common.SeedInfo", null)
                        .WithMany("Uses")
                        .HasForeignKey("SeedInfoId");
                });

            modelBuilder.Entity("SharedDomain.Entities.Schedules.Cultivations.ConsumeCultivation", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.BaseComponent", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId")
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.BaseComponent", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Common.SeedInfo", b =>
                {
                    b.Navigation("Uses");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Site", b =>
                {
                    b.Navigation("Components");
                });
#pragma warning restore 612, 618
        }
    }
}
