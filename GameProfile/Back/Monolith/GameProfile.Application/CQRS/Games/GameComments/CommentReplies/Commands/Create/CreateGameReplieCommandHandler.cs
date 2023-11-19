using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Shared;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Create
{
    public sealed class CreateGameReplieCommandHandler : IRequestHandler<CreateGameReplieCommand, Result<GameCommentHasReplie>>
    {
        private readonly IDatabaseContext _context;

        public CreateGameReplieCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Result<GameCommentHasReplie>> Handle(CreateGameReplieCommand request, CancellationToken cancellationToken)
        {
            var gameReplie = GameCommentHasReplie.Create(
                request.Comment,
                request.CommentId,
                request.ProfileId);
            if (!string.IsNullOrWhiteSpace(gameReplie.ErrorMessage))
            {
                return gameReplie;
            }
            await _context.GameCommentHasReplies.AddAsync(gameReplie.Content, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return gameReplie;
        }
    }
}
