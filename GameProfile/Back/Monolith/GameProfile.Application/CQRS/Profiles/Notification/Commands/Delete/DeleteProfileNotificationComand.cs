using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Notification.Commands.Delete
{
    public sealed record class DeleteProfileNotificationComand(Guid ProfileId,string Notification) : IRequest;
}
