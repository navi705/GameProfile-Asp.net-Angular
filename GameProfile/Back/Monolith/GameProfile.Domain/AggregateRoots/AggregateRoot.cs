using GameProfile.Domain.Entities;

namespace GameProfile.Domain.AggregateRoots
{
    public abstract class AggregateRoot : Entity
    {
        public AggregateRoot(Guid id) : base(id)
        {
        }
    }
}
