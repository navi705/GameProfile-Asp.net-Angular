﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameProfile.Domain.Entities.Profile;

namespace GameProfile.Persistence.EntityConfigurations
{
    public sealed class ProfileConfigure : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(profile => profile.Id);
            builder.Property(profile => profile.Id).ValueGeneratedOnAdd();
            builder.HasIndex(profile => profile.Id).IsUnique();


            builder.OwnsMany(profile => profile.SteamIds);
            builder.OwnsOne(x => x.Description);
            builder.OwnsOne(x => x.Name);
        }
    }
}