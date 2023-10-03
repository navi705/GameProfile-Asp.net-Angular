using GameProfile.Domain.ValueObjects;

namespace GameProfile.Domain.Entities.GameEntites
{
    public class GameHasComments : Entity
    {
        public GameHasComments(Guid id,
                               Guid profileId,
                               Guid gameId,
                               DateTime createdDate,
                               string comment) : base(id)
        {
            ProfileId = profileId;
            GameId = gameId;
            CreatedDate = createdDate;
            Comment = comment;
        }

        public string Comment { get; set; }

        public Guid ProfileId { get; private set; }

        public Guid GameId { get; private set; }

        public DateTime CreatedDate { get; private set; }

        //refrences properties ef core
        public ProfileEntites.Profile Profile { get; set; }

        public Game Game { get; set; }

        public ICollection<GameCommentHasReplie> GameCommentHasReplies { get; set; }
    }
}
