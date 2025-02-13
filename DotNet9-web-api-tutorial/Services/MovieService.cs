using System.Linq;
using DotNet9_web_api_tutorial.DTOs;
using DotNet9_web_api_tutorial.Models;
using DotNet9_web_api_tutorial.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DotNet9_web_api_tutorial.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _dbContext;
        private readonly ILogger<MovieService> _logger;
        public MovieService(MovieDbContext dbContext, ILogger<MovieService> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<MovieDto> CreateMovieAsync(CreateMovieDto command)
        {
            var movie = Movie.Create(command.Title, command.Genre, command.ReleaseDate, command.Rating);

            await _dbContext.AddAsync(movie);
            await _dbContext.SaveChangesAsync();
            
            return new MovieDto(movie.Id, movie.Title, movie.Genre, movie.ReleaseDate, movie.Rating);
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await _dbContext.Movies
                .AsNoTracking()
                .Select(m => new MovieDto(
                    m.Id, m.Title, m.Genre, m.ReleaseDate, m.Rating)).ToListAsync();

            return movies;
        }

        public async Task<MovieDto?> GetMovieByIdAsync(Guid id)
        {
            var movie = await _dbContext.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return null;

            return new MovieDto(movie.Id, movie.Title, movie.Genre, movie.ReleaseDate, movie.Rating);
        }

        public async Task UpdateMovieAsync(Guid id, UpdateMovieDto command)
        {
            var movieToUpdate = await _dbContext.Movies.FindAsync(id);
            if (movieToUpdate == null)
                throw new ArgumentNullException($"Invalid movie: {id}");

            movieToUpdate.Update(command.Title, command.Genre, command.ReleaseDate, command.Rating);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(Guid id)
        {
            var movieToDelete = await _dbContext.Movies.FindAsync(id);
            if (movieToDelete == null)
                throw new ArgumentNullException($"Invalid movie: {id}");

            _dbContext.Movies.Remove(movieToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
