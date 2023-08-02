using GameProfile.Domain.Entities.Forum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameProfile.Persistence.EntityConfigurations.Forum
{
    public class MessagePostConfiguration : IEntityTypeConfiguration<MessagePost>
    {
        public void Configure(EntityTypeBuilder<MessagePost> builder)
        {
            builder.HasKey(forum => forum.Id);
            builder.Property(forum => forum.Id).ValueGeneratedOnAdd();
            builder.HasIndex(forum => forum.Id).IsUnique();

            builder.HasOne(x => x.Profile).WithMany(x => x.Messages).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Post).WithMany(x => x.MessagePosts).HasForeignKey(x => x.PostId);
        }
    }
}
