using GameProfile.Domain.Entities.Profile;
using GameProfile.Domain.ValueObjects.Game;

namespace GameProfile.Domain.Entities.GameEntites
{
    public sealed class Game : Entity
    {
        public Game(Guid id,
                    string title,
                    DateTime releaseDate,
                    Uri headerImage,
                    bool nsfw,
                    string description,
                    ICollection<StringForGame> developers,
                    ICollection<StringForGame> publishers,
                    ICollection<StringForGame> genres,
                    ICollection<UriForGame> screenshots,
                    ICollection<UriForGame> shopsLinkBuyGame,
                    int achievementsCount) : this(id,title,releaseDate,headerImage,nsfw,description,achievementsCount)
        {
            Developers = developers;
            Publishers = publishers;
            Genres = genres;
            Screenshots = screenshots;
            ShopsLinkBuyGame = shopsLinkBuyGame;
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
                    int achievementsCount) : base(id)
        {
            Title = title;
            ReleaseDate = releaseDate;
            HeaderImage = headerImage;
            Nsfw = nsfw;
            Description = description;
            AchievementsCount = achievementsCount;
        }

        public string Title { get; private set; }

        public DateTime ReleaseDate { get; private set; }

        public Uri HeaderImage { get; private set; }

        public bool Nsfw { get; private set; }

        public string Description { get; private set; }

        public ICollection<UriForGame>? Screenshots { get; private set; }

        public ICollection<StringForGame>? Genres { get; private set; }

        public ICollection<StringForGame>? Publishers { get; private set; }

        public ICollection<StringForGame>? Developers { get; private set; }

        public ICollection<UriForGame>? ShopsLinkBuyGame { get; private set; }

        //reference property
        public ICollection<ProfileHasGames>? ProfileHasGames { get; set; }

        public int AchievementsCount { get; private set; }
    }
}
