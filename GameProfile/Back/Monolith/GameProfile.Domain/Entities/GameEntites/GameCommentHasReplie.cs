using GameProfile.Domain.Entities.ProfileEntites;
using GameProfile.Domain.Shared;
using System.ComponentModel.Design;

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

        public string Replie { get; private set; }

        //refrences properties ef core
        public Profile? Profile { get; set; }

        public GameHasComments? GameHasComments { get; set; }

        public static Result<GameCommentHasReplie> Create(string content,Guid commentId, Guid profileId)
        {
            Result<GameCommentHasReplie> result = new(null, null);

            if (string.IsNullOrEmpty(content))
            {
                result.Failture("Replie can't be null");
                return result;
            }

            if (content.Length > 2000)
            {
                result.Failture("The replie should be < 2000 characters");
                return result;
            }

            if (content.Length < 3)
            {
                result.Failture("The replie must be at least 3 symbols");
                return result;
            }

            GameCommentHasReplie game =  new(Guid.Empty,profileId,commentId,DateTime.Now,content);
            result.UpdateContent(game);
            return result;
        }
        
        public Result<GameCommentHasReplie> Update(string content)
        {
            Result<GameCommentHasReplie> result = new(this, null);

            if (string.IsNullOrEmpty(content))
            {
                result.Failture("Replie can't be null");
                return result;
            }

            if (content.Length > 2000)
            {
                result.Failture("The replie should be < 2000 characters");
                return result;
            }

            if (content.Length < 3)
            {
                result.Failture("The replie must be at least 3 symbols");
                return result;
            }
            Replie = content;
            return result;
        }
    }
}
