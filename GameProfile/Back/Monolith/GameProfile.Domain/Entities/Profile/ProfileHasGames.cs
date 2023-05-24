using GameProfile.Domain.Enums.Profile;

namespace GameProfile.Domain.Entities.Profile
{
    public sealed class ProfileHasGames : Entity
    {
        public ProfileHasGames(Guid id,
                               Guid profileId,
                               Guid gameId,
                               StatusGameProgressions statusGame,
                               int minutesInGame) : this(id,profileId,gameId,minutesInGame)
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

        public StatusGameProgressions StatusGame { get; private set; }

        public int MinutesInGame { get; private set; }

    }
}
