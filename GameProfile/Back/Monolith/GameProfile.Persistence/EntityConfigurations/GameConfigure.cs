using Microsoft.EntityFrameworkCore;
using GameProfile.Domain.Entities.GameEntites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameProfile.Persistence.EntityConfigurations
{
    public sealed class GameConfigure : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(game => game.Id);
            builder.Property(game => game.Id).ValueGeneratedOnAdd();
            builder.HasIndex(game => game.Id).IsUnique();

            builder.Property(x => x.Title).UseCollation("Latin1_General_100_CS_AS_SC");
            builder.OwnsMany(game => game.Screenshots);
            builder.OwnsMany(game => game.Genres);
            builder.OwnsMany(game => game.Developers);
            builder.OwnsMany(game => game.Publishers);
            builder.OwnsMany(game => game.ShopsLinkBuyGame);
            builder.OwnsMany(game => game.Tags);
            builder.OwnsMany(game => game.Reviews);
        }
    }
}
