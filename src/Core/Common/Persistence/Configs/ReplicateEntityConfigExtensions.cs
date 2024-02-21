﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedDomain.Entities.FarmComponents;
using SharedDomain.Entities.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Persistence.Configs
{
    public static class ReplicateEntityConfigExtensions
    {
        public static EntityTypeBuilder<Site> ExtractSite(this EntityTypeBuilder<Site> builder)
        {
            builder
                .Ignore(e => e.PaymentDetail)
                .Ignore(e => e.Capitals)
                .Ignore(e => e.Components)
                .Ignore(e => e.Description)
                .Ignore(e => e.Subscripts)
                .Ignore(e => e.LogoImg)
                .Ignore(e => e.Acreage)
                .Ignore(e => e.Position)
                .Ignore(e => e.AvatarImg)
                .Ignore(e => e.CreatedDate)
                .Ignore(e => e.LastModify)
                ;

            return builder;
        }

        public static EntityTypeBuilder<BaseComponent> ExtractComponent(this EntityTypeBuilder<BaseComponent> builder)
        {
            builder
                .Ignore(e => e.Description)
                .Ignore(e => e.Documents)
                .Ignore(e => e.Notes)
                .Ignore(e => e.Properties)
                .Ignore(e => e.States)
                .Ignore(e => e.Resource)
                .Ignore(e => e.Unit)
                ;

            return builder;
        }

        public static EntityTypeBuilder<FarmSoil> ExtractSoil(this EntityTypeBuilder<FarmSoil> builder)
        {
            builder
                .Ignore(e => e.Acreage)
                .Ignore(e => e.Position)
                ;

            return builder;
        }

        public static EntityTypeBuilder<FarmSeed> ExtractSeed(this EntityTypeBuilder<FarmSeed> builder)
        {
            builder
                .Ignore(e => e.Reference)
                .Ignore(e => e.ReferenceId)
                .Ignore(e => e.Stock)
                .Ignore(e => e.UnitPrice)
                ;

            return builder;
        }
        





        public static EntityTypeBuilder<Activity> ExtractActivity(this EntityTypeBuilder<Activity> builder)
        {
            builder
                .Ignore(e => e.Participants)
                .Ignore(e => e.IsCompleted)
                .Ignore(e => e.CompletedAt)
                .Ignore(e => e.StartIn)
                .Ignore(e => e.EndIn)
                .Ignore(e => e.Addtions)
                .Ignore(e => e.Season)
                .Ignore(e => e.SeasonId)
                .Ignore(e => e.Notes)
                .Ignore(e => e.Description)
                ;

            return builder;
        }
    }
}