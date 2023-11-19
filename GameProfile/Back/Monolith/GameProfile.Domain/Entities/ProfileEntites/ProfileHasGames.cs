using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Enums.Profile;

namespace GameProfile.Domain.Entities.ProfileEntites
{
    public sealed class ProfileHasGames : Entity
    {
        public ProfileHasGames(Guid id,
                               Guid profileId,
                               Guid gameId,
                               StatusGameProgressions statusGame,
                               int minutesInGame,
                               int minutesInGameVerified) : this(id, profileId, gameId, minutesInGame,minutesInGameVerified)
        {
            StatusGame = statusGame;
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        /// 
        private ProfileHasGames(Guid id,
                               Guid profileId,
                               Guid gameId,
                               int minutesInGame,
                               int minutesInGameVerified) : base(id)
        {
            ProfileId = profileId;
            GameId = gameId;
            MinutesInGame = minutesInGame;
            MinutesInGameVerified = minutesInGameVerified;
        }

        public Guid ProfileId { get; private set; }

        public Guid GameId { get; private set; }

        public StatusGameProgressions StatusGame { get; private set; }

        public int MinutesInGame { get; private set; }

        public int MinutesInGameVerified { get; private set; }

        //reference properties for ef core
        public Game Game { get; set; }
        public Profile Profile { get; set; }

        public void ChangeVerificatedHours(int minutes)
        {
            MinutesInGameVerified = minutes;
        }
    }
}
