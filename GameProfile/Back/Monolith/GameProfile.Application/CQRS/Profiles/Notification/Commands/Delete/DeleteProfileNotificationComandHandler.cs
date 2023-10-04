using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Notification.Commands.Delete
{
    public sealed class DeleteProfileNotificationComandHandler : IRequestHandler<DeleteProfileNotificationComand>
    {
        private readonly IDatabaseContext _context;

        public DeleteProfileNotificationComandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProfileNotificationComand request, CancellationToken cancellationToken)
        {
            var profile = _context.Profiles.FirstOrDefault(x => x.Id == request.ProfileId);
            profile.NotificationMessages.Remove(profile.NotificationMessages.FirstOrDefault(x => x.StringFor == request.Notification));
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
