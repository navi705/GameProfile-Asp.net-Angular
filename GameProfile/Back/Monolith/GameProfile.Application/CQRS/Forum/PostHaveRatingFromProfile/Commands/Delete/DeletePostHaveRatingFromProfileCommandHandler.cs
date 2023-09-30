using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.PostHaveRatingFromProfile.Commands.Delete
{
    public sealed class DeletePostHaveRatingFromProfileCommandHandler : IRequestHandler<DeletePostHaveRatingFromProfileCommand>
    {
        private readonly IDatabaseContext _context;

        public DeletePostHaveRatingFromProfileCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(DeletePostHaveRatingFromProfileCommand request, CancellationToken cancellationToken)
        {
            await _context.PostHaveRatingFromProfiles.Where(x => x.PostId == request.PostId && x.ProfileId == request.ProfileId).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
