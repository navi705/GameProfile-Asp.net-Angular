using GameProfile.Domain.Enums.Game;

namespace GameProfile.Domain.ValueObjects.Game
{
    public class Review : ValueObject
    {
        public Review(SiteReviews site, decimal score)
        {
            Site = site;
            Score = score;
        }

        public SiteReviews Site { get; private set; }

        public decimal Score { get; private set; }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Site;
            yield return Score;
        }
    }
}
