using MediatR;

namespace GameProfile.Application.CQRS.Games.Genres.Command.AddGenre
{
    public sealed record class AddGenreCommand(string Genre) : IRequest;
}
