using GameProfile.Application.CQRS.Games.Commands.CreateGame;
using GameProfile.Application.CQRS.Games.GamesSteamAppId.Commands;
using GameProfile.Application.CQRS.Games.GamesSteamAppId.Requests;
using GameProfile.Application.CQRS.Games.NotSteamGameAppID.Command.Create;
using GameProfile.Application.CQRS.Games.NotSteamGameAppID.Requests;
using GameProfile.Application.CQRS.Games.Requests.GetGameByName;
using GameProfile.Application.CQRS.Profiles.Ranks.Commands;
using GameProfile.Domain.Enums.Game;
using GameProfile.Domain.ValueObjects.Game;
using GameProfile.Infrastructure.Shared;
using GameProfile.Infrastructure.Steam;
using GameProfile.Persistence.Migrations;
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
                    // TODO: Is right?
                    await Sender.Send(new NotSteamGameAppIDCreateCommand(appId));
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

        public async Task AddRatingGameFromSteam(int steamAppId,string steamId, Guid profileId, int i)
        {
            if(steamAppId == 530)
            {
                if(i == 0)
                {
                    var query3 = new RankDeleteByGameCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId);
                    await Sender.Send(query3);
                }
                await AddRatingToDota2(steamId, profileId);
            }

            //if (steamAppId == 730)
            //{
            //    if (i == 0)
            //    {
            //        var query3 = new RankDeleteByGameCommand(new Guid("cb9e4ba3-a40c-4d5a-ddea-08db8ac12e56"), profileId);
            //        await Sender.Send(query3);
            //    }
            //    await AddRatingToCS2(steamId,profileId);
            //}
        }

        public async Task AddRatingToDota2(string steamId,Guid profileId)
        {
            var rating = await _steamApi.GetDota2Rating(steamId);

            if (string.IsNullOrWhiteSpace(rating))
            {
                return;
            }

            #region a lot of if
            if (rating == "11")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "1-154 MMR", "Herald 1", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-herald-1.png","","","");
                await Sender.Send(query);
                return;
            }

            if(rating == "12")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "154-308 MMR", "Herald 2", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-herald-2.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "13")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "308-462 MMR", "Herald 3", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-herald-3.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "14")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "462-616 MMR", "Herald 4", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-herald-4.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "15")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "616-769 MMR", "Herald 5", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-herald-5.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "21")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "770-924 MMR", "Guardian 1", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-guardian-1.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "22")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "924-1078 MMR", "Guardian 2", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-guardian-2.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "23")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "1078-1232 MMR", "Guardian 3", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-guardian-3.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "24")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "1232-1386 MMR", "Guardian 4", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-guardian-4.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "26")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "1386-1540 MMR", "Guardian 5", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-guardian-5.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "31")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "1540-1694 MMR", "Crusader 1", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-crusader-1.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "32")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "1694-1848", "Crusader 2", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-crusader-2.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "33")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "1848-2002 MMR", "Crusader 3", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-crusader-3.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "34")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "2002-2156 MMR", "Crusader 4", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-crusader-4.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "35")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "2156-2310 MMR", "Crusader 5", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-crusader-5.png", "", "", "");
                await Sender.Send(query);
                return;
            }


            if (rating == "41")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "2310-2464 MMR", "Archon 1", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-archon-1.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "42")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "2464-2618 MMR", "Archon 2", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-archon-2.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "43")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "2618-2772 MMR", "Archon 3", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-archon-3.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "44")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "2772-2926 MMR", "Archon 4", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-archon-4.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "45")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "2926-3080 MMR", "Archon 5", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-archon-5.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "51")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "3080-3234 MMR", "Legend 1", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-legend-1.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "52")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "3234-3388 MMR", "Legend 2", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-legend-2.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "53")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "3388-3542 MMR", "Legend 3", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-legend-3.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "54")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "3542-3696 MMR", "Legend 4", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-legend-4.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "55")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "3696-3850 MMR", "Legend 5", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-legend-5.png", "", "", "");
                await Sender.Send(query);
                return;
            }


            if (rating == "61")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "3850-4004 MMR", "Ancient 1", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-ancient-1.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "62")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "4004-4158 MMR", "Ancient 2", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-ancient-2.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "63")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "4158-4312 MMR", "Ancient 3", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-ancient-3.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "64")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "4312-4466 MMR", "Ancient 4", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-ancient-4.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "65")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "4466-4620 MMR", "Ancient 5", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-ancient-5.png", "", "", "");
                await Sender.Send(query);
                return;
            }


            if (rating == "71")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "4620-4820 MMR", "Divine 1", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-divine-1.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "72")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "4820-5020 MMR", "Divine 2", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-divine-2.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "73")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "5020-5220 MMR", "Divine 3", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-divine-3.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "74")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "5220-5420 MMR", "Divine 4", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-divine-4.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            if (rating == "75")
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, "5420-5620 MMR", "Divine 5", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-divine-5.png", "", "", "");
                await Sender.Send(query);
                return;
            }

            else
            {
                var query = new RankCreateCommand(new Guid("2bd44f4a-d35f-4974-f11c-08db8f68ef37"), profileId, ">5620 MMR", "Immortal", "https://hawk.live/images/dota-2-seasonal-ranking-medals/seasonal-rank-immortal.png", "", "", "");
                await Sender.Send(query);
                return;
            }


            #endregion
        }

        //public async Task AddRatingToCS2(string steamId, Guid profileId)
        //{
        //    var rank = await _steamApi.GetCS2Rank(steamId);
        //}

    }

}

