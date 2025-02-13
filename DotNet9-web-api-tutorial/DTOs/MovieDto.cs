namespace DotNet9_web_api_tutorial.DTOs
{
    public record MovieDto(Guid Id, string Title, string Genre, DateTimeOffset ReleaseDate, double Rating);
        
}
