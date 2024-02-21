﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Entities.FarmComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedApplication.Persistence.Configs;

namespace Infrastructure.FarmCultivation.Config
{
    public class SeedConfig : IEntityTypeConfiguration<FarmSeed>
    {
        public void Configure(EntityTypeBuilder<FarmSeed> builder)
        {
            builder.ExtractSeed();
            builder.ToTable("SeedInfos");


        }
    }
}
