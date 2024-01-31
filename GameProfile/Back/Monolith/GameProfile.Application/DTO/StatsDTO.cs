using System.Net;

namespace GameProfile.Application.DTO
{
    public sealed record class StatsDTO(Guid ProfileId,
        string ProfieleNickname,
        int TotalHours,
        int HoursVerificated,
        int NotVereficatedHours,
        int CountGames);

    public sealed record class AdvancedStatsDTO(List<YearStatsDTO> YearStats,List<TagsStatsDTO> TagsStats, List<GenreStatsDTO> GenreStats,
        List<RatingStatsDTO> RatingGenresStats,List<RatingStatsDTO> MostPopularGenreInYearsDTO, List<RatingStatsDTO> TagsPopularGenreInYearsDTO,
        List<RatingStatsDTO> TagsPopularProfileDTO, List<RatingStatsDTO> GenrePopularProfileDTO, List<RatingStatsDTO> MostPopularGameDTO);

    public sealed class YearStatsDTO
    {
        public string Name { get; set; } 
        public int Value { get; set; }
    }

    public sealed class TagsStatsDTO 
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public sealed class GenreStatsDTO
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public sealed class RatingStatsDTO
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }


}
