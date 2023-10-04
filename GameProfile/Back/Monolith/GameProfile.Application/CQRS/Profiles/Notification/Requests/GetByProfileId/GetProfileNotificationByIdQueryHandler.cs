using GameProfile.Application.Data;
using GameProfile.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.Notification.Requests.GetByProfileId
{
    public sealed class GetProfileNotificationByIdQueryHandler : IRequestHandler<GetProfileNotificationByIdQuery,List<StringForEntity>>
    {
        private readonly IDatabaseContext _context;

        public GetProfileNotificationByIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<StringForEntity>> Handle(GetProfileNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Profiles.AsNoTracking().Where(x => x.Id == request.ProfileId).SelectMany(x => x.NotificationMessages).ToListAsync(cancellationToken);
        }
    }
}
