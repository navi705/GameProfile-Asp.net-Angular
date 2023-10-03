using GameProfile.Domain.Entities.ProfileEntites;

namespace GameProfile.Domain.Entities.GameEntites
{
    public class GameCommentHasReplie : Entity
    {
        public GameCommentHasReplie(Guid id,
                                     Guid profileId,
                                     Guid commentId,
                                     DateTime created,
                                     string replie) : base(id)
        {
            ProfileId = profileId;
            CommentId = commentId;
            Replie = replie;
            Created = created;
        }

        public Guid ProfileId { get; private set; }


        public Guid CommentId { get; private set; }

        public DateTime Created { get; private set; }

        public string Replie { get; set; }

        //refrences properties ef core
        public Profile Profile { get; set; }

        public GameHasComments GameHasComments { get; set; }
    }
}
