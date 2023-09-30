using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.Replie.Commands.Create
{
    public sealed class CreateReplieCommandhandler : IRequestHandler<CreateReplieCommand, GameProfile.Domain.Entities.Forum.Replie>
    {
        private readonly IDatabaseContext _context;

        public CreateReplieCommandhandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GameProfile.Domain.Entities.Forum.Replie> Handle(CreateReplieCommand request, CancellationToken cancellationToken)
        {
            var replie = new GameProfile.Domain.Entities.Forum.Replie(Guid.Empty,request.Content,DateTime.Now,request.AuthorId,request.MessagePostId);
            await  _context.Replies.AddAsync(replie);
            await _context.SaveChangesAsync(cancellationToken);
            return replie;
        }
    }
}
