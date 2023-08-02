using GameProfile.Domain.Entities.Forum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameProfile.Persistence.EntityConfigurations.Forum
{
    public class ReplieConfiguration : IEntityTypeConfiguration<Replie>
    {
        public void Configure(EntityTypeBuilder<Replie> builder)
        {
            builder.HasKey(forum => forum.Id);
            builder.Property(forum => forum.Id).ValueGeneratedOnAdd();
            builder.HasIndex(forum => forum.Id).IsUnique();

            builder.HasOne(x => x.Profile).WithMany(x => x.Replies).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.MessagePost).WithMany(x => x.Replies).HasForeignKey(x => x.MessagePostId);
        }
    }
}
