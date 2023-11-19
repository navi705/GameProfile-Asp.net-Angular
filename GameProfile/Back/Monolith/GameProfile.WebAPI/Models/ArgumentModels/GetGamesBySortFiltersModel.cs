using GameProfile.Domain.Enums.Profile;

namespace GameProfile.WebAPI.Models.ArgumentModels
{
    public class GetGamesBySortFiltersModel
    {
        public string Sorting { get; set; }

        public int Page { get; set; }

        public string Nsfw { get; set; } = "none";

        public DateTime ReleaseDateOf { get; set; }

        public DateTime ReleaseDateTo { get; set; }

        public List<string>? Genres { get; set; } = new();

        public List<string>? GenresExcluding { get; set; } = new();

        public List<string>? Tags { get; set; } = new();

        public List<string>? TagsExcluding { get; set; } = new();

        public decimal? RateOf { get; set; }

        public decimal? RateTo { get; set; }

        public List<StatusGameProgressions>? StatusGameProgressions { get; set; } = new();

        public List<StatusGameProgressions>? StatusGameProgressionsExcluding { get; set; } = new();

    }
}
