using GameProfile.Application.Games.Commands.CreateGame;
using GameProfile.Domain.Entities;
using GameProfile.Presentation.Configuration;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPut("game", async (Game game,IMediator mediator) =>
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
    await mediator.Send(query);
});

app.UseHttpsRedirection();

app.Run();