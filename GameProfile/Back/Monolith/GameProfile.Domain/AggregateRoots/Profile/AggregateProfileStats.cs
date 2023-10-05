using GameProfile.Domain.Entities;
using GameProfile.Domain.ValueObjects.Profile;

namespace GameProfile.Domain.AggregateRoots.Profile
{
    public sealed class AggregateProfileStats : Entity
    {
        public AggregateProfileStats(Guid id, Name profileName,int totalHours, int hoursVerificated,int totalNotVereficatedHours, int countGames) : base(id)
        {
            ProfileName = profileName;
            TotalHours = totalHours;
            CountGames = countGames;
            HoursVerificated = hoursVerificated;
            TotalNotVereficatedHours = totalNotVereficatedHours;
        }

        public Name ProfileName { get; private set; }

        public int TotalNotVereficatedHours { get; private set; }

        public int CountGames { get; private set; }

        public int HoursVerificated { get; private set; }

        public int TotalHours { get; private set; }
    }
}
