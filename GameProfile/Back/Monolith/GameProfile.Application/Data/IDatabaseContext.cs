using Microsoft.EntityFrameworkCore;
using GameProfile.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using GameProfile.Domain.Entities.Profile;
using GameProfile.Domain.Entities.GameEntites;

namespace GameProfile.Application.Data
{
    public interface IDatabaseContext 
    {
        DbSet<Game> Games { get; set; }

        DbSet<Profile> Profiles { get; set; }

        DbSet<ProfileHasGames> ProfileHasGames { get; set; }

        DbSet<GameSteamId> GameSteamIds { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellation = default);

        Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql, CancellationToken cancellationToken = default);

        Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken = default);

    }
}
