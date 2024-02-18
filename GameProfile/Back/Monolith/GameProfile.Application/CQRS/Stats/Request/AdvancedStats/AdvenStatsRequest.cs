using GameProfile.Application.Data;
using GameProfile.Application.DTO;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Diagnostics;


namespace GameProfile.Application.CQRS.Stats.Request.AdvancedStats
{

    public sealed record class AdvenStatsRequest() : IRequest<AdvancedStatsDTO>;

    public sealed class AdvenStatsRequestHandler : IRequestHandler<AdvenStatsRequest, AdvancedStatsDTO>
    {
        private readonly IDatabaseContext _context;

        public AdvenStatsRequestHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<AdvancedStatsDTO> Handle(AdvenStatsRequest request, CancellationToken cancellationToken)
        {
            AdvancedStatsDTO Stats;
            var Years = await _context.Games
                .AsNoTracking()
            .GroupBy(g => g.ReleaseDate.Year)
            .Select(group => new YearStatsDTO { Name = group.Key.ToString(), Value = group.Count() }
            )
            .OrderBy(x=>x.Name)
        .ToListAsync(cancellationToken);

            Years[0].Name = "Release soon";

            Years.RemoveRange(Years.Count - 14, 14);

            TagsStatsDTO tagsStatsDTO;

            var tags = await _context.Games
                .AsNoTracking()
                .SelectMany(x => x.Tags).GroupBy(x=>x.GameString).Select(g=> new TagsStatsDTO { Name = g.Key.ToString(), Value = g.Count() })
                .ToListAsync(cancellationToken);

            GenreStatsDTO genreStatsDTO;

            var genre = await _context.Games
                .AsNoTracking()
                .SelectMany(x => x.Genres).GroupBy(x => x.GameString).Select(g => new GenreStatsDTO { Name = g.Key.ToString(), Value = g.Count() })
                .ToListAsync(cancellationToken);

            var genreAverageRatings = await _context.Games
    .AsNoTracking()
    .Where(game => game.Reviews.Any()) // Filter out games with no reviews
    .SelectMany(game => game.Genres.Select(tag => new { Tag = tag.GameString, Score = game.Reviews.Average(review => review.Score) }))
    .GroupBy(x => x.Tag)
    .Select(group => 
        new RatingStatsDTO
        {
            Name = group.Key,
            Value = group.Select(x => x.Score).DefaultIfEmpty().Average().ToString()
        }   
    )
    .ToListAsync(cancellationToken);

            //

            var mostPopularGenresByYear = await _context.Games
    .AsNoTracking()
    .Where(game => game.Genres.Any()) // Filter out games with no genres
    .Select(game => new
    {
        Year = game.ReleaseDate.Year,
        Genre = game.Genres.Select(genre => genre.GameString)
                          .GroupBy(genre => genre)
                          .OrderByDescending(group => group.Count())
                          .Select(group => group.Key)
                          .FirstOrDefault()
    })
    .GroupBy(result => result.Year)
    .Select(group => new RatingStatsDTO
    {
        Name = group.Key.ToString(),
        Value = group.Select(result => result.Genre).FirstOrDefault()
    }).OrderBy(x=>x.Name)
    .ToListAsync();

            mostPopularGenresByYear.RemoveRange(0, 1);
            mostPopularGenresByYear.RemoveRange(mostPopularGenresByYear.Count - 12, 12);

            var mostPopularTagsByYear = await _context.Games
    .AsNoTracking()
    .Where(game => game.Tags.Any()) // Filter out games with no genres
    .Select(game => new
    {
        Year = game.ReleaseDate.Year,
        Tag = game.Tags.Select(genre => genre.GameString)
                          .GroupBy(genre => genre)
                          .OrderByDescending(group => group.Count())
                          .Select(group => group.Key)
                          .FirstOrDefault()
    })
    .GroupBy(result => result.Year)
    .Select(group => new RatingStatsDTO
    {
        Name = group.Key.ToString(),
        Value = group.Select(result => result.Tag).FirstOrDefault()
    }).OrderBy(x => x.Name)
    .ToListAsync();

            mostPopularTagsByYear.RemoveRange(0, 1);
            mostPopularTagsByYear.RemoveRange(mostPopularTagsByYear.Count - 14, 14);


            var tagsAndGamesCountInProfiles = await _context.ProfileHasGames
            .Include(profileHasGames => profileHasGames.Game)
                .ThenInclude(game => game.Tags)
            .SelectMany(profileHasGames => profileHasGames.Game.Tags.Select(tag => tag.GameString))
            .GroupBy(tag => tag)
            .Select(group => new RatingStatsDTO
            {
                Name = group.Key,
                Value = group.Count().ToString()
            })
            .ToListAsync();

            var genreAndGamesCountInProfiles = await _context.ProfileHasGames
           .Include(profileHasGames => profileHasGames.Game)
               .ThenInclude(game => game.Genres)
           .SelectMany(profileHasGames => profileHasGames.Game.Genres.Select(tag => tag.GameString))
           .GroupBy(tag => tag)
           .Select(group => new RatingStatsDTO
           {
               Name = group.Key,
               Value = group.Count().ToString()
           })
           .ToListAsync();

            var popularGamesInProfiles = await _context.ProfileHasGames
            .GroupBy(profileHasGames => profileHasGames.GameId)
            .Where(group => group.Count() >= 6)
            .Select(group => new RatingStatsDTO
            {
                Name = group.First().Game.Title,
                Value = group.Count().ToString()
            })
            .OrderBy(result => result.Value)
            .ToListAsync();

            Stats = new AdvancedStatsDTO(Years, tags, genre, genreAverageRatings,mostPopularGenresByYear, mostPopularTagsByYear,
                tagsAndGamesCountInProfiles, genreAndGamesCountInProfiles, popularGamesInProfiles);

            return Stats;
        }

    }
}