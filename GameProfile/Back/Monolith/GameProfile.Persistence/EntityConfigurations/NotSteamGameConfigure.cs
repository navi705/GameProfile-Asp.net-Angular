using GameProfile.Domain.Entities.GameEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameProfile.Persistence.EntityConfigurations
{
    public class NotSteamGameConfigure : IEntityTypeConfiguration<NotGameSteamId>
    {
        void IEntityTypeConfiguration<NotGameSteamId>.Configure(EntityTypeBuilder<NotGameSteamId> builder)
        {
            builder.HasKey(app => app.Id);
            builder.Property(app => app.Id).ValueGeneratedOnAdd();
            builder.HasIndex(app => app.Id).IsUnique();

            builder.HasIndex(app => app.SteamAppId).IsUnique();
        }
    }
}
