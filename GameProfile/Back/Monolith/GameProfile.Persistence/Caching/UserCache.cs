namespace GameProfile.Persistence.Caching
{
    public sealed class UserCache
    {
        public Uri AvatarImage { get; set; }

        public List<UserDevice> DeviceList { get; set; }

        public SteamUpdateTime SteamUpdateTime { get; set; }
    }

    public class UserDevice
    {
        public string UserAgent { get; set; }

        public string SessionCookie { get; set; }

    }

    public class SteamUpdateTime 
    { 
        public DateTime DateTime { get; set; }

        public int CountUpdateSteam { get; set; }
    }

}

