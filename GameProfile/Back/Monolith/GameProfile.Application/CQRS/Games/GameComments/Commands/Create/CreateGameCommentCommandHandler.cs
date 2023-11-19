using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Shared;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.Commands.Create
{
    public sealed class CreateGameCommentCommandHandler : IRequestHandler<CreateGameCommentCommand, Result<GameHasComments>>
    {
        private readonly IDatabaseContext _context;

        public CreateGameCommentCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Result<GameHasComments>> Handle(CreateGameCommentCommand request, CancellationToken cancellationToken)
        {
            var gameComment = GameHasComments.Create(request.Comment,
                                                     request.ProfileId,
                                                     request.GameId);
            if (!string.IsNullOrWhiteSpace(gameComment.ErrorMessage))
            {
                return gameComment;
            }
            await _context.GameHasComments.AddAsync(gameComment.Content, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return gameComment;
        }
    }
}
