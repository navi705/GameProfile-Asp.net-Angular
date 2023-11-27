using GameProfile.Domain.Entities.ProfileEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameProfile.Persistence.EntityConfigurations.Profile
{
    public sealed class RankConfigure : IEntityTypeConfiguration<GameProfile.Domain.Entities.ProfileEntites.Ranks>
    {
        public void Configure(EntityTypeBuilder<Ranks> builder)
        {
            builder.HasKey(profile => profile.Id);
            builder.Property(profile => profile.Id).ValueGeneratedOnAdd();
            builder.HasIndex(profile => profile.Id).IsUnique();

            builder.HasOne(x => x.Game)
                .WithMany(x => x.Ranks)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Profile).WithMany(x => x.Ranks).HasForeignKey(x => x.ProfileId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
