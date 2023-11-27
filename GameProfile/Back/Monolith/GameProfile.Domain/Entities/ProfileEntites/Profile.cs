using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.ValueObjects;
using GameProfile.Domain.ValueObjects.Profile;

namespace GameProfile.Domain.Entities.ProfileEntites
{
    public sealed class Profile : Entity
    {
        public Profile(Guid id,
                       Name name,
                       Description description,
                       ICollection<StringForEntity> steamIds,
                       ICollection<StringForEntity> notificationMessages) : this(id)
        {
            Name = name;
            Description = description;
            SteamIds = steamIds;
            NotificationMessages = notificationMessages;
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        /// I can't remove because ef core
        private Profile(Guid id) : base(id)
        {

        }

        public Name Name { get; private set; }

        public Description Description { get; private set; }

        public ICollection<StringForEntity> SteamIds { get; private set; }

        public ICollection<StringForEntity> NotificationMessages { get; private set; }

        //reference properties for ef core
        #region reference properties 
        public ICollection<ProfileHasGames>? ProfileHasGames { get; set; }
        public ICollection<Post>? Posts { get; set; }

        public ICollection<MessagePost>? Messages { get; set; }

        public ICollection<Replie>? Replies { get; set; }

        public ICollection<PostHaveRatingFromProfile>? PostHaveRatingFromProfiles { get; set; }

        public ICollection<Role>? Roles { get; set; }

        public ICollection<GameHasRatingFromProfile>? GameHasRatingFromProfiles { get; set; }

        public ICollection<GameHasComments>? GameHasComments { get; set; }

        public ICollection<GameCommentHasReplie>? GameCommentHasReplies { get; set;}

        public ICollection<Ranks>? Ranks { get; set; }
        #endregion

        public void AddNotification(StringForEntity notify)
        {
            if(NotificationMessages == null)
            {
                NotificationMessages = new List<StringForEntity>{
                    notify
                };
            }
            else
            {
                NotificationMessages.Add(notify);
            }
        }

    }
}
