using GameProfile.Application.DTO;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Requests.GetGamesByPubOrDev
{
    public sealed record class GetGamesByPubOrDevQuery(string Type,string Who, Guid ProfileId) : IRequest<List<GamesDTO>>;
}
