﻿using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Entities.Profile;
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
            modelBuilder.ApplyConfiguration(new ProfileConfigure());
            modelBuilder.ApplyConfiguration(new ProfileHasGamesConfigure());
            modelBuilder.ApplyConfiguration(new GameSteamIdConfigure());
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
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileHasGames> ProfileHasGames { get; set; }
        public DbSet<GameSteamId> GameSteamIds { get; set; }
    }
}
