using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Requests.GetGamesBySearch
{
    public sealed record class GetGamesBySearch(string SearchString) : IRequest<List<Game>>;
}
