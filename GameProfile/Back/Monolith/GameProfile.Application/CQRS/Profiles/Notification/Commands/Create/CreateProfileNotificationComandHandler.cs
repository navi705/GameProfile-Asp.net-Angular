using GameProfile.Application.Data;
using GameProfile.Domain.ValueObjects;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Notification.Commands.Create
{
    public sealed class CreateProfileNotificationComandHandler : IRequestHandler<CreateProfileNotificationComand>
    {
        private readonly IDatabaseContext _context;

        public CreateProfileNotificationComandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateProfileNotificationComand request, CancellationToken cancellationToken)
        {
          var profile = _context.Profiles.FirstOrDefault(x => x.Id == request.ProfileId);
            if(profile.NotificationMessages == null)
            {
                profile.NotificationMessages = new List<StringForEntity>
                {
                    new StringForEntity(request.Notification)
                };
            }
            else
            {
                profile.NotificationMessages.Add(new StringForEntity(request.Notification));
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
