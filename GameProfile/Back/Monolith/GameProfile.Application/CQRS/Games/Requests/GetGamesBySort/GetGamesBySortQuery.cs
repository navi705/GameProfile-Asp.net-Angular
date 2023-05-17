using GameProfile.Domain.Entities;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Requests.GetGamesBySort
{
    public sealed record class GetGamesBySortQuery(string sort) :IRequest<List<Game>>;
}
