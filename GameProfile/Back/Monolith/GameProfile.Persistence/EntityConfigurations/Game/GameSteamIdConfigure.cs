using GameProfile.Domain.Entities.GameEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameProfile.Persistence.EntityConfigurations.Game
{
    public sealed class GameSteamIdConfigure : IEntityTypeConfiguration<GameSteamId>
    {
        public void Configure(EntityTypeBuilder<GameSteamId> builder)
        {
            builder.HasKey(game => game.Id);
            builder.Property(game => game.Id).ValueGeneratedOnAdd();
            builder.HasIndex(game => game.Id).IsUnique();

            builder.HasIndex(game => game.SteamAppId).IsUnique();

        }
    }
}
