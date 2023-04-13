using GameProfile.Application.Data;
using GameProfile.Domain.Entities;
using GameProfile.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<Game> Games { get; set; }
    }
}
