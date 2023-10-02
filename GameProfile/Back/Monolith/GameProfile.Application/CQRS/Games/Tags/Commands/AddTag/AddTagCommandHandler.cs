using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Tags.Commands.AddTag
{
    public sealed class AddTagCommandHandler : IRequestHandler<AddTagCommand>
    {
        private readonly ICacheService _cacheService;

        public AddTagCommandHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Handle(AddTagCommand request, CancellationToken cancellationToken)
        {
            var tags = await _cacheService.GetAsync<List<string>>("tags");
            if (tags is null)
            {
                tags = new List<string>();
            }
            tags.Add(request.Tag);
            await _cacheService.SetAsync("tags", tags);
        }
    }
}
