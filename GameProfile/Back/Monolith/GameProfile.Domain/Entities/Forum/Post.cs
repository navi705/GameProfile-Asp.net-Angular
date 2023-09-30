using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Entities.ProfileEntites;
using GameProfile.Domain.ValueObjects;
using GameProfile.Domain.ValueObjects.Profile;

namespace GameProfile.Domain.Entities.Forum
{
    public sealed class Post : Entity
    {
        public Post(Guid id,
                    string title,
                    string description,
                    string topic,
                    Guid author,
                    int rating,
                    bool closed,
                    DateTime created,
                    DateTime updated,
                    ICollection<StringForEntity> languages,
                    ICollection<Game> games,
                    ICollection<MessagePost>? messagePosts) : this(id,title,description,topic,rating,closed,created,updated)
        {
            Author = author;
            Languages = languages;
            Games = games;
            MessagePosts = messagePosts;
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        private Post(Guid id,
                    string title,
                    string description,
                    string topic, 
                    int rating,
                    bool closed,
                    DateTime created,
                    DateTime updated) : base(id)
        {
            Title = title;
            Description = description;
            Topic = topic;
            Rating = rating;
            Closed = closed;

            Created = created;
            Updated = updated;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string Topic { get; private set; }

        public Guid Author { get; private set; }

        public int Rating { get; private set; }

        public bool Closed { get;  set; } = false;

        public DateTime Created { get; private set; }

        public DateTime Updated { get; set; }

        public ICollection<StringForEntity> Languages { get; private set; }

        public ICollection<Game>? Games { get;  set; }

        public ICollection<MessagePost>? MessagePosts { get; private set; }

        // refrences properties
        public Profile Profile { get; set; }
    }
}
