using GameProfile.Domain.ValueObjects;

namespace GameProfile.Domain.Entities.ProfileEntites
{
    public class Role : Entity
    {
        public Role(Guid id, string name, ICollection<StringForEntity>? rights) : this(id, rights)
        {
            Name = name;
        }

        private Role(Guid id, ICollection<StringForEntity>? rights) : base(id)
        {
            Rights = rights;
        }

        public string Name { get; private set; }

        public ICollection<StringForEntity>? Rights { get; set; }

        //refrences properties ef core
        //Пробую без промежуточной сущности типо без profilehasgames
        //public ICollection<ProfileHasRole>? ProfileHasRoles { get; set; }

        public ICollection<Profile>? Profile { get; set; }
    }
}
