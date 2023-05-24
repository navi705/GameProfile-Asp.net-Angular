namespace GameProfile.Domain.Enums.Profile
{
    public class StatusGameProgressions1 : Enumeration<StatusGameProgressions1>
    {
        public static readonly StatusGameProgressions1 Playing = new (1,"Playing");

        public static readonly StatusGameProgressions1 Completed = new (2, "Completed");

        public static readonly StatusGameProgressions1 Dropped = new (3,"Dropped");

        public static readonly StatusGameProgressions1 Planned = new (4, "Planned");

        public StatusGameProgressions1(int value, string name) : base(value, name)
        {
        }
    }
}
