using GameProfile.Domain.Entities.GameEntites;

namespace GameProfile.Domain.Entities.ProfileEntites
{
    public sealed class Ranks : Entity
    {
        public Ranks(Guid id,
            Guid gameId,
            Guid profileId,
            string rank,
            string rankName,
            string rankImage,
            string rankMax,
            string rankNameMax,
            string rankImageMax) : base(id)
        {
            GameId = gameId;
            ProfileId = profileId;
            Rank = rank;
            RankName = rankName;
            RankImage = rankImage;
            RankMax = rankMax;
            RankNameMax = rankNameMax;
            RankImageMax = rankImageMax;
            RankMax = rankMax;
        }

        public Guid GameId { get; private set; }

        public Guid ProfileId { get; private set; }
        public string Rank { get; private set; }

        public string RankName { get; private set; }
      
        public string RankImage { get; private set; }

        public string RankMax { get; private set; }

        public string RankNameMax { get; private set; }

        public string RankImageMax { get; private set; }

        #region reference properties
        public Game Game { get; set; }

        public Profile Profile { get; set; }
        #endregion
    }
}
