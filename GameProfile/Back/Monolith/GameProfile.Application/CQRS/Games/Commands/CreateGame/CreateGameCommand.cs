using GameProfile.Domain.ValueObjects.Game;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Commands.CreateGame
{
    public sealed record class CreateGameCommand(string Title,
                                                 DateTime ReleaseDate,
                                                 Uri HeaderImage,
                                                 Uri BackgroundImage,
                                                 bool NSFW,
                                                 string Description,
                                                 ICollection<StringForGame> Genres,
                                                 ICollection<StringForGame> Publishers,
                                                 ICollection<StringForGame> Developers,
                                                 ICollection<StringForGame> Tags,
                                                 ICollection<UriForGame> Screenshots,
                                                 ICollection<UriForGame> ShopsLinkBuyGame,
                                                 ICollection<Review> Reviews,
                                                 int AchievementsCount) : IRequest;

}
