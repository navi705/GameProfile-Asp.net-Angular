﻿// <auto-generated />
using System;
using GameProfile.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameProfile.Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GamePost", b =>
                {
                    b.Property<Guid>("GamesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GamesId", "PostsId");

                    b.HasIndex("PostsId");

                    b.ToTable("GamePost");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Forum.MessagePost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PostId");

                    b.ToTable("MessagePosts");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Forum.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Author")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Closed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .UseCollation("Latin1_General_100_CS_AS_SC");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Author");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Forum.PostHaveRatingFromProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPositive")
                        .HasColumnType("bit");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PostId");

                    b.HasIndex("ProfileId");

                    b.ToTable("PostHaveRatingFromProfiles");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Forum.Replie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MessagePostId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("MessagePostId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.GameEntites.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AchievementsCount")
                        .HasColumnType("int");

                    b.Property<string>("BackgroundImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeaderImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Nsfw")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .UseCollation("Latin1_General_100_CS_AS_SC");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.GameEntites.GameSteamId", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SteamAppId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("SteamAppId")
                        .IsUnique();

                    b.ToTable("GameSteamIds");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.GameEntites.NotGameSteamId", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SteamAppId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("SteamAppId")
                        .IsUnique();

                    b.ToTable("NotGameSteamIds");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.ProfileEntites.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.ProfileEntites.ProfileHasGames", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MinutesInGame")
                        .HasColumnType("int");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StatusGame")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ProfileId");

                    b.ToTable("ProfileHasGames");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.ProfileEntites.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ProfileRole", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RolesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProfileId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("ProfileRole");
                });

            modelBuilder.Entity("GamePost", b =>
                {
                    b.HasOne("GameProfile.Domain.Entities.GameEntites.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameProfile.Domain.Entities.Forum.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Forum.MessagePost", b =>
                {
                    b.HasOne("GameProfile.Domain.Entities.ProfileEntites.Profile", "Profile")
                        .WithMany("Messages")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GameProfile.Domain.Entities.Forum.Post", "Post")
                        .WithMany("MessagePosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Forum.Post", b =>
                {
                    b.HasOne("GameProfile.Domain.Entities.ProfileEntites.Profile", "Profile")
                        .WithMany("Posts")
                        .HasForeignKey("Author")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsMany("GameProfile.Domain.ValueObjects.StringForEntity", "Languages", b1 =>
                        {
                            b1.Property<Guid>("PostId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("StringFor")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("PostId", "Id");

                            b1.ToTable("Posts_Languages");

                            b1.WithOwner()
                                .HasForeignKey("PostId");
                        });

                    b.Navigation("Languages");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Forum.PostHaveRatingFromProfile", b =>
                {
                    b.HasOne("GameProfile.Domain.Entities.Forum.Post", "Post")
                        .WithMany("PostHaveRatingFromProfiles")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameProfile.Domain.Entities.ProfileEntites.Profile", "Profile")
                        .WithMany("PostHaveRatingFromProfiles")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Forum.Replie", b =>
                {
                    b.HasOne("GameProfile.Domain.Entities.ProfileEntites.Profile", "Profile")
                        .WithMany("Replies")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GameProfile.Domain.Entities.Forum.MessagePost", "MessagePost")
                        .WithMany("Replies")
                        .HasForeignKey("MessagePostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MessagePost");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.GameEntites.Game", b =>
                {
                    b.OwnsMany("GameProfile.Domain.ValueObjects.Game.StringForGame", "Developers", b1 =>
                        {
                            b1.Property<Guid>("GameId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("GameString")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("GameId", "Id");

                            b1.ToTable("Games_Developers");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.OwnsMany("GameProfile.Domain.ValueObjects.Game.StringForGame", "Genres", b1 =>
                        {
                            b1.Property<Guid>("GameId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("GameString")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("GameId", "Id");

                            b1.ToTable("Games_Genres");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.OwnsMany("GameProfile.Domain.ValueObjects.Game.StringForGame", "Publishers", b1 =>
                        {
                            b1.Property<Guid>("GameId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("GameString")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("GameId", "Id");

                            b1.ToTable("Games_Publishers");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.OwnsMany("GameProfile.Domain.ValueObjects.Game.UriForGame", "Screenshots", b1 =>
                        {
                            b1.Property<Guid>("GameId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Uri")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("GameId", "Id");

                            b1.ToTable("Games_Screenshots");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.OwnsMany("GameProfile.Domain.ValueObjects.Game.UriForGame", "ShopsLinkBuyGame", b1 =>
                        {
                            b1.Property<Guid>("GameId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Uri")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("GameId", "Id");

                            b1.ToTable("Games_ShopsLinkBuyGame");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.OwnsMany("GameProfile.Domain.ValueObjects.Game.StringForGame", "Tags", b1 =>
                        {
                            b1.Property<Guid>("GameId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("GameString")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("GameId", "Id");

                            b1.ToTable("Games_Tags");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.OwnsMany("GameProfile.Domain.ValueObjects.Game.Review", "Reviews", b1 =>
                        {
                            b1.Property<Guid>("GameId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<decimal>("Score")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<int>("Site")
                                .HasColumnType("int");

                            b1.HasKey("GameId", "Id");

                            b1.ToTable("Review");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.Navigation("Developers");

                    b.Navigation("Genres");

                    b.Navigation("Publishers");

                    b.Navigation("Reviews");

                    b.Navigation("Screenshots");

                    b.Navigation("ShopsLinkBuyGame");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.ProfileEntites.Profile", b =>
                {
                    b.OwnsMany("GameProfile.Domain.ValueObjects.StringForEntity", "SteamIds", b1 =>
                        {
                            b1.Property<Guid>("ProfileId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("StringFor")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProfileId", "Id");

                            b1.ToTable("Profiles_SteamIds");

                            b1.WithOwner()
                                .HasForeignKey("ProfileId");
                        });

                    b.OwnsOne("GameProfile.Domain.ValueObjects.Profile.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("ProfileId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProfileId");

                            b1.ToTable("Profiles");

                            b1.WithOwner()
                                .HasForeignKey("ProfileId");
                        });

                    b.OwnsOne("GameProfile.Domain.ValueObjects.Profile.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("ProfileId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProfileId");

                            b1.ToTable("Profiles");

                            b1.WithOwner()
                                .HasForeignKey("ProfileId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("SteamIds");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.ProfileEntites.ProfileHasGames", b =>
                {
                    b.HasOne("GameProfile.Domain.Entities.GameEntites.Game", "Game")
                        .WithMany("ProfileHasGames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GameProfile.Domain.Entities.ProfileEntites.Profile", "Profile")
                        .WithMany("ProfileHasGames")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.ProfileEntites.Role", b =>
                {
                    b.OwnsMany("GameProfile.Domain.ValueObjects.StringForEntity", "Rights", b1 =>
                        {
                            b1.Property<Guid>("RoleId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("StringFor")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("RoleId", "Id");

                            b1.ToTable("Roles_Rights");

                            b1.WithOwner()
                                .HasForeignKey("RoleId");
                        });

                    b.Navigation("Rights");
                });

            modelBuilder.Entity("ProfileRole", b =>
                {
                    b.HasOne("GameProfile.Domain.Entities.ProfileEntites.Profile", null)
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameProfile.Domain.Entities.ProfileEntites.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Forum.MessagePost", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Forum.Post", b =>
                {
                    b.Navigation("MessagePosts");

                    b.Navigation("PostHaveRatingFromProfiles");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.GameEntites.Game", b =>
                {
                    b.Navigation("ProfileHasGames");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.ProfileEntites.Profile", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("PostHaveRatingFromProfiles");

                    b.Navigation("Posts");

                    b.Navigation("ProfileHasGames");

                    b.Navigation("Replies");
                });
#pragma warning restore 612, 618
        }
    }
}
