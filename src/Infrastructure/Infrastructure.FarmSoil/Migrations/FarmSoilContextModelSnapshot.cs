﻿// <auto-generated />
using System;
using Infrastructure.Soil.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Soil.Migrations
{
    [DbContext(typeof(FarmSoilContext))]
    partial class FarmSoilContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.BaseComponent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("IsConsumable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("BaseComponent");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.ComponentProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComponentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<double>("Require")
                        .HasColumnType("float");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.ComponentState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ActivityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComponentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Site", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarImg")
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("datetime2");

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

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmSoil", b =>
                {
                    b.HasBaseType("SharedDomain.Entities.FarmComponents.BaseComponent");

                    b.Property<double>("Acreage")
                        .HasColumnType("float");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.ToTable("FarmLands");
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

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.ComponentState", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.BaseComponent", "Component")
                        .WithMany("States")
                        .HasForeignKey("ComponentId")
                        .IsRequired();

                    b.Navigation("Component");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.FarmSoil", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.BaseComponent", null)
                        .WithOne()
                        .HasForeignKey("SharedDomain.Entities.FarmComponents.FarmSoil", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.BaseComponent", b =>
                {
                    b.Navigation("Properties");

                    b.Navigation("States");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Site", b =>
                {
                    b.Navigation("Components");
                });
#pragma warning restore 612, 618
        }
    }
}
