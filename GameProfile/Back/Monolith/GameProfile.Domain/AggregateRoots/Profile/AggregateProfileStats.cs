using GameProfile.Domain.Entities;
using GameProfile.Domain.ValueObjects.Profile;

namespace GameProfile.Domain.AggregateRoots.Profile
{
    public sealed class AggregateProfileStats : Entity
    {
        public AggregateProfileStats(Guid id, Name profileName,int hours, int countGames) : base(id)
        {
            ProfileName = profileName;
            TotalHours = hours;
            CountGames = countGames;
        }

        public Name ProfileName { get; private set; }

        public int TotalHours { get; private set; }

        public int CountGames { get; private set; }
    }
}
