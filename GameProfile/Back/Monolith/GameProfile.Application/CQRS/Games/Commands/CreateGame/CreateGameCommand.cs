using GameProfile.Domain.ValueObjects.Game;
using MediatR;

namespace GameProfile.Application.Games.Commands.CreateGame
{
    public sealed record class CreateGameCommand(string Title,
                                                 DateTime ReleaseDate,
                                                 Uri HeaderImage,
                                                 bool NSFW,
                                                 string Description,                                                 
                                                 ICollection<StringForGame> Genres,
                                                 ICollection<StringForGame> Publishers,
                                                 ICollection<StringForGame> Developers,
                                                 ICollection<UriForGame> Screenshots,
                                                 ICollection<UriForGame> ShopsLinkBuyGame,
                                                 int AchievementsCount) : IRequest;

}
