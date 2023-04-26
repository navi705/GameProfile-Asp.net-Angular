using Microsoft.EntityFrameworkCore;
using GameProfile.Domain.Entities;

namespace GameProfile.Application.Data
{
    public interface IDatabaseContext
    {
        DbSet<Game> Games { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellation = default);

        
    }
}
