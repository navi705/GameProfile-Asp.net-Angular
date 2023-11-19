namespace GameProfile.Application.DTO
{
    public sealed record class PostsSearchDTO(
        Guid Id,
        string Title,
        string Description,
        string Topic,
        DateTime Created,
        ICollection<string> Languages,
        ICollection<PostGameDTO> Games,
        bool IsClosed,
        PostAuthorDTO Author,
        int Rating
        );

    public sealed record class PostGameDTO(
        Guid Id,
        string Title);
    public sealed record class PostAuthorDTO(
        Guid Id,
        string Nickname);
   
    public sealed record class PostOneDTO(Guid Id,
        string Title,
        string Description,
        string Topic,
        int Rating,
        bool IsClosed,
        DateTime Created,
        DateTime Updated,
        PostAuthorDTO Author,
        ICollection<string> Languages,
        ICollection<PostGameDTO> Games,
        ICollection<PostMessagesDTO> Messages
        );

    public sealed record class PostMessagesDTO(Guid Id, string Content, DateTime Created, PostAuthorDTO Author, ICollection<PostMessagesRepliesDTO> Replies );

    public sealed record class PostMessagesRepliesDTO(Guid Id,string Content, DateTime Created, PostAuthorDTO Author);

}
