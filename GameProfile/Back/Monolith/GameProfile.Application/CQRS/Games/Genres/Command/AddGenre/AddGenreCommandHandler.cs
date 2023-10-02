using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Genres.Command.AddGenre
{
    public sealed class AddGenreCommandHandler : IRequestHandler<AddGenreCommand>
    {
        private readonly ICacheService _cacheService;
        public AddGenreCommandHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            var genres = await _cacheService.GetAsync<List<string>>("genres");
            if(genres is null)
            {
                genres = new List<string>();
            }
            genres.Add(request.Genre);
            await _cacheService.SetAsync("genres", genres);
        }
    }
}
