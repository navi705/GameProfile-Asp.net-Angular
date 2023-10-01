using Microsoft.EntityFrameworkCore;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Entities.ProfileEntites;
using GameProfile.Domain.Entities.Forum;

namespace GameProfile.Application.Data
{
    public interface IDatabaseContext 
    {
        DbSet<Game> Games { get; set; }

        DbSet<Profile> Profiles { get; set; }

        DbSet<Post> Posts { get; set; }

        DbSet<MessagePost> MessagePosts { get; set; }

        DbSet<Replie> Replies { get; set; }

        DbSet<ProfileHasGames> ProfileHasGames { get; set; }

        DbSet<GameSteamId> GameSteamIds { get; set; }

        DbSet<NotGameSteamId> NotGameSteamIds { get; set; }

        DbSet<PostHaveRatingFromProfile> PostHaveRatingFromProfiles { get; set; }

        DbSet<Role> Roles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellation = default);

        Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql, CancellationToken cancellationToken = default);

        Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken = default);

    }
}
