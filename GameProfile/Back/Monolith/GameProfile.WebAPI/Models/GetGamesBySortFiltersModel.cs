

using GameProfile.Domain.Enums.Profile;

namespace GameProfile.WebAPI.Models
{
    public class GetGamesBySortFiltersModel
    {
        public string Sorting { get; set; }

        public int Page { get; set; }

        public string Nsfw { get; set; } = "none";

        public DateTime ReleaseDateOf { get; set; }

        public DateTime ReleaseDateTo { get; set; }

        public List<string>? Genres { get; set; } 

        public List<string>? GenresExcluding { get; set; }

        public List<string>? Tags { get; set; }

        public List<string>? TagsExcluding { get; set; }

        public decimal? RateOf { get; set; }

        public decimal? RateTo { get; set; }

        public string? StatusGameProgressions { get; set; }

        public string? StatusGameProgressionsExcluding { get; set; }

    }
}
