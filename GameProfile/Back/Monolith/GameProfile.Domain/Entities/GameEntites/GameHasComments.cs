using GameProfile.Domain.Shared;
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

        public string Comment { get; private set; }

        public Guid ProfileId { get; private set; }

        public Guid GameId { get; private set; }

        public DateTime CreatedDate { get; private set; }

        //reference properties for ef core
        public ProfileEntites.Profile? Profile { get; set; }

        public Game? Game { get; set; }

        public ICollection<GameCommentHasReplie>? GameCommentHasReplies { get; set; }

        public static Result<GameHasComments> Create(string content, Guid profileId, Guid gameId)
        {
            Result<GameHasComments> result = new(null, null);

            if (string.IsNullOrEmpty(content))
            {
                result.Failture("content can't be null");
                return result;
            }

            if (content.Length > 2000)
            {
                result.Failture("The content of post should be < 2000 characters");
                return result;
            }

            if (content.Length < 3)
            {
                result.Failture("The Comment must be at least 3 symbols");
                return result;
            }

            GameHasComments comment = new(Guid.Empty,profileId,gameId,DateTime.Now,content);
            result.UpdateContent(comment);
            return result;
        }
        public Result<GameHasComments> UpdateContent(string content)
        {
            Result<GameHasComments> result = new(this, null);
            if (string.IsNullOrEmpty(content))
            {
                result.Failture("content can't be null");
                return result;
            }

            if (content.Length > 2000)
            {
                result.Failture("The content of post should be < 2000 characters");
                return result;
            }

            if (content.Length < 3)
            {
                result.Failture("The Comment must be at least 3 symbols");
                return result;
            }

            Comment = content;
            return result;
        }
    }
}
