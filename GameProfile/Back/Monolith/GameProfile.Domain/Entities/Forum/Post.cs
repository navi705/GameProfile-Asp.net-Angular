using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Entities.ProfileEntites;
using GameProfile.Domain.Shared;
using GameProfile.Domain.ValueObjects;

namespace GameProfile.Domain.Entities.Forum
{
    public sealed class Post : Entity
    {
        #region Constructor
        public Post(Guid id,
                    string title,
                    string description,
                    string topic,
                    Guid author,
                    int rating,
                    bool closed,
                    DateTime created,
                    DateTime updated,
                    ICollection<StringForEntity> languages,
                    ICollection<Game> games,
                    ICollection<MessagePost>? messagePosts) : this(id, title, description, topic, rating, closed, created, updated)
        {
            Author = author;
            Languages = languages;
            Games = games;
            MessagePosts = messagePosts;
        }

        /// <summary>
        /// EF constructor
        /// </summary>
        private Post(Guid id,
                    string title,
                    string description,
                    string topic,
                    int rating,
                    bool closed,
                    DateTime created,
                    DateTime updated) : base(id)
        {
            Title = title;
            Description = description;
            Topic = topic;
            Rating = rating;
            Closed = closed;

            Created = created;
            Updated = updated;
        }
        #endregion
        public string Title { get; private set; }

        public string Description { get; private set; }

        public string Topic { get; private set; }

        public Guid Author { get; private set; }

        public int Rating { get; private set; }

        public bool Closed { get; private set; } = false;

        public DateTime Created { get; private set; }

        public DateTime Updated { get; private set; }

        public ICollection<StringForEntity> Languages { get; private set; }

        public ICollection<Game>? Games { get; private set; }

        public ICollection<MessagePost>? MessagePosts { get; private set; }

        // refrences properties
        public Profile? Profile { get; set; }

        public ICollection<PostHaveRatingFromProfile>? PostHaveRatingFromProfiles { get; set; }
        #region methods
        // In fact, it is extra code, but I wanted to keep as much ddd as possible, but because of ef core we get compromises.
        public void ChangeOpenStatus(bool status)
        {
            Closed = status;
        }

        public void GamesAdd(ICollection<Game> games)
        {
            Games = games;
        }

        public Result<Post> ChangeRating(int rating)
        {
            // for exmaple 
            Result<Post> result = new(this, null);

            if (rating < -2 && rating > 2)
            {
                result.Failture("You can't up rating more 2 or -2");
                return result;
            }
            Rating += rating;
            return result;
        }

        public void UpdateUpdateTime()
        {
            Updated = DateTime.Now;
        }

        public static Result<Post> Create(string title,
                                   string description,
                                   string topic,
                                   Guid authorProfileId,
                                   ICollection<string> languages)
        {
            Result<Post> result = new(null, null);

            if (string.IsNullOrEmpty(title))
            {
                result.Failture("Title can't be null");
                return result;
            }

            if(title.Length > 128)
            {
                result.Failture("The title of post should be < 128 characters");
                return result;
            }

            if (title.Length < 6)
            {
                result.Failture("The title must be at least 6 symbols");
                return result;
            }

            if (string.IsNullOrEmpty(description))
            {
                result.Failture("Description can't be null");
                return result;
            }

            if (description.Length > 1024 )
            {
                result.Failture("The description of post should be < 1024 characters");
                return result;
            }

            if (description.Length < 10)
            {
                result.Failture("The description of post should be > 10 characters");
                return result;
            }

            if(topic is null)
            {
                result.Failture("The topic can't be null");
                return result;
            }

            if(topic is not "Game" and not "Site" and not "Other")
            {
                result.Failture("The topic should be Game or Site or Other");
                return result;
            }

            List<string> languagesConfrim = new() { "Afar", "Abkhazian", "Avestan", "Afrikaans", "Akan", "Amharic", "Aragonese", "Arabic", "Assamese", "Avaric", "Aymara", "Azerbaijani", "Bashkir", "Belarusian", "Bulgarian", "Bihari languages", "Bislama", "Bambara", "Bengali", "Tibetan", "Breton", "Bosnian", "Catalan", "Chechen", "Chamorro", "Corsican", "Cree", "Czech", "Church Slavic", "Chuvash", "Welsh", "Danish", "German", "Maldivian", "Dzongkha", "Ewe", "Greek", "English", "Esperanto", "Spanish", "Estonian", "Basque", "Persian", "Fulah", "Finnish", "Fijian", "Faroese", "French", "Western Frisian", "Irish", "Gaelic", "Galician", "Guarani", "Gujarati", "Manx", "Hausa", "Hebrew", "Hindi", "Hiri Motu", "Croatian", "Haitian", "Hungarian", "Armenian", "Herero", "Interlingua", "Indonesian", "Interlingue", "Igbo", "Sichuan Yi", "Inupiaq", "Ido", "Icelandic", "Italian", "Inuktitut", "Japanese", "Javanese", "Georgian", "Kongo", "Kikuyu", "Kuanyama", "Kazakh", "Kalaallisut", "Central Khmer", "Kannada", "Korean", "Kanuri", "Kashmiri", "Kurdish", "Komi", "Cornish", "Kirghiz", "Latin", "Luxembourgish", "Ganda", "Limburgan", "Lingala", "Lao", "Lithuanian", "Luba-Katanga", "Latvian", "Malagasy", "Marshallese", "Maori", "Macedonian", "Malayalam", "Mongolian", "Marathi", "Malay", "Maltese", "Burmese", "Nauru", "Norwegian", "North Ndebele", "Nepali", "Ndonga", "Dutch", "Norwegian", "Norwegian", "South Ndebele", "Navajo", "Chichewa", "Occitan", "Ojibwa", "Oromo", "Oriya", "Ossetic", "Panjabi", "Pali", "Polish", "Pushto", "Portuguese", "Quechua", "Romansh", "Rundi", "Romanian", "Russian", "Kinyarwanda", "Sanskrit", "Sardinian", "Sindhi", "Northern Sami", "Sango", "Sinhala", "Slovak", "Slovenian", "Samoan", "Shona", "Somali", "Albanian", "Serbian", "Swati", "Sotho, Southern", "Sundanese", "Swedish", "Swahili", "Tamil", "Telugu", "Tajik", "Thai", "Tigrinya", "Turkmen", "Tagalog", "Tswana", "Tonga", "Turkish", "Tsonga", "Tatar", "Twi", "Tahitian", "Uighur", "Ukrainian", "Urdu", "Uzbek", "Venda", "Vietnamese", "Volapük", "Walloon", "Wolof", "Xhosa", "Yiddish", "Yoruba", "Zhuang", "Chinese", "Zulu" };
            // TODO: Vereficated this constuction
            if (languages.Except(languagesConfrim).Any())
            {
                result.Failture("Languages should be confirm");
                return result;
            }

            List<StringForEntity> lang = languages.Select(s => new StringForEntity(s)).ToList();

            result.UpdateContent(new Post(Guid.Empty,title,description,topic,authorProfileId,0,false,DateTime.Now,DateTime.Now,lang,null,null));
            return result;
        }
        #endregion
    }
}
