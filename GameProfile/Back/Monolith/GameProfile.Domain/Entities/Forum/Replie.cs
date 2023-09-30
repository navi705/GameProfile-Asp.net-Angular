
using GameProfile.Domain.Entities.ProfileEntites;

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

        public string Content { get; set; }

        public DateTime Created { get; private set; }

        public Guid AuthorId { get; private set; }

        public Guid MessagePostId { get; private set; }
        //reference properties
        public MessagePost MessagePost { get; set; }
        public Profile Profile { get; set; }
    }
}
