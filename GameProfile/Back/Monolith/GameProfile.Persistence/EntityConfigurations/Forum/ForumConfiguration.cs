using GameProfile.Domain.Entities.Forum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameProfile.Persistence.EntityConfigurations.Forum
{
    public class ForumConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(forum => forum.Id);
            builder.Property(forum => forum.Id).ValueGeneratedOnAdd();
            builder.HasIndex(forum => forum.Id).IsUnique();

            builder.Property(x => x.Title).UseCollation("Latin1_General_100_CS_AS_SC");

            builder.OwnsMany(x => x.Languages);

            builder.HasOne(x => x.Profile).WithMany(x => x.Posts).HasForeignKey(x => x.Author).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Games).WithMany(p => p.Posts);

        }
    }
}
