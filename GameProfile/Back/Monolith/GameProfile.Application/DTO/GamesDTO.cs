using GameProfile.Domain.Enums.Profile;
using GameProfile.Domain.ValueObjects.Game;

namespace GameProfile.Application.DTO
{
    public sealed record class GamesForTitleSearchDTO(Guid Id, string Titile, Uri HeaderImage);

    public sealed record class GamesDTO(
        Guid Id,
        string Title,
        DateTime ReleaseDate,
        Uri HeaderImage,
        ICollection<string> Developers,
        ICollection<string> Publishers,
        ICollection<string> Genres,
        ICollection<Review> Reviews, 
        StatusGameProgressions? Status);
    
    public sealed record class GamesComment(Guid Id, string Content, DateTime Created, GameCommentAuthorDTO Author, ICollection<GamesReplie> Replies);

    public sealed record class GamesReplie(Guid Id, string Content, DateTime Created, GameCommentAuthorDTO Author);

    public sealed record class GameCommentAuthorDTO(
      Guid Id,
      string Nickname,
      int Rating);

}
