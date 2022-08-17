using Microsoft.AspNetCore.Http;

namespace CinemaAppContracts.DTO.MovieDTOs
{
    public class MovieForCreatingDTO
    {
        public MovieForCreatingDTO()
        {
        }
        public MovieForCreatingDTO(string name, string originalName, float durationMinutes, ICollection<Guid> genreIds, IFormFile posterImage)
        {
            Name = name;
            OriginalName = originalName;
            DurationMinutes = durationMinutes;
            GenreIds = genreIds;
            PosterImage = posterImage;
        }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public float DurationMinutes { get; set; }
        public ICollection<Guid> GenreIds { get; set; } = new List<Guid>();
        public IFormFile PosterImage { get; set; }
    }
}
