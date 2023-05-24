using GameProfile.Domain.ValueObjects;
using GameProfile.Domain.ValueObjects.Profile;

namespace GameProfile.Domain.Entities.Profile
{
    public sealed class Profile : Entity
    {
        public Profile(Guid id,
                       Name name,
                       Description description,
                       ICollection<StringForEntity> steamIds) : this(id)
        {
            Name = name;
            Description = description;
            SteamIds = steamIds;
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        private Profile(Guid id) : base(id)
        {

        }

        public Name Name { get; private set; }

        public Description Description { get; private set; }

        public ICollection<StringForEntity> SteamIds { get; private set; }


    }
}
