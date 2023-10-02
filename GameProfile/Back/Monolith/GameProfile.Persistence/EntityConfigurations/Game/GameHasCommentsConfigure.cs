using GameProfile.Domain.Entities.GameEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameProfile.Persistence.EntityConfigurations.Game
{
    public class GameHasCommentsConfigure : IEntityTypeConfiguration<GameHasComments>
    {
        public void Configure(EntityTypeBuilder<GameHasComments> builder)
        {
            builder.HasKey(GameHasComments => GameHasComments.Id);
            builder.Property(GameHasComments => GameHasComments.Id).ValueGeneratedOnAdd();
            builder.HasIndex(GameHasComments => GameHasComments.Id).IsUnique();

            builder.HasOne(GameHasComments => GameHasComments.Profile)
             .WithMany(profile => profile.GameHasComments)
             .HasForeignKey(GameHasComments => GameHasComments.ProfileId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(GameHasComments => GameHasComments.Game)
                .WithMany(game => game.GameHasComments)
                .HasForeignKey(GameHasComments => GameHasComments.GameId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
