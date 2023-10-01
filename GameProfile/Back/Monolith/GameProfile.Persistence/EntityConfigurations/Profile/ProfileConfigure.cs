using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameProfile.Domain.Entities.ProfileEntites;

namespace GameProfile.Persistence.EntityConfigurations.Profile
{
    public sealed class ProfileConfigure : IEntityTypeConfiguration<GameProfile.Domain.Entities.ProfileEntites.Profile>
    {
        public void Configure(EntityTypeBuilder<GameProfile.Domain.Entities.ProfileEntites.Profile> builder)
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
