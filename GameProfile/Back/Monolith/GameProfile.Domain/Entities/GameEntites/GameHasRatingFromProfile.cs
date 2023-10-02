using GameProfile.Domain.Entities.GameEntites;

namespace GameProfile.Domain.Entities.ProfileEntites
{
    public class GameHasRatingFromProfile : Entity
    {
        public GameHasRatingFromProfile(Guid id,
            Guid profileId,
            Guid gameId,
            int reviewScore) : base(id)
        {
            ProfileId = profileId;
            GameId = gameId;
            ReviewScore = reviewScore;
        }

        public Guid ProfileId { get; private set; }

        public Guid GameId { get; private set; }

        public int ReviewScore { get; set; }

        //refrences properties ef core
        public Profile Profile { get; set; }

        public Game Game { get; set; }


    }
}
