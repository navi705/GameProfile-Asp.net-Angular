using GameProfile.Domain.ValueObjects;

namespace GameProfile.Domain.Entities.ProfileEntites
{
    public class Role : Entity
    {
        public Role(Guid id, string name, ICollection<StringForEntity>? rights) : this(id, name)
        {
            Rights = rights;
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        /// 
        private Role(Guid id, string name) : base(id)
        {
           Name = name;
        }

        public string Name { get; private set; }

        public ICollection<StringForEntity>? Rights { get; set; }

        //refrences properties ef core
        //Пробую без промежуточной сущности типо без profilehasgames
        //public ICollection<ProfileHasRole>? ProfileHasRoles { get; set; }

        public ICollection<Profile>? Profile { get; set; }
    }
}
