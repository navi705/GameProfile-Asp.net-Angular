using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Notification.Commands.Create
{
    public sealed record class CreateProfileNotificationComand(Guid ProfileId,string Notification) : IRequest;
}
