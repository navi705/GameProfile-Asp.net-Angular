using GameProfile.Domain.Enums.Profile;

namespace GameProfile.Domain.AggregateRoots.Profile
{
    public sealed class AggregateProfileHasGame : AggregateRoot
    {
        public AggregateProfileHasGame(Guid id, string title, Uri headerImage, int hours, StatusGameProgressions statusGame) : base(id)
        {
            Title = title;
            HeaderImage = headerImage;
            Hours = hours;
            StatusGame = statusGame;
        }

        public string Title { get; private set; }

        public Uri HeaderImage { get; private set; }

        public int Hours { get; private set; }

        public StatusGameProgressions StatusGame { get; private set; }
    }
}
