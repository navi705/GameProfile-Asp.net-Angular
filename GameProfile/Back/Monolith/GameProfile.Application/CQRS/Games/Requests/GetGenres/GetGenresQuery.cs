using MediatR;

namespace GameProfile.Application.CQRS.Games.Requests.GetGenres
{
    public sealed record class  GetGenresQuery() : IRequest<List<string>>; 

}
