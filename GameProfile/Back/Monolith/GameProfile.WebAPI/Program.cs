using GameProfile.Application.Games.Commands.CreateGame;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Infrastructure.Steam;
using GameProfile.Presentation.Configuration;
using MediatR;
using GameProfile.Application.CQRS.Games.GamesSteamAppId.Commands;
using GameProfile.Application.CQRS.Games.Requests.GetGameByName;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddPresentation();
builder.Services.AddAuthentications(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPut("minimaApi", async (IMediator mediator) =>
{
    SteamApi steamApi = new();
    var aerws = await steamApi.GetGamesList();

    foreach (var item in aerws.applist.apps)
    {
        var game1 = await steamApi.GetgameInfo(item.appid);
        if (game1 == null)
        {
            continue;
        }
        var query1 = new CreateGameCommand(game1.Name, game1.ReleaseTime, game1.HeaderImg, game1.Nsfw, "", game1.Genres, game1.Publishers, game1.Developers, null, null, 0);
        await mediator.Send(query1);
        var query2 = await mediator.Send(new GetGameByNameQuery(game1.Name));
        var query3 = new CreateGamesSteamAppIdQuery(query2.Id, item.appid);
        await mediator.Send(query3);

    }
});

app.MapControllers();
//app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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