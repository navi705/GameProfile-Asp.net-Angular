using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.Commands.Create
{
    public sealed class CreateGameCommentCommandHandler : IRequestHandler<CreateGameCommentCommand>
    {
        private readonly IDatabaseContext _context;

        public CreateGameCommentCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateGameCommentCommand request, CancellationToken cancellationToken)
        {
            var gameComment = new GameHasComments(Guid.Empty,
                                                  request.ProfileId,
                                                  request.GameId,
                                                  DateTime.Now,
                                                  request.Comment);
            await _context.GameHasComments.AddAsync(gameComment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
