using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Requests.GetGameByName
{
    public sealed record class GetGameByNameQuery(string Name) : IRequest<Game?>;
}
