using GameProfile.Domain.Entities.ProfileEntites;

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

        public string Content { get; set; }

        public DateTime Created { get; private set; }

        public Guid AuthorId { get; private set; }

        public Guid PostId { get; private set; }

        public ICollection<Replie>? Replies { get; set; }
        //reference properties
        public Profile Profile { get; set; }
        public Post Post { get; set; }
    }
}
