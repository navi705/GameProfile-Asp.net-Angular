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

        Task<string> GetDota2Rating(string steamId);

        //Task<string> GetCS2Rank(string steamId);

        public Task<double> GetGameReview(int appID);
    }
}
