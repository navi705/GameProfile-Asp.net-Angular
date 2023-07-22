using GameProfile.Infrastructure.Steam.Models;

namespace GameProfile.Infrastructure.Steam
{
    public interface ISteamApi
    {
        Task<SteamGameFromApi?> GetgameInfoByCmd(int appID);

        Task<ListGames> GetGamesList();

        Task<bool> CheckOpenIdSteam(SteamOpenIdData steamOpenIdData);

        Task<List<string>> SteamUserGetPlayerSummaries(string id);

        Task<SteamOwnedGames> SteamOwnedGames(string steamId);

        Task<SteamGameFromStoreApi> GetGameFromStoreApi(int appId);

        public Task<double> GetGameReview(int appID);
    }
}
