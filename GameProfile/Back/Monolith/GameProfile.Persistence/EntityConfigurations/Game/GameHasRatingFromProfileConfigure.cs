using GameProfile.Domain.Entities.ProfileEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameProfile.Persistence.EntityConfigurations.Game
{
    public class GameHasRatingFromProfileConfigure : IEntityTypeConfiguration<GameHasRatingFromProfile>
    {
        public void Configure(EntityTypeBuilder<GameHasRatingFromProfile> builder)
        {
            builder.HasKey(gameHasRating => gameHasRating.Id);
            builder.Property(gameHasRating => gameHasRating.Id).ValueGeneratedOnAdd();
            builder.HasIndex(gameHasRating => gameHasRating.Id).IsUnique();

            builder.HasOne(gameHasRating => gameHasRating.Profile)
             .WithMany(profile => profile.GameHasRatingFromProfiles)
             .HasForeignKey(gameHasRating => gameHasRating.ProfileId);

            builder.HasOne(gameHasRating => gameHasRating.Game)
                .WithMany(game => game.GameHasRatingFromProfiles)
                .HasForeignKey(gameHasRating => gameHasRating.GameId);

        }
    }
}
