using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Create
{
    public sealed class CreateGameReplieCommandHandler : IRequestHandler<CreateGameReplieCommand>
    {
        private readonly IDatabaseContext _context;

        public CreateGameReplieCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateGameReplieCommand request, CancellationToken cancellationToken)
        {
            var gameReplie = new GameCommentHasReplie(Guid.Empty,
                                                      request.ProfileId,
                                                      request.CommentId,
                                                      DateTime.Now,
                                                      request.Comment);
            await _context.GameCommentHasReplies.AddAsync(gameReplie, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
