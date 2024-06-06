using GameProfile.Domain.ValueObjects.Game;
using GameProfile.Infrastructure.Steam.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Globalization;

namespace GameProfile.Infrastructure.Steam
{
    public class SteamApi : ISteamApi
    {
        private readonly HttpClient _httpClient = new();
        private readonly string _steamCmdUri;
        public SteamApi(string steamCmdUri)
        {
            _steamCmdUri = steamCmdUri;
        }

        public async Task<SteamGameFromApi?> GetgameInfoByCmd(int appID)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_steamCmdUri}/v1/info/{appID}");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);

                var commonData = data["data"]?[appID.ToString()]?["common"];
                if (commonData == null)
                {
                    return null;
                }
                if (commonData["type"]?.ToString() != "Game" && commonData["type"]?.ToString() != "game")
                {
                    return new SteamGameFromApi() { Name = "" };
                }



                var game = new SteamGameFromApi
                {
                    Name = commonData.Value<string>("name"),
                    Nsfw = commonData.Value<string>("has_adult_content_sex") == "1",                 
                    HeaderImg = new Uri($"https://cdn.cloudflare.steamstatic.com/steam/apps/{appID}/header.jpg")
                };

                var tagsJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Steam/tags.json"));
                var tagsDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(tagsJson);
                var tagsData = commonData["store_tags"];
                if (tagsData != null)
                {
                    var tags = new List<StringForGame>();
                    foreach (var tagId in tagsData)
                    {
                        var jsonTag = tagId.ToString();
                        var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>("{" + jsonTag + "}");
                        var idtag = temp.First().Value;
                        tagsDict.TryGetValue(idtag, out var tagName);
                        tags.Add(new StringForGame(tagName));

                    }
                    game.Tags = tags;
                }

                return game;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ListGames> GetGamesList()
        {
            HttpResponseMessage response;
            response = await _httpClient.GetAsync("http://api.steampowered.com/ISteamApps/GetAppList/v0002/?format=json");
            var games = System.Text.Json.JsonSerializer.Deserialize<ListGames>(response.Content.ReadAsStringAsync().Result.ToString());
            return games;
        }
        public async Task<bool> CheckOpenIdSteam(SteamOpenIdData steamOpenIdData)
        {
            HttpResponseMessage response;
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("openid.assoc_handle", steamOpenIdData.openidassoc_handle);
            queryString.Add("openid.signed", steamOpenIdData.openidsigned);
            queryString.Add("openid.sig", steamOpenIdData.openidsig);
            queryString.Add("openid.ns", steamOpenIdData.openidns);
            queryString.Add("openid.mode", steamOpenIdData.openidmode);
            queryString.Add("openid.op_endpoint", steamOpenIdData.openidop_endpoint);
            queryString.Add("openid.claimed_id", steamOpenIdData.openidclaimed_id);
            queryString.Add("openid.identity", steamOpenIdData.openididentity);
            queryString.Add("openid.return_to", steamOpenIdData.openidreturn_to);
            queryString.Add("openid.response_nonce", steamOpenIdData.openidresponse_nonce);
            response = await _httpClient.GetAsync("https://steamcommunity.com/openid/login?" + queryString.ToString());
            var responeString = response.Content.ReadAsStringAsync().Result.ToString();
            if (responeString.Contains("true"))
            {
                return true;
            }
            return false;
        }

        public async Task<List<string>> SteamUserGetPlayerSummaries(string id)
        {
            HttpResponseMessage response;
            response = await _httpClient.GetAsync("https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=E3F8346565FF771EDB7691695BB4A081&steamids=" + id);
            var answer = System.Text.Json.JsonSerializer.Deserialize<GetPlayerSummaries>(response.Content.ReadAsStringAsync().Result.ToString());
            var array = new List<string>();
            array.Add(answer.response.players[0].avatarmedium);
            array.Add(answer.response.players[0].personaname);
            return array;
        }

        public async Task<SteamOwnedGames> SteamOwnedGames(string steamId)
        {
            HttpResponseMessage response;
            response = await _httpClient.GetAsync("https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key=E3F8346565FF771EDB7691695BB4A081&steamid=" + steamId + "&include_played_free_games=true&include_free_sub=true");
            var answer = System.Text.Json.JsonSerializer.Deserialize<SteamOwnedGamesApi>(response.Content.ReadAsStringAsync().Result.ToString());
            return answer.response;
        }

        public async Task<SteamGameFromStoreApi> GetGameFromStoreApi(int appId)
        {
            var game = new SteamGameFromStoreApi();
            var response = await _httpClient.GetAsync($"https://store.steampowered.com/api/appdetails/?appids={appId}&cc=en");
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JObject.Parse(json);

                    var gameData = data[appId.ToString()]["data"];

                    game.Name = gameData["name"]?.ToString() ?? "";
                    game.IsFree = gameData["is_free"]?.ToObject<bool>() ?? false;
                    game.Description = gameData["short_description"]?.ToString() ?? "";
                    game.ImageUrl = gameData["header_image"]?.ToString() ?? "";
                    game.Website = gameData["website"]?.ToString() ?? "";
                    game.Developers = gameData["developers"]?.ToObject<string[]>()?.Select(x => new StringForGame(x)).ToList() ?? new List<StringForGame>();
                    game.Publishers = gameData["publishers"]?.ToObject<string[]>()?.Select(x => new StringForGame(x)).ToList() ?? new List<StringForGame>();
                    game.MetacriticUrl = gameData["metacritic"]?["url"]?.ToString() ?? "";
                    game.BackgroundUrl = gameData["background"]?.ToString() ?? "";
                    DateTime releaseDate = DateTime.TryParseExact(gameData["release_date"]?["date"]?.ToString(), "d MMM, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate) ? releaseDate : DateTime.MinValue;
                    game.ReleaseDate = releaseDate;
                    game.AchievementsCount = gameData["achievements"]?["total"]?.ToObject<int>() ?? 0;

                    game.Genres = gameData["genres"]?.ToObject<JArray>()?.Select(x => new StringForGame(x["description"]?.ToString() ?? "")).ToList() ?? new List<StringForGame>();
                    game.Screenshots = gameData["screenshots"]?.ToObject<JArray>()?.Select(x => new UriForGame(new Uri(x["path_full"]?.ToString()))).ToList() ?? new List<UriForGame>();
                    if (game.Publishers.First().GameString == "")
                    {
                        game.Publishers = new List<StringForGame>();
                    }
                }
                return game;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<double> GetGameReview(int appID)
        {
            var response = await _httpClient.GetAsync($"https://store.steampowered.com/appreviews/{appID}?json=1&language=all");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);
                var positive = data["query_summary"]["total_positive"].ToObject<int>();
                var total = data["query_summary"]["total_reviews"].ToObject<int>();
                if(total == 0)
                {
                    return 0;
                }
                double average = (double)positive / total;
                var score = average - (average - 0.5) * Math.Pow(2, -Math.Log10(total + 1));
                return score * 100;
            }
            return 0;
        }

        public async Task<string> GetDota2Rating(string steamId)
        {
            var steamId32 = SteamID64toSteamID32(steamId);

            var response = await _httpClient.GetAsync($"https://api.opendota.com/api/players/{steamId32}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);
                var mmrEstimate = data["rank_tier"];
                return mmrEstimate.ToString();
            }
            return "";
        }

        static string SteamID64toSteamID32(string steamID64)
        {
            long a = long.Parse(steamID64);
            var asdf = a - 76561197960265728;

            return asdf.ToString();
        }
 
    }

    public class SteamGameFromApi
    {
        public string Name { get; set; }

        public bool Nsfw { get; set; }

        public DateTime ReleaseTime { get; set; }

        public Uri HeaderImg { get; set; }

        public List<StringForGame> Genres { get; set; } = new();

        public List<StringForGame> Publishers { get; set; } = new();

        public List<StringForGame> Developers { get; set; } = new();

        public List<StringForGame> Tags { get; set; } = new();
    }

    public class SteamOpenIdData
    {
        public string openidassoc_handle { get; set; }
        public string openidsigned { get; set; }
        public string openidsig { get; set; }
        public string openidns { get; set; }
        public string openidmode { get; set; }
        public string openidop_endpoint { get; set; }
        public string openidclaimed_id { get; set; }
        public string openididentity { get; set; }
        public string openidreturn_to { get; set; }
        public string openidresponse_nonce { get; set; }
    }

    public class GetPlayerSummaries
    {
        public ResponseGetPlayerSummaries response { get; set; }
    }

    public class ResponseGetPlayerSummaries
    {
        public Player[] players { get; set; }
    }

    public class Player
    {
        public string steamid { get; set; }
        public int communityvisibilitystate { get; set; }
        public int profilestate { get; set; }
        public string personaname { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
        public string avatarhash { get; set; }
        public int lastlogoff { get; set; }
        public int personastate { get; set; }
        public string primaryclanid { get; set; }
        public int timecreated { get; set; }
        public int personastateflags { get; set; }
    }

    public class SteamOwnedGamesApi
    {
        public SteamOwnedGames response { get; set; }
    }

    public class SteamOwnedGames
    {
        public int game_count { get; set; }
        public GameForSteamOwned[] games { get; set; }
    }

    public class GameForSteamOwned
    {
        public int appid { get; set; }
        public int playtime_forever { get; set; }
        public int playtime_windows_forever { get; set; }
        public int playtime_mac_forever { get; set; }
        public int playtime_linux_forever { get; set; }
        public long rtime_last_played { get; set; }
        public int playtime_2weeks { get; set; }
    }

    public class SteamGameFromStoreApi
    {
        public string Name { get; set; }
        public bool IsFree { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Website { get; set; }
        public List<StringForGame> Developers { get; set; }
        public List<StringForGame> Publishers { get; set; }
        public string MetacriticUrl { get; set; }
        public string BackgroundUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<StringForGame> Genres { get; set; }
        public List<UriForGame> Screenshots { get; set; }
        public int AchievementsCount { get; set; }
    }

}
