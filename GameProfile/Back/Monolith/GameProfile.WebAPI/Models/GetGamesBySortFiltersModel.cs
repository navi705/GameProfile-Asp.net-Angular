namespace GameProfile.WebAPI.Models
{
    public class GetGamesBySortFiltersModel
    {
        public string Sorting { get; set; }

        public DateTime ReleaseDateOf { get; set; }
        public DateTime ReleaseDateTo { get; set; }
    }
}
