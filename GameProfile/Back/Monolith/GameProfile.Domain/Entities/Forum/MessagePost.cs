using GameProfile.Domain.Entities.ProfileEntites;
using GameProfile.Domain.Shared;

namespace GameProfile.Domain.Entities.Forum
{
    public sealed class MessagePost : Entity
    {
        public MessagePost(Guid id,string content, DateTime created, Guid authorId, Guid postId) : this(id,created,content)
        {
            AuthorId = authorId;
            PostId = postId;
        }
        private MessagePost(Guid id,DateTime created, string content) : base(id)
        {
            Content = content;
            Created = created;
        }

        public string Content { get; private set; }

        public DateTime Created { get; private set; }

        public Guid AuthorId { get; private set; }

        public Guid PostId { get; private set; }

        public ICollection<Replie>? Replies { get; set; }

        //reference properties for ef core
        public Profile? Profile { get; set; }
        public Post? Post { get; set; }

        public Result<MessagePost> UpdateContent(string content)
        {
            // TODO: Check reference content from heap  
            Result<MessagePost> result = new(this, "");

            if (content == null)
            {
                result.Failture("Message can't be null");
                return result;
            }

            if (content.Length > 2000)
            {
                result.Failture("The content of the message for post should be < 2000 characters");
                return result;
            }

            Content = content;

            return result;
        }

        public static Result<MessagePost> Create(string content, Guid author, Guid post)
        {
            Result<MessagePost> result = new(null, null);

            if (content == null)
            {
                result.Failture("Message can't be null");
                return result;
            }

            if (content.Length > 2000)
            {
                result.Failture("The content of the message for post should be < 2000 characters");
                return result;
            }

            MessagePost messagePost = new(Guid.Empty,content,DateTime.Now,author,post);
            result.UpdateContent(messagePost);
            return result;
        }

    }
}
