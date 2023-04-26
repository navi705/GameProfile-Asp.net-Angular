﻿using GameProfile.Domain.Entities;
using GameProfile.Domain.ValueObjects.Game;
using GameProfile.Infrastructure.Steam.Models;
using System.Text.Json;

namespace GameProfile.Infrastructure.Steam
{
    public class SteamApi
    {
        private HttpClient _httpClient = new();

        public async Task<SteamGameFromApi> GetgameInfo(int appID)
        {
            HttpResponseMessage response;
            SteamGameFromApi game = new();
            response = await _httpClient.GetAsync($"https://api.steamcmd.net/v1/info/{appID}");
            JsonDocument document;
            try
            {
                document = JsonDocument.Parse(response.Content.ReadAsStringAsync().Result.ToString());
            }
            catch
            {
                return null;
            }
            JsonElement data;
            try
            {
                data = document.RootElement.GetProperty("data").GetProperty($"{appID}").GetProperty("common");
            }
            catch
            {
                return null;
            }
            JsonElement type = data.GetProperty("type");
            JsonElement associations = data.GetProperty("associations");
            JsonElement genr = data.GetProperty("genres");
            if (type.ToString() != "Game")
            {
                return null;
            }
            game.Name = data.GetProperty("name").ToString();
            try
            {
                game.Nsfw = data.GetProperty("has_adult_content_sex").ToString() == "0" ? false : true;
            }
            catch
            {
                game.Nsfw = false;
            }

            try
            {
                long number = long.Parse(data.GetProperty("steam_release_date").ToString());
                game.ReleaseTime = DateTimeOffset.FromUnixTimeSeconds(number).UtcDateTime;
            }
            catch
            {
            }
            game.HeaderImg= new Uri($"https://cdn.cloudflare.steamstatic.com/steam/apps/{appID}/header.jpg");
            int i = 0;
            while (true)
            {
                try
                {
                    if (associations.GetProperty($"{i}").GetProperty("type").ToString() == "developer" || associations.GetProperty($"{i}").GetProperty("type").ToString() == "publisher")
                    {
                        if (associations.GetProperty($"{i}").GetProperty("type").ToString() == "developer")
                        {
                            var namef = associations.GetProperty($"{i}").GetProperty("name").ToString();
                            game.Developers.Add(new(namef));
                        }
                        if (associations.GetProperty($"{i}").GetProperty("type").ToString() == "publisher")
                        {
                            var namef = associations.GetProperty($"{i}").GetProperty("name").ToString();
                            game.Publishers.Add(new(namef));
                        }
                    }
                }
                catch
                {
                    break;
                }
                i++;
            }
            i = 0;
            StreamReader r = new StreamReader(@"C:\Users\vrclu\source\repos\GameProfile-Asp.net-Angular\GameProfile\Back\Monolith\GameProfile.Infrastructure\Steam\genres.json");
            string json = r.ReadToEnd();
            JsonDocument document1 = JsonDocument.Parse(json);
            JsonElement element = document1.RootElement;
            var arr = JsonSerializer.Deserialize<Dictionary<string, string>>(element); 
            while (true)
            {
                try
                {
                    var getIdGenre = genr.GetProperty($"{i}").ToString();
                   game.Genres.Add(new(arr[getIdGenre]));
                }
                catch
                {
                    break;
                }
                i++;
            }

            return game;
        }

        public async Task<ListGames> GetGamesList()
        {
            HttpResponseMessage response;
            response = await _httpClient.GetAsync("http://api.steampowered.com/ISteamApps/GetAppList/v0002/?format=json");
            var games = JsonSerializer.Deserialize<ListGames>(response.Content.ReadAsStringAsync().Result.ToString());
            return games;
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


    }
}
