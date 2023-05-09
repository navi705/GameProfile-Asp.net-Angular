using GameProfile.Application.CQRS.Games.Commands.DeleteGame;
using GameProfile.Application.CQRS.Games.Commands.Requests;
using GameProfile.Application.CQRS.Games.Commands.UpdateGame;
using GameProfile.Application.Games.Commands.CreateGame;
using GameProfile.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameProfile.WebAPI.Controllers
{
    [Route("game")]
    public sealed class GameController : ApiController
    {
        public GameController(ISender sender) 
            : base(sender)
        {

        }
        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGameById(Guid gameId)
        {
            var query = new GetGameByIdQuery(gameId);
            var response = await Sender.Send(query);
            return Ok(response);
        }
        [HttpGet("games")]
        public async Task<IActionResult> GetGames()
        {
            var query = new GetGamesQuery();
            return Ok(await Sender.Send(query));
        }
        [HttpPut]
        public async Task<IActionResult> PutGame(Game game)
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
            await Sender.Send(query);
                return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteGame(Guid gameId)
        {
            var query = new DeleteGameCommand(gameId);
            await Sender.Send(query);
            return Ok();
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateGame(Game game,Guid id)
        {
            var query = new UpdateGameCommand(game,id);
            await Sender.Send(query);
            return Ok();
        }
    }
}
