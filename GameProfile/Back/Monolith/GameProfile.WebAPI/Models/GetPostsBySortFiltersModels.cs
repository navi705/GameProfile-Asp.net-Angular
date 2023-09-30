namespace GameProfile.WebAPI.Models
{
    public sealed class GetPostsBySortFiltersModels
    {
        public string Sorting { get; set; }

        public int Page { get; set; }

        public DateTime CreatedDateOf { get; set; }

        public DateTime CreatedDateTo { get; set; }

        public string? Language { get; set; }

        public string? LanguageExcluding { get; set; }

        public string? Game { get; set; }

        public string? GameExcluding { get; set; }

        public decimal? RateOf { get; set; }

        public decimal? RateTo { get; set; }

        public string? Closed { get; set; }

        public string? Topic{ get; set; }

        public string? TopicExcluding { get; set; }

        public string? SearchString { get; set; }

    }
}
