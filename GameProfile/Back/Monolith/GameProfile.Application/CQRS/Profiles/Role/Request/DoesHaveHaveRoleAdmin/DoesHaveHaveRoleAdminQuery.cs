using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Role.Request.DoesHaveHaveRoleAdmin
{
    public sealed record class DoesHaveHaveRoleAdminQuery(Guid ProfileId) : IRequest<bool>;
}
