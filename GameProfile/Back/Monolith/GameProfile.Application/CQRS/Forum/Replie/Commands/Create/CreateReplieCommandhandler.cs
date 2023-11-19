using GameProfile.Application.Data;
using GameProfile.Domain.Shared;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.Replie.Commands.Create
{
    public sealed class CreateReplieCommandhandler : IRequestHandler<CreateReplieCommand, Result<GameProfile.Domain.Entities.Forum.Replie>>
    {
        private readonly IDatabaseContext _context;

        public CreateReplieCommandhandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Result<Domain.Entities.Forum.Replie>> Handle(CreateReplieCommand request, CancellationToken cancellationToken)
        {
            var replie = Domain.Entities.Forum.Replie.Create(request.Content, request.AuthorId, request.MessagePostId);

            if (!string.IsNullOrWhiteSpace(replie.ErrorMessage))
            {
                return replie;
            }

            await _context.Replies.AddAsync(replie.Content);
            await _context.SaveChangesAsync(cancellationToken);
            return replie;
        }
    }
}
