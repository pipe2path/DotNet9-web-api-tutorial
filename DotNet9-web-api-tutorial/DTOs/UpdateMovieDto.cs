namespace DotNet9_web_api_tutorial.DTOs
{
    public record UpdateMovieDto (string Title, string Genre, DateTimeOffset ReleaseDate, double Rating);
}
