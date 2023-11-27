using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Entities.ProfileEntites;
using GameProfile.Domain.ValueObjects.Game;

namespace GameProfile.Domain.Entities.GameEntites
{
    public sealed class Game : Entity
    {
        #region Constructors
        public Game(Guid id,
                    string title,
                    DateTime releaseDate,
                    Uri headerImage,
                    Uri backgroundImage,
                    bool nsfw,
                    string description,
                    ICollection<StringForGame> developers,
                    ICollection<StringForGame> publishers,
                    ICollection<StringForGame> genres,
                    ICollection<StringForGame> tags,
                    ICollection<UriForGame> screenshots,
                    ICollection<UriForGame> shopsLinkBuyGame,
                    ICollection<Review> reviews,
                    int achievementsCount) : this(id, title, releaseDate, headerImage, nsfw, description, achievementsCount, backgroundImage)
        {
            Developers = developers;
            Publishers = publishers;
            Genres = genres;
            Tags = tags;
            Screenshots = screenshots;
            ShopsLinkBuyGame = shopsLinkBuyGame;
            Reviews = reviews;
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        
        private Game(Guid id,
                    string title,
                    DateTime releaseDate,
                    Uri headerImage,
                    bool nsfw,
                    string description,
                    int achievementsCount,
                    Uri backgroundImage) : base(id)
        {
            Title = title;
            ReleaseDate = releaseDate;
            HeaderImage = headerImage;
            Nsfw = nsfw;
            Description = description;
            AchievementsCount = achievementsCount;
            BackgroundImage = backgroundImage;
        }
        #endregion
        //I didn't do read only collection because it's a lot of unnecessary code for each collection as the project is small it's not reasonable
        public string Title { get; private set; }

        public DateTime ReleaseDate { get; private set; }

        public Uri HeaderImage { get; private set; }

        public Uri BackgroundImage { get; private set; }

        public bool Nsfw { get; private set; }

        public string Description { get; private set; }

        public ICollection<UriForGame>? Screenshots { get; private set; }

        public ICollection<StringForGame>? Genres { get; private set; }

        public ICollection<StringForGame>? Publishers { get; private set; }

        public ICollection<StringForGame>? Developers { get; private set; }

        public ICollection<UriForGame>? ShopsLinkBuyGame { get; private set; }

        public ICollection<Review>? Reviews { get; private set; }

        public ICollection<StringForGame>? Tags { get; private set; }

        public int AchievementsCount { get; private set; }
        //reference propertys ef core
        public ICollection<ProfileHasGames>? ProfileHasGames { get; set; }

        public ICollection<Post>? Posts { get; set; }

        public ICollection<GameHasRatingFromProfile>? GameHasRatingFromProfiles { get; set; }

        public ICollection<GameHasComments>? GameHasComments { get; set; }

        public ICollection<Ranks>? Ranks { get; set;}

    }
}
