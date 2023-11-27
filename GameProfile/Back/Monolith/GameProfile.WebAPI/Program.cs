using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Infrastructure.Steam;
using GameProfile.Infrastructure.Shared;
using MediatR;
using GameProfile.Application.CQRS.Games.GamesSteamAppId.Commands;
using GameProfile.Application.CQRS.Games.Requests.GetGameByName;
using GameProfile.Domain.ValueObjects.Game;
using GameProfile.Domain.Enums.Game;
using System.Diagnostics;
using GameProfile.Application.CQRS.Games.GamesSteamAppId.Requests;
using GameProfile.Application.CQRS.Games.NotSteamGameAppID.Requests;
using GameProfile.Application.CQRS.Games.NotSteamGameAppID.Command.Create;
using GameProfile.WebAPI.Configuration;
using GameProfile.Application.CQRS.Games.Commands.CreateGame;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddPresentation();
builder.Services.AddAuthentications(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapPut("minimaApi", async (IMediator mediator, ISteamApi steamApi) =>
//{
//    var metaCritic = new MetaCriticApi();

//    var games = await steamApi.GetGamesList();

//    int i = 0;
//    foreach (var item in games.applist.apps)
//    {
//        if (i <= 0)
//        {
//            i++;
//            continue; // skip this iteration
//        }
//        var querySteamId = new GetGamesIdBySteamIdQuery(item.appid);
//        var gameSteamId = await mediator.Send(querySteamId);
//        var queryNotGameSteam = new NotSteamGameAppIdQuery(item.appid);
//        var notGameSteam = await mediator.Send(queryNotGameSteam);
//        if (notGameSteam)
//        {
//            Debug.WriteLine($"{item.appid} not game exists iteration {i}");
//            i++;
//            continue;
//        }
//        if (gameSteamId is not null)
//        {
//            Debug.WriteLine($"{item.appid} already exists iteration {i}");
//            i++;
//            continue;
//        }
//        try
//        {
//            var gameCmd = await steamApi.GetgameInfoByCmd(item.appid);
//            if (gameCmd == null)
//            {
//                Debug.WriteLine($"{item.appid} null iteration {i}");
//                await mediator.Send(new NotSteamGameAppIDCreateCommand(item.appid));
//                i++;
//                continue;
//            }
//            if (gameCmd.Name == "")
//            {
//                await mediator.Send(new NotSteamGameAppIDCreateCommand(item.appid));
//                Debug.WriteLine($"{item.appid} not game exists iteration {i}");
//                i++;
//                continue;
//            }
//            var gameStore = await steamApi.GetGameFromStoreApi(item.appid);
//            if (gameStore is null)
//            {
//                Debug.WriteLine($"{item.appid} null iteration {i}");
//                continue;
//            }
//            double scoreMetactiric = -1;
//            if (gameStore.MetacriticUrl is not null && gameStore.MetacriticUrl != "")
//            {
//                scoreMetactiric = await metaCritic.GetGameReview(gameStore.MetacriticUrl);
//            }
//            var scoreSteam = await steamApi.GetGameReview(item.appid);

//            var sites = new List<UriForGame>() { new UriForGame(new Uri($"https://store.steampowered.com/app/{item.appid}")) };
//            if (gameStore.Website is not null && gameStore.Website != "")
//                sites.Add(new UriForGame(new Uri(gameStore.Website)));

//            var tagsToRemove = new List<StringForGame> { new StringForGame("Profile Features Limited"), new StringForGame("Free to Play"), new StringForGame("Early Access"), new StringForGame("Soundtrack"), new StringForGame("Controller") };
//            gameCmd.Tags.RemoveAll(t => tagsToRemove.Contains(t));
//            gameStore.Genres.Remove(new StringForGame("Free to Play"));
//            var nonGenre = new List<StringForGame> { new StringForGame("Violent"), new StringForGame("Nudity"), new StringForGame("Sexual Content"), new StringForGame("Gore"), new StringForGame("Casual"), new StringForGame("Indie") };
//            foreach (var tag in nonGenre)
//            {
//                if (gameStore.Genres.Contains(tag))
//                {
//                    gameStore.Genres.Remove(tag);
//                    if (!gameCmd.Tags.Contains(tag))
//                    {
//                        gameCmd.Tags.Add(tag);
//                    }
//                }
//            }

//            var genres = new List<StringForGame> { new StringForGame("Action"), new StringForGame("Strategy"), new StringForGame("Adventure"), new StringForGame("RPG"), new StringForGame("Simulation"), new StringForGame("Racing"), new StringForGame("Massively Multiplayer"), new StringForGame("Sports") };
//            foreach (var genre in genres)
//            {
//                if (gameCmd.Tags.Contains(genre))
//                {
//                    gameCmd.Tags.Remove(genre);
//                    if (!gameStore.Genres.Contains(genre))
//                    {
//                        gameStore.Genres.Add(genre);
//                    }
//                }

//            }

//            if (gameStore.BackgroundUrl == "" || gameStore.BackgroundUrl is null)
//            {
//                gameStore.BackgroundUrl = "https://none.com";
//            }


//            var reviews = new List<Review>() { new Review(SiteReviews.Steam, (decimal)Math.Round(scoreSteam / 10, 1, MidpointRounding.AwayFromZero)) };
//            if (scoreMetactiric > 0)
//                reviews.Add(new Review(SiteReviews.Metacritic, (decimal)scoreMetactiric));
//            var query1 = new CreateGameCommand(gameCmd.Name,
//                                               gameStore.ReleaseDate,
//                                               gameCmd.HeaderImg,
//                                               new Uri(gameStore.BackgroundUrl),
//                                               gameCmd.Nsfw,
//                                               gameStore.Description,
//                                               gameStore.Genres,
//                                               gameStore.Publishers,
//                                               gameStore.Developers,
//                                               gameCmd.Tags,
//                                               gameStore.Screenshots,
//                                               sites,
//                                               reviews,
//                                               gameStore.AchievementsCount);
//            Debug.WriteLine($"Game {gameCmd.Name} released on {gameStore.ReleaseDate:dd MMM, yyyy}, header image: {gameCmd.HeaderImg}, background URL: {gameStore.BackgroundUrl}, NSFW: {gameCmd.Nsfw}, description: {gameStore.Description}, genres: {string.Join(", ", gameStore.Genres.Select(g => g.GameString))}, publishers: {string.Join(", ", gameStore.Publishers.Select(p => p.GameString))}, developers: {string.Join(", ", gameStore.Developers.Select(d => d.GameString))}, tags: {string.Join(", ", gameCmd.Tags.Select(t => t.GameString))}, screenshots: {string.Join(", ", gameStore.Screenshots.Select(s => s.Uri))}, sites: {string.Join(", ", sites.Select(s => s.Uri))}, reviews: {string.Join(", ", reviews.Select(r => r.Score))}, achievements: {gameStore.AchievementsCount}");
//            await mediator.Send(query1);
//            var query2 = await mediator.Send(new GetGameByNameQuery(gameCmd.Name));
//            var query3 = new CreateGamesSteamAppIdQuery(query2.Id, item.appid);
//            await mediator.Send(query3);
//        }
//        catch
//        {
//            continue;
//        }
//    }
//});

app.UseRateLimiter();

app.MapControllers().RequireRateLimiting("global");
app.UseCors("AllowCredentials");
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});

app.Run();
public partial class Program { }// for testing