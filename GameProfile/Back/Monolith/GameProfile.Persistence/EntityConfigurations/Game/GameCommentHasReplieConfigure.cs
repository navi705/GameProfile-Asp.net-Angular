using GameProfile.Domain.Entities.GameEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameProfile.Persistence.EntityConfigurations.Game
{
    public class GameCommentHasReplieConfigure : IEntityTypeConfiguration<GameCommentHasReplie>
    {
        public void Configure(EntityTypeBuilder<GameCommentHasReplie> builder)
        {
            builder.HasKey(replie => replie.Id);
            builder.Property(replie => replie.Id).ValueGeneratedOnAdd();
            builder.HasIndex(replie => replie.Id).IsUnique();

            builder.HasOne(x => x.Profile).WithMany(x => x.GameCommentHasReplies).HasForeignKey(x => x.ProfileId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.GameHasComments).WithMany(x => x.GameCommentHasReplies).HasForeignKey(x => x.CommentId);
        }
    }
}
