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
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Notes")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<string>("Resource")
                        .HasMaxLength(2147483647)
                        .HasColumnType("text");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uuid");

                    b.Property<string>("Unit")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("MeasureUnit");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("BaseComponent");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Common.ReferencedSeed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(2147483647)
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Manufactory")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<DateTime?>("ManufactureDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Notes")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<string>("Property")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Resources")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.HasKey("Id");

                    b.ToTable("RefSeedInfos", (string)null);
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

                    b.HasIndex("ComponentId");

                    b.ToTable("Properties");
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

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmSeed", b =>
                {
                    b.HasBaseType("SharedDomain.Entities.FarmComponents.BaseComponent");

                    b.Property<Guid?>("ReferenceId")
                        .HasColumnType("uuid");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("numeric");

                    b.HasIndex("ReferenceId");

                    b.ToTable("FarmSeeds", (string)null);
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.BaseComponent", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
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

                    b.HasOne("SharedDomain.Entities.FarmComponents.Common.ReferencedSeed", "Reference")
                        .WithMany("InUse")
                        .HasForeignKey("ReferenceId");

                    b.Navigation("Reference");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.BaseComponent", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Common.ReferencedSeed", b =>
                {
                    b.Navigation("InUse");
                });
#pragma warning restore 612, 618
        }
    }
}
