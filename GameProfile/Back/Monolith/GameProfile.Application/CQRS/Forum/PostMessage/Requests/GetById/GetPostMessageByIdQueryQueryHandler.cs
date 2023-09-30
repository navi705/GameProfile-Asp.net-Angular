using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.PostMessage.Requests.GetById
{
    public sealed class GetPostMessageByIdQueryQueryHandler : IRequestHandler<GetPostMessageByIdQuery, MessagePost>
    {
        private readonly IDatabaseContext _context;

        public GetPostMessageByIdQueryQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<MessagePost> Handle(GetPostMessageByIdQuery request, CancellationToken cancellationToken)
        {
            var postMessage = await _context.MessagePosts.AsNoTracking().Where(x => x.Id == request.PostMessageId).FirstOrDefaultAsync(cancellationToken);
            return postMessage;
        }
    }
}
