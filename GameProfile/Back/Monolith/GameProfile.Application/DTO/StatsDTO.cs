namespace GameProfile.Application.DTO
{
    public sealed record class StatsDTO(Guid ProfileId,
        string ProfieleNickname,
        int TotalHours,
        int HoursVerificated,
        int NotVereficatedHours,
        int CountGames);
}
