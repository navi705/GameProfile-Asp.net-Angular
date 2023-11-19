
using GameProfile.Domain.Entities.ProfileEntites;
using GameProfile.Domain.Shared;

namespace GameProfile.Domain.Entities.Forum
{
    public sealed class Replie : Entity
    {
        public Replie(Guid id,string content, DateTime created, Guid authorId, Guid messagePostId) :this(id,created,content)
        { 
            AuthorId = authorId;
            MessagePostId = messagePostId;
        }

        private Replie(Guid id, DateTime created,string content) : base(id)
        {
            Created = created;
            Content = content;
        }

        public string Content { get; private set; }

        public DateTime Created { get; private set; }

        public Guid AuthorId { get; private set; }

        public Guid MessagePostId { get; private set; }
        //reference properties
        public MessagePost MessagePost { get; set; }
        public Profile Profile { get; set; }

        public Result<Replie> UpdateContent(string content)
        {
            // TODO: Check reference content from heap  
            Result<Replie> result = new(this,null);

            if(content == null)
            {
                result.Failture("Reply can't be null");
                return result;
            }

            if (content.Length > 2000)
            {
                result.Failture("The content of the reply to the message should be < 2000 characters");
                return result;
            }

            Content = content;

            return result;
        } 

        public static Result<Replie> Create(string content, Guid author, Guid messagePost)
        {
            Result<Replie> result = new(null, null);

            if (content == null)
            {
                result.Failture("Reply can't be null");
                return result;
            }

            if (content.Length > 2000)
            {
                result.Failture("The content of the reply to the message should be < 2000 characters");
                return result;
            }

            Replie replie = new(Guid.Empty, content, DateTime.Now, author, messagePost);
            result.UpdateContent(replie);
            return result;
        }
    }
}
