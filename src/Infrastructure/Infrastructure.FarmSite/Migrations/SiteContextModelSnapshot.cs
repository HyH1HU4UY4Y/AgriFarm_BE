﻿// <auto-generated />
using System;
using Infrastructure.FarmSite.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.FarmSite.Migrations
{
    [DbContext(typeof(SiteContext))]
    partial class SiteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.CapitalState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("CapitalStates");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.ComponentDocument", b =>
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

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("ComponentDocuments");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Document", b =>
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<string>("Resource")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Site", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Intro")
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("PaymentDetail")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("SiteKey")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("SharedDomain.Entities.Subscribe.PackageSolution", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
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
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("SolutionDetails", (string)null);
                });

            modelBuilder.Entity("SharedDomain.Entities.Subscribe.Subscripton", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndIn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SolutionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartIn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.HasIndex("SolutionId");

                    b.ToTable("SubscriptonBills");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.CapitalState", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.Site", "Site")
                        .WithMany("Capitals")
                        .HasForeignKey("SiteId")
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.ComponentDocument", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.Document", "Document")
                        .WithMany("Components")
                        .HasForeignKey("DocumentId")
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Document", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId")
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("SharedDomain.Entities.Subscribe.Subscripton", b =>
                {
                    b.HasOne("SharedDomain.Entities.FarmComponents.Site", "Site")
                        .WithMany("Subscripts")
                        .HasForeignKey("SiteId")
                        .IsRequired();

                    b.HasOne("SharedDomain.Entities.Subscribe.PackageSolution", "Solution")
                        .WithMany()
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");

                    b.Navigation("Solution");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Document", b =>
                {
                    b.Navigation("Components");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Site", b =>
                {
                    b.Navigation("Capitals");

                    b.Navigation("Subscripts");
                });
#pragma warning restore 612, 618
        }
    }
}
