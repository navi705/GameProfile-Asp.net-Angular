namespace GameProfile.WebAPI.Models.ArgumentModels
{
    public sealed class GetPostsBySortFiltersModels
    {
        public string Sorting { get; set; }

        public int Page { get; set; }

        public DateTime CreatedDateOf { get; set; }

        public DateTime CreatedDateTo { get; set; }

        public List<string>? Languages { get; set; } = new();

        public List<string>? LanguagesExcluding { get; set; } = new();

        public List<string>?  Games { get; set; } = new();

        public List<string>? GamesExcluding { get; set; } = new();

        public decimal? RateOf { get; set; }

        public decimal? RateTo { get; set; }

        public string? Closed { get; set; }

        public List<string>? Topics { get; set; } = new();

        public List<string>? TopicsExcluding { get; set; } = new();

        public string? SearchString { get; set; }

    }
}
