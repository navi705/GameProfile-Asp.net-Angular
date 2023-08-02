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
                               int minutesInGame) : this(id, profileId, gameId, minutesInGame)
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
                               int minutesInGame) : base(id)
        {
            ProfileId = profileId;
            GameId = gameId;
            MinutesInGame = minutesInGame;
        }

        public Guid ProfileId { get; private set; }

        public Guid GameId { get; private set; }
        //reference property
        public Game Game { get; set; }
        //reference property
        public Profile Profile { get; set; }
        public StatusGameProgressions StatusGame { get; private set; }

        public int MinutesInGame { get; private set; }

    }
}
