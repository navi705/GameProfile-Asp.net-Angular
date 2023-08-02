using GameProfile.Application.CQRS.Games.Commands.CreateGame;
using GameProfile.Application.CQRS.Games.GamesSteamAppId.Commands;
using GameProfile.Application.CQRS.Games.GamesSteamAppId.Requests;
using GameProfile.Application.CQRS.Games.NotSteamGameAppID.Command.Create;
using GameProfile.Application.CQRS.Games.NotSteamGameAppID.Requests;
using GameProfile.Application.CQRS.Games.Requests.GetGameByName;
using GameProfile.Domain.Enums.Game;
using GameProfile.Domain.ValueObjects.Game;
using GameProfile.Infrastructure.Shared;
using GameProfile.Infrastructure.Steam;
using MediatR;

namespace GameProfile.WebAPI.ApiCompilation
{
    public sealed class SteamApiCompilation
    {
        // move to steamApi project/ to infrac layer
        private readonly ISteamApi _steamApi;
        private readonly ISender Sender;
        private readonly MetaCriticApi _metaCritic = new();
        public SteamApiCompilation(ISender sender, ISteamApi steamApi)
        {
            _steamApi = steamApi;
            Sender = sender;
        }
        public async Task<Guid> AddGame(int appId)
        {

            var querySteamId = new GetGamesIdBySteamIdQuery(appId);
            var gameSteamId = await Sender.Send(querySteamId);
            if (gameSteamId is not null)
            {
                return Guid.Empty;
            }
            var queryNotGameSteam = new NotSteamGameAppIdQuery(appId);
            var notGameSteam = await Sender.Send(queryNotGameSteam);
            if (notGameSteam)
            {
                return Guid.Empty; 
            }
            try
            {
                var gameCmd = await _steamApi.GetgameInfoByCmd(appId);
                if (gameCmd == null)
                {
                    return Guid.Empty;
                }
                if (gameCmd.Name == "")
                {
                    await Sender.Send(new NotSteamGameAppIDCreateCommand(appId));
                    return Guid.Empty;
                }
                var gameStore = await _steamApi.GetGameFromStoreApi(appId);
                if (gameStore is null)
                {
                    return Guid.Empty;
                }
                double scoreMetactiric = -1;
                if (gameStore.MetacriticUrl is not null && gameStore.MetacriticUrl != "")
                {
                    scoreMetactiric = await _metaCritic.GetGameReview(gameStore.MetacriticUrl);
                }
                var scoreSteam = await _steamApi.GetGameReview(appId);

                var sites = new List<UriForGame>() { new UriForGame(new Uri($"https://store.steampowered.com/app/{appId}")) };
                if (gameStore.Website is not null && gameStore.Website != "")
                    sites.Add(new UriForGame(new Uri(gameStore.Website)));

                var tagsToRemove = new List<StringForGame> { new StringForGame("Profile Features Limited"), new StringForGame("Free to Play"), new StringForGame("Early Access"), new StringForGame("Soundtrack"), new StringForGame("Controller") };
                gameCmd.Tags.RemoveAll(t => tagsToRemove.Contains(t));
                gameStore.Genres.Remove(new StringForGame("Free to Play"));
                var nonGenre = new List<StringForGame> { new StringForGame("Violent"), new StringForGame("Nudity"), new StringForGame("Sexual Content"), new StringForGame("Gore"), new StringForGame("Casual"), new StringForGame("Indie") };
                foreach (var tag in nonGenre)
                {
                    if (gameStore.Genres.Contains(tag))
                    {
                        gameStore.Genres.Remove(tag);
                        if (!gameCmd.Tags.Contains(tag))
                        {
                            gameCmd.Tags.Add(tag);
                        }
                    }
                }

                var genres = new List<StringForGame> { new StringForGame("Action"), new StringForGame("Strategy"), new StringForGame("Adventure"), new StringForGame("RPG"), new StringForGame("Simulation"), new StringForGame("Racing"), new StringForGame("Massively Multiplayer"), new StringForGame("Sports") };
                foreach (var genre in genres)
                {
                    if (gameCmd.Tags.Contains(genre))
                    {
                        gameCmd.Tags.Remove(genre);
                        if (!gameStore.Genres.Contains(genre))
                        {
                            gameStore.Genres.Add(genre);
                        }
                    }

                }

                if (gameStore.BackgroundUrl == "" || gameStore.BackgroundUrl is null)
                {
                    gameStore.BackgroundUrl = "https://none.com";
                }

                var reviews = new List<Review>() { new Review(SiteReviews.Steam, (decimal)Math.Round(scoreSteam / 10, 1, MidpointRounding.AwayFromZero)) };
                if (scoreMetactiric > 0)
                    reviews.Add(new Review(SiteReviews.Metacritic, (decimal)scoreMetactiric));
                var query1 = new CreateGameCommand(gameCmd.Name,
                                                   gameStore.ReleaseDate,
                                                   gameCmd.HeaderImg,
                                                   new Uri(gameStore.BackgroundUrl),
                                                   gameCmd.Nsfw,
                                                   gameStore.Description,
                                                   gameStore.Genres,
                                                   gameStore.Publishers,
                                                   gameStore.Developers,
                                                   gameCmd.Tags,
                                                   gameStore.Screenshots,
                                                   sites,
                                                   reviews,
                                                   gameStore.AchievementsCount);
                await Sender.Send(query1);
                var query2 = await Sender.Send(new GetGameByNameQuery(gameCmd.Name));
                var query3 = new CreateGamesSteamAppIdQuery(query2.Id, appId);
                await Sender.Send(query3);
                return query2.Id;
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }

}

