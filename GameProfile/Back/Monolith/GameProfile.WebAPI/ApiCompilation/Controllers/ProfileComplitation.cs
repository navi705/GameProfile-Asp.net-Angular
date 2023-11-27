using GameProfile.Application;
using GameProfile.Application.CQRS.Games.GamesSteamAppId.Requests;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.CreateProfileHasGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.UpdateValidHours;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGameBySteamId;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileSteamIds;
using GameProfile.Application.CQRS.Profiles.Ranks.Commands;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Enums.Profile;
using GameProfile.Infrastructure.Steam;
using GameProfile.Persistence.Caching;
using MediatR;
using System.Runtime.CompilerServices;

namespace GameProfile.WebAPI.ApiCompilation.Controllers
{
    public sealed class ProfileComplitation
    {
        private readonly SteamApiCompilation _steamApiCompilation;
        private readonly ISteamApi _steamApi;
        private readonly ILogger<ProfileComplitation> _logger;
        private readonly ISender Sender;
        private readonly ICacheService _cacheService;

        public ProfileComplitation(ISender sender,
                                   ICacheService cacheService,
                                   ISteamApi steamApi)
        {
            Sender = sender;
            _steamApiCompilation = new SteamApiCompilation(sender,steamApi);
            _steamApi = steamApi;
            _cacheService = cacheService;

        }

        public async Task<string> UpdateSteamGames(Guid profileId)
        {
            var userCache = await _cacheService.GetAsync<UserCache>(profileId.ToString());


            //if (userCache.SteamUpdateTime.CountUpdateSteam > 2)
            //{
            //    return "You already update your profile 2 for 24 hours";
            //}

            //if (userCache.SteamUpdateTime.DateTime.AddHours(24) > DateTime.Now)
            //{
            //    return "You already update your profile you need wait 24 hours";

            //}

            var steamIds = await Sender.Send(new GetProfileSteamIdsQuery(profileId));
            int i = 0;
            int temp= 0;
            Dictionary<int,int> gameCount= new ();
            foreach (var steamId in steamIds)
            {
                var games = await _steamApi.SteamOwnedGames(steamId);
                if (games.games is null)
                {
                    return "Check your profile settings";
                }

                foreach (var game in games.games)
                {
                    gameCount.TryGetValue(game.appid, out temp);
                    if (temp == 0)
                    {
                        gameCount.Add(game.appid,1);
                    }
                    else
                    {
                        gameCount[game.appid]++;
                    }

                    var query = new GetProfileHasGameBySteamIdQuery(profileId, game.appid);
                    var gameProfile = await Sender.Send(query);
                   
                    await _steamApiCompilation.AddRatingGameFromSteam(game.appid,steamId,profileId,i);

                    if (gameProfile is null)
                    {
                        var query3 = new GetGamesIdBySteamIdQuery(game.appid);
                        var gameInfo = await Sender.Send(query3);

                        var gameId = Guid.Empty;

                        if (gameInfo is null)
                        {
                            gameId = await _steamApiCompilation.AddGame(game.appid);
                        }
                        else
                        {
                            gameId = gameInfo.GameId;
                        }

                        if (gameId == Guid.Empty)
                        {
                            continue;
                        }


                        var query2 = new CreateProfileHasGameCommand(profileId, gameId, StatusGameProgressions.Playing, 0, game.playtime_forever);
                        await Sender.Send(query2);
                        continue;
                    }

                    if (i > 0)
                    {
                        //если игра только на одном акке
                        if (gameCount[game.appid] == 1)
                        {
                            var query7 = new UpdateVerificatedMinutesProfileHasGameCommand(gameProfile.ProfileId, gameProfile.GameId, game.playtime_forever);
                            await Sender.Send(query7);
                        }
                        var query6 = new UpdateVerificatedMinutesProfileHasGameCommand(gameProfile.ProfileId, gameProfile.GameId, game.playtime_forever + gameProfile.MinutesInGameVerified);
                        await Sender.Send(query6);
                        continue;
                    }
                    var query1 = new UpdateVerificatedMinutesProfileHasGameCommand(gameProfile.ProfileId, gameProfile.GameId, game.playtime_forever);
                    await Sender.Send(query1);
                }
                i++;
            }

            //userCache.SteamUpdateTime.CountUpdateSteam++;
            //if (userCache.SteamUpdateTime.CountUpdateSteam > 2)
            //{
            //    userCache.SteamUpdateTime.CountUpdateSteam = 0;
            //    userCache.SteamUpdateTime.DateTime = DateTime.Now;
            //}
            //await _cacheService.SetAsync(profileId.ToString(), userCache);
            return "";
        }

        

    }
}
