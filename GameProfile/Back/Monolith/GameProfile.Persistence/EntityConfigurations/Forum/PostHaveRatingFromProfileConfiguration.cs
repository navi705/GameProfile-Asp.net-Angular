using GameProfile.Domain.Entities.Forum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameProfile.Persistence.EntityConfigurations.Forum
{
    public sealed class PostHaveRatingFromProfileConfiguration : IEntityTypeConfiguration<PostHaveRatingFromProfile>
    {
        public void Configure(EntityTypeBuilder<PostHaveRatingFromProfile> builder)
        {
            builder.HasKey(forum => forum.Id);
            builder.Property(forum => forum.Id).ValueGeneratedOnAdd();
            builder.HasIndex(forum => forum.Id).IsUnique();

            builder.HasOne(x => x.Profile).WithMany(x => x.PostHaveRatingFromProfiles).HasForeignKey(x => x.ProfileId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Post).WithMany(x => x.PostHaveRatingFromProfiles).HasForeignKey(x => x.PostId);
        }
    }
}
