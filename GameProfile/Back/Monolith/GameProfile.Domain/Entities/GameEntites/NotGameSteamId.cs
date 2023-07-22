namespace GameProfile.Domain.Entities.GameEntites
{
    public class NotGameSteamId : Entity
    {
        public NotGameSteamId(Guid id, int steamAppId) : base(id)
        {
            SteamAppId = steamAppId;
        }

        public int SteamAppId { get; init; }

    }
}
