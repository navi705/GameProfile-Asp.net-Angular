namespace GameProfile.Domain.Entities.GameEntites
{
    public sealed class GameSteamId : Entity
    {
        public GameSteamId(Guid id, Guid gameId, int steamAppId) : base(id)
        {
            GameId = gameId;
            SteamAppId = steamAppId;
        }

        public Guid GameId { get; private set; }

        public int SteamAppId { get; private set; }


    }
}
