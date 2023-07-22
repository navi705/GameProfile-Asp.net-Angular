﻿// <auto-generated />
using System;
using GameProfile.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameProfile.Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230717165116_maybeitswor")]
    partial class maybeitswor
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("GameProfile.Domain.Entities.Profile.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Profile.ProfileHasGames", b =>
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

            modelBuilder.Entity("GameProfile.Domain.Entities.Profile.Profile", b =>
                {
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

                            b1.ToTable("StringForEntity");

                            b1.WithOwner()
                                .HasForeignKey("ProfileId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("SteamIds");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Profile.ProfileHasGames", b =>
                {
                    b.HasOne("GameProfile.Domain.Entities.GameEntites.Game", "Game")
                        .WithMany("ProfileHasGames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GameProfile.Domain.Entities.Profile.Profile", "Profile")
                        .WithMany("ProfileHasGames")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.GameEntites.Game", b =>
                {
                    b.Navigation("ProfileHasGames");
                });

            modelBuilder.Entity("GameProfile.Domain.Entities.Profile.Profile", b =>
                {
                    b.Navigation("ProfileHasGames");
                });
#pragma warning restore 612, 618
        }
    }
}
