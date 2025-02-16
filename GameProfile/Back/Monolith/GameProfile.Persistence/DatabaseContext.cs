﻿using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Entities.ProfileEntites;
using GameProfile.Persistence.EntityConfigurations.Forum;
using GameProfile.Persistence.EntityConfigurations.Game;
using GameProfile.Persistence.EntityConfigurations.Profile;
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

            modelBuilder.ApplyConfiguration(new ProfileConfigure());
            modelBuilder.ApplyConfiguration(new ProfileHasGamesConfigure());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.ApplyConfiguration(new GameSteamIdConfigure());
            modelBuilder.ApplyConfiguration(new NotSteamGameConfigure());

            modelBuilder.ApplyConfiguration(new ReplieConfiguration());
            modelBuilder.ApplyConfiguration(new MessagePostConfiguration());
            modelBuilder.ApplyConfiguration(new ForumConfiguration());
            modelBuilder.ApplyConfiguration(new PostHaveRatingFromProfileConfiguration());

            modelBuilder.ApplyConfiguration(new GameHasRatingFromProfileConfigure());
            modelBuilder.ApplyConfiguration(new GameCommentHasReplieConfigure());
            modelBuilder.ApplyConfiguration(new GameHasCommentsConfigure());

            modelBuilder.ApplyConfiguration(new RankConfigure());

            base.OnModelCreating(modelBuilder);
        }

        #region Games
        public DbSet<Game> Games { get; set; }
        public DbSet<GameSteamId> GameSteamIds { get; set; }
        public DbSet<NotGameSteamId> NotGameSteamIds { get; set; }

        public DbSet<GameHasComments> GameHasComments { get; set; }

        public DbSet<GameCommentHasReplie> GameCommentHasReplies { get; set; }

        public DbSet<GameHasRatingFromProfile> GameHasRatingFromProfiles { get; set; }
        #endregion
        #region Profile
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileHasGames> ProfileHasGames { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<PostHaveRatingFromProfile> PostHaveRatingFromProfiles { get; set; }

        public DbSet<Ranks> Ranks { get; set; }
        #endregion
        #region Forum
        public DbSet<Post> Posts { get; set; }

        public DbSet<MessagePost> MessagePosts {get;set;}

        public DbSet<Replie> Replies { get; set; }
        #endregion
    }
}
