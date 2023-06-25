using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Requests.GetGamesByPubOrDev
{
    public sealed record class GetGamesByPubOrDevQuery(string Type,string Who) : IRequest<List<Game>>;
}
