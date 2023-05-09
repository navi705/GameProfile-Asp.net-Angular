using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Commands.UpdateGame
{
    public sealed class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand>
    {
        private readonly IDatabaseContext _context;

        public UpdateGameCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var nsfw = request.game.Nsfw ? 1 : 0;
            await _context.ExecuteSqlInterpolatedAsync($"update Games set title = {request.game.Title}, releasedate={request.game.ReleaseDate},headerimage = {request.game.HeaderImage},nsfw = {nsfw},description={request.game.Description},achievementscount={request.game.AchievementsCount} where id = {request.id}",cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
