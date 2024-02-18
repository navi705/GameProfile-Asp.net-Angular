using GameProfile.Domain.Entities.ProfileEntites;
using GameProfile.Domain.Enums.Profile;
using System.Globalization;

namespace GameProfile.Application.DTO
{
    public sealed class ProfileDTO
    {
        public string NickName { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int TotalHours { get; set; }
        public int TotalHoursVerification { get; set; }
        public int TotalHoursNotVerification { get; set; }
        public int TotalHoursForSort { get; set; }
        public List<ProfileGamesDTO> GameList { get; set; }
    }

    public sealed record class ProfileGamesDTO(
        Guid Id,
        string Title,
        Uri HeaderImage,
        int Hours,
        StatusGameProgressions StatusGame,
        int HoursVereficated);

    public sealed record class RankDTO(Ranks Ranks,
                                       Guid GameId,
                                       string GameTitle);

    public sealed record class ProfileForAdminDTO(string name, string role, Guid id);

}
