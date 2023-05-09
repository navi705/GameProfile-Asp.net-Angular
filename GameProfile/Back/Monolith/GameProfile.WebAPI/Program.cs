using GameProfile.Application.Games.Commands.CreateGame;
using GameProfile.Domain.Entities;
using GameProfile.Infrastructure.Steam;
using GameProfile.Presentation.Configuration;
using MediatR;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddPresentation();
builder.Services.AddAuthentications();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPut("minimaApi", async (Game game,IMediator mediator) =>
{
    var query = new CreateGameCommand(game.Title,
                                  game.ReleaseDate,
                                  game.HeaderImage,
                                  game.Nsfw,
                                  game.Description,
                                  game.Genres,
                                  game.Publishers,
                                  game.Developers,
                                  game.Screenshots,
                                  game.ShopsLinkBuyGame,
                                  game.AchievementsCount);

    SteamApi steamApi = new();
    List<int> apps = new(){ 594650, 218620, 548430, 304930, 648800, 252490, 1465360, 227300, 1190000 };
    //var aerws= await steamApi.GetGamesList();
    //await steamApi.GetgameInfo(746850);
    //int j = 0;
    //foreach (var item in aerws.applist.apps)
    //{
    //    await steamApi.GetgameInfo(item.appid);
    //    if(j > 40)
    //    {
    //        break;
    //    }
    //    j++;
    //}
    //await mediator.Send(query);
    //foreach (var item in apps)
    //{
    //    var game1 = await steamApi.GetgameInfo(item);
    //    var query1 = new CreateGameCommand(game1.Name,game1.ReleaseTime,game1.HeaderImg,game1.Nsfw,"",game1.Genres,game1.Publishers,game1.Developers,null,null,0);
    //    await mediator.Send(query1);
    //}

    
});

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.Run();