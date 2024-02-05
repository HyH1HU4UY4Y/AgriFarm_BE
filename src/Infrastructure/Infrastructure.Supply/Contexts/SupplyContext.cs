using Infrastructure.Supply.Config;
using Microsoft.EntityFrameworkCore;
using SharedApplication.Persistence;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.FarmComponents.Others;
using SharedDomain.Entities.PreHarvest;
using SharedDomain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Supply.Contexts
{
    public class SupplyContext : MultiSiteDbContext
    {
        public SupplyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplyDetail> SupplyDetails { get; set; }
        //public DbSet<Site> Farm  { get; set; }
        public DbSet<BaseComponent> Components  { get; set; }
/*
        public DbSet<FarmSeed> FarmSeeds { get; set; }
        public DbSet<FarmSoil> FarmSoils { get; set; }
        public DbSet<FarmFertilize> FarmFertilizes { get; set; }
        public DbSet<FarmPesticide> FarmPesticides { get; set; }
        public DbSet<FarmWater> FarmWater { get; set; }
*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComponentConfig).Assembly);
            ReduceComponent(modelBuilder);
            //modelBuilder.Entity<BaseComponent>().UseTphMappingStrategy();
/*
            modelBuilder.Ignore<FarmSeed>();
            modelBuilder.Ignore<FarmSoil>();
            modelBuilder.Ignore<FarmWater>();
            modelBuilder.Ignore<FarmEquipment>();
            modelBuilder.Ignore<FarmPesticide>();
            modelBuilder.Ignore<FarmFertilize>();
*/
            modelBuilder.Ignore<Site>();
            modelBuilder.Ignore<ComponentProperty>();
            modelBuilder.Ignore<ComponentState>();
            modelBuilder.Ignore<ComponentDocument>();
        }

        private void ReduceComponent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FarmFertilize>()
                        .Ignore(e=>e.Stock)
                        .Ignore(e=>e.UnitPrice)
                        .Ignore(e=>e.ReferenceId)
                        .Ignore(e=>e.Reference)
                        ;
            modelBuilder.Entity<FarmPesticide>()
                        .Ignore(e=>e.Stock)
                        .Ignore(e=>e.UnitPrice)
                        .Ignore(e=>e.ReferenceId)
                        .Ignore(e=>e.Reference)
                        ;
            modelBuilder.Entity<FarmSeed>()
                        .Ignore(e=>e.Stock)
                        .Ignore(e=>e.UnitPrice)
                        .Ignore(e=>e.ReferenceId)
                        .Ignore(e=>e.Reference)
                        ;
            modelBuilder.Entity<FarmEquipment>()
                        .Ignore(e=>e.UnitPrice)
                        ;
            modelBuilder.Entity<FarmWater>()
                        .Ignore(e=>e.Acreage)
                        .Ignore(e=>e.FromSource)
                        .Ignore(e=>e.Position)
                        ;
            modelBuilder.Entity<FarmSoil>()
                        .Ignore(e=>e.Acreage)
                        .Ignore(e=>e.Position)
                        ;

        }
    }
}
