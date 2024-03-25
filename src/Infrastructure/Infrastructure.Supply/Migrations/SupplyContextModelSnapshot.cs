﻿// <auto-generated />
using System;
using Infrastructure.Supply.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Supply.Migrations
{
    [DbContext(typeof(SupplyContext))]
    partial class SupplyContextModelSnapshot : ModelSnapshot
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

                    b.Property<bool>("Available")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Components");

                    b.HasDiscriminator<string>("Type").HasValue("BaseComponent");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SharedDomain.Entities.PreHarvest.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid?>("CreatedByFarmId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<string>("Email")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Notes")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Phone")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("SharedDomain.Entities.PreHarvest.SupplyDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ComponentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision");

                    b.Property<string>("Resource")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uuid");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("ValidFrom")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("ValidTo")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("SupplierId");

                    b.ToTable("SupplyDetails");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmEquipment", b =>
                {
                    b.HasBaseType("SharedDomain.Entities.FarmComponents.BaseComponent");

                    b.HasDiscriminator().HasValue("Equipment");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmFertilize", b =>
                {
                    b.HasBaseType("SharedDomain.Entities.FarmComponents.BaseComponent");

                    b.HasDiscriminator().HasValue("Fertilize");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmPesticide", b =>
                {
                    b.HasBaseType("SharedDomain.Entities.FarmComponents.BaseComponent");

                    b.HasDiscriminator().HasValue("Pesticide");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmSeed", b =>
                {
                    b.HasBaseType("SharedDomain.Entities.FarmComponents.BaseComponent");

                    b.HasDiscriminator().HasValue("Seed");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmSoil", b =>
                {
                    b.HasBaseType("SharedDomain.Entities.FarmComponents.BaseComponent");

                    b.HasDiscriminator().HasValue("Soil");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmWater", b =>
                {
                    b.HasBaseType("SharedDomain.Entities.FarmComponents.BaseComponent");

                    b.HasDiscriminator().HasValue("Water");
                });

            modelBuilder.Entity("SharedDomain.Entities.PreHarvest.SupplyDetail", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.BaseComponent", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId")
                        .IsRequired();

                    b.HasOne("SharedDomain.Entities.PreHarvest.Supplier", "Supplier")
                        .WithMany("Components")
                        .HasForeignKey("SupplierId")
                        .IsRequired();

                    b.Navigation("Component");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("SharedDomain.Entities.PreHarvest.Supplier", b =>
                {
                    b.Navigation("Components");
                });
#pragma warning restore 612, 618
        }
    }
}
