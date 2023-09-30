using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.PostHaveRatingFromProfile.Commands.Create
{
    public sealed class CreatePostHaveRatingFromProfileCommandHandler : IRequestHandler<CreatePostHaveRatingFromProfileCommand>
    {
        private readonly IDatabaseContext _context;

        public CreatePostHaveRatingFromProfileCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(CreatePostHaveRatingFromProfileCommand request, CancellationToken cancellationToken)
        {
            await _context.PostHaveRatingFromProfiles.AddAsync(new Domain.Entities.Forum.PostHaveRatingFromProfile(Guid.NewGuid(), request.ProfileId, request.PostId, request.IsPositive), cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
