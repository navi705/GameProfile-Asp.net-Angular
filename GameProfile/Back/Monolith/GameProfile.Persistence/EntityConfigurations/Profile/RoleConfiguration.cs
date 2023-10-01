using GameProfile.Domain.Entities.ProfileEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameProfile.Persistence.EntityConfigurations.Profile
{
    public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(profile => profile.Id);
            builder.Property(profile => profile.Id).ValueGeneratedOnAdd();
            builder.HasIndex(profile => profile.Id).IsUnique();

            builder.OwnsMany(profile => profile.Rights);

            builder.HasMany(x=>x.Profile).WithMany(x=>x.Roles);
        }
    }
}
