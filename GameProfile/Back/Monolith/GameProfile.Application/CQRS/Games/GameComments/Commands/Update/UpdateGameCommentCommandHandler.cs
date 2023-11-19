using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameComments.Commands.Update
{
    public sealed class UpdateGameCommentCommandHandler : IRequestHandler<UpdateGameCommentCommand, Result<GameHasComments>>
    {
        private readonly IDatabaseContext _context;

        public UpdateGameCommentCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Result<GameHasComments>> Handle(UpdateGameCommentCommand request, CancellationToken cancellationToken)
        {
            var gameComment = await _context.GameHasComments.Where(x => x.Id == request.CommentId).FirstOrDefaultAsync(cancellationToken);              
            if(gameComment.ProfileId != request.ProfileId)
            {
                return new (null, "Id don't match");
            }
            var result = gameComment.UpdateContent(request.Comment);
            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                return result;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
