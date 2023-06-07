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
     //List<int> apps = new(){ 92, 440, 570, 630, 1840, 8230, 17510, 17520, 17530, 17550, 17700, 17730, 24920, 27040, 61800, 61810, 61820, 61830, 92500, 212542, 215350, 215360, 235780, 238690, 259080, 261820, 261980, 220, 340, 4000, 2990, 11240, 20900, 22300, 33910, 45740, 620, 105600, 9350, 9420, 55230, 20920, 4560, 228200, 20540, 115320, 204360, 202970, 202990, 212910, 200510, 219640, 4920, 218660, 223470, 224540, 227300, 72850, 13140, 99900, 206210, 208090, 218230, 1083500, 230410, 236390, 240320, 39000, 107400, 265630, 224260, 235340, 245550, 253710, 233450, 242760, 91310, 216250, 231430, 218620, 250340, 251570, 252490, 700580, 257750, 108600, 9200, 235460, 271590, 222880, 285160, 286940, 222900, 299740, 912290, 301520, 304050, 304930, 238090, 244630, 227940, 322170, 307690, 331470, 10, 80, 100, 240, 730, 282070, 247730, 255710, 355840, 365670, 393380, 774941, 359550, 623990, 431960, 433850, 439700, 438100, 444090, 596350, 448780, 378370, 381210, 521340, 292030, 527450, 555210, 555450, 438740, 632470, 386180, 698780, 700330, 629520, 544920, 863550, 13250, 873190, 878750, 705210, 927890, 304390, 654310, 945360, 963690, 1004240, 838380, 1046930, 1049590, 1146950, 1085660, 1172470, 203770, 1281930, 1293230, 1310910, 43110, 424840, 1782210, 578080 };
    var aerws = await steamApi.GetGamesList();
    //await steamApi.GetgameInfo(746850);
   // int j = 0;
    //foreach (var item in aerws.applist.apps)
    //{
    //   var game1 = await steamApi.GetgameInfo(item.appid);
    //    if (game1 == null)
    //        continue;
    //    if (j > 300)
    //    {
    //        break;
    //    }
    //    var query1 = new CreateGameCommand(game1.Name, game1.ReleaseTime, game1.HeaderImg, game1.Nsfw, "", game1.Genres, game1.Publishers, game1.Developers, null, null, 0);
    //    Debug.WriteLine(game1.Name);
    //    await mediator.Send(query);
    //    j++;
    //}

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