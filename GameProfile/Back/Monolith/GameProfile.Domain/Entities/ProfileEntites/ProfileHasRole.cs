namespace GameProfile.Domain.Entities.ProfileEntites
{
    public class ProfileHasRole : Entity
    {
        public ProfileHasRole(Guid id, Guid profileId, Guid roleId) : base(id)
        {
            ProfileId = profileId;
            RoleId = roleId;
        }

        public Guid ProfileId { get; private set; }

        public Guid RoleId { get; private set; }

        //reference property
        public Profile? Profile { get; set; }

        public Role? Role { get; set; }
    }
}
