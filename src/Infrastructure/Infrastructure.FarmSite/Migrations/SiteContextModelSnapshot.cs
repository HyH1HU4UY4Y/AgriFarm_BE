﻿// <auto-generated />
using System;
using Infrastructure.FarmSite.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.CapitalState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("CapitalStates");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.ComponentDocument", b =>
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

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("ComponentDocuments");
                });

            modelBuilder.Entity("SharedDomain.Entities.FarmComponents.Document", b =>
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Resource")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uuid");

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
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarImg")
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Intro")
                        .HasColumnType("varchar(150)");

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

                    b.Property<string>("PaymentDetail")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("SiteCode")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("SharedDomain.Entities.Subscribe.Subscripton", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EndIn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SolutionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartIn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

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

                    b.Navigation("Site");
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
