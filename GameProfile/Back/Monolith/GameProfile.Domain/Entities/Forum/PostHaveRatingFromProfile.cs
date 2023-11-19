using GameProfile.Domain.Entities.ProfileEntites;

namespace GameProfile.Domain.Entities.Forum
{
    public sealed class PostHaveRatingFromProfile : Entity
    {
        public PostHaveRatingFromProfile(Guid id, Guid profileId, Guid postId, bool isPositive) : base(id)
        {
            ProfileId = profileId;
            PostId = postId;
            IsPositive = isPositive;
        }

        public Guid ProfileId { get; private set; }

        public Guid PostId { get; private set; }

        public bool IsPositive { get; private set; }

        public void IsPositiveEdit(bool isPositive)
        {
            IsPositive = isPositive;
        }

        // reference propertetys
        public Profile? Profile { get; set; }

        public Post? Post { get; set; }
    }
}
