using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameProfile.Domain.Entities.Profile;
namespace GameProfile.Persistence.EntityConfigurations
{
    public sealed class ProfileHasGamesConfigure : IEntityTypeConfiguration<ProfileHasGames>
    {

        public void Configure(EntityTypeBuilder<ProfileHasGames> builder)
        {
            builder.HasKey(profilleHasGamesConfigure => profilleHasGamesConfigure.Id);
            builder.Property(profilleHasGamesConfigure => profilleHasGamesConfigure.Id).ValueGeneratedOnAdd();
            builder.HasIndex(profilleHasGamesConfigure => profilleHasGamesConfigure.Id).IsUnique();

            builder.HasOne(x => x.Profile).WithMany(p => p.ProfileHasGames).HasForeignKey(phg => phg.ProfileId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(phg => phg.Game).WithMany(g => g.ProfileHasGames).HasForeignKey(phg => phg.GameId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
