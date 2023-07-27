using GameProfile.Application.CQRS.Games.Commands.DeleteGame;
using GameProfile.Application.CQRS.Games.Commands.Requests;
using GameProfile.Application.CQRS.Games.Commands.UpdateGame;
using GameProfile.Application.CQRS.Games.Requests.GetGamesByPubOrDev;
using GameProfile.Application.CQRS.Games.Requests.GetGamesBySort;
using GameProfile.Application.CQRS.Games.Requests.GetGenres;
using GameProfile.Application.CQRS.Games.Requests.GetTags;
using GameProfile.Application.Games.Commands.CreateGame;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.WebAPI.Models;
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
        [HttpGet("games/search")]
        public async Task<IActionResult> GetGamesBySearch(string title)
        {
            var query = new GetGamesBySearch(title);
            return Ok(await Sender.Send(query));
        }
        [HttpGet("games/devorpub")]
        public async Task<IActionResult> GetGamesByDevOrPub(string type, string who)
        {
            var query = new GetGamesByPubOrDevQuery(type, who);
            return Ok(await Sender.Send(query));
        }
        [HttpGet("games")]
        public async Task<IActionResult> GetGames([FromQuery] GetGamesBySortFiltersModel filters)
        {
            List<string> genres = new();
            List<string> genres1 = new();
            List<string> tags = new();
            List<string> tagsExcluding = new();
            if (filters.Genres is not null)
            {
                 genres = filters.Genres[0].Split(',').ToList();
            }
            if (filters.GenresExcluding is not null)
            {
                genres1 = filters.GenresExcluding[0].Split(',').ToList();
            }
            if(filters.Tags is not null)
            {
                tags = filters.Tags[0].Split(',').ToList();
            }
            if (filters.TagsExcluding is not null)
            {
                tagsExcluding = filters.TagsExcluding[0].Split(',').ToList();
            }
            var query = new GetGamesQuery(filters.Sorting, filters.Page, filters.Nsfw, filters.ReleaseDateOf, filters.ReleaseDateTo, genres, genres1,tags,tagsExcluding);
            return Ok(await Sender.Send(query));
        }
        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres()
        {
            var query = new GetGenresQuery();
            return Ok(await Sender.Send(query));
        }

        [HttpGet("tags")]
        public async Task<IActionResult> GetTags()
        {
            var query = new GetTagsQuery();
            return Ok(await Sender.Send(query));
        }

        //[HttpPut]
        //public async Task<IActionResult> PutGame(Game game)
        //{
        //    var query = new CreateGameCommand(game.Title,
        //                          game.ReleaseDate,
        //                          game.HeaderImage,
        //                          game.Nsfw,
        //                          game.Description,
        //                          game.Genres,
        //                          game.Publishers,
        //                          game.Developers,
        //                          game.Screenshots,
        //                          game.ShopsLinkBuyGame,
        //                          game.AchievementsCount);
        //    await Sender.Send(query);
        //    return Ok();
        //}
        [HttpDelete]
        public async Task<IActionResult> DeleteGame(Guid gameId)
        {
            var query = new DeleteGameCommand(gameId);
            await Sender.Send(query);
            return Ok();
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateGame(Game game, Guid id)
        {
            var query = new UpdateGameCommand(game, id);
            await Sender.Send(query);
            return Ok();
        }
    }
}
