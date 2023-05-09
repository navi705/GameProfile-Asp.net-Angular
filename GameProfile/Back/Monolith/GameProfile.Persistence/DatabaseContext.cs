using GameProfile.Application.Data;
using GameProfile.Domain.Entities;
using GameProfile.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GameProfile.Persistence
{
    public sealed class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameConfigure());
            base.OnModelCreating(modelBuilder);
        }

        public Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql, CancellationToken cancellationToken = default)
        {
            return this.Database.ExecuteSqlInterpolatedAsync(sql,cancellationToken);            
        }

        public Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken = default)
        {
            return  this.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        }

        public DbSet<Game> Games { get; set; }
    }
}
