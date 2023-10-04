using GameProfile.Domain.ValueObjects;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Notification.Requests.GetByProfileId
{
    public sealed record class GetProfileNotificationByIdQuery(Guid ProfileId): IRequest<List<StringForEntity>>;
}
