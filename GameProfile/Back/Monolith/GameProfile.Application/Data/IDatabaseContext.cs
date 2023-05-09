using Microsoft.EntityFrameworkCore;
using GameProfile.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GameProfile.Application.Data
{
    public interface IDatabaseContext 
    {
        DbSet<Game> Games { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellation = default);

        Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql, CancellationToken cancellationToken = default);
        Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken = default);

    }
}
