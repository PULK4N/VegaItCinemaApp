using CinemaAppContracts.DTO.GenreDTOs;
using CinemaAppContracts.DTO.MovieScreeningDTOs;


namespace CinemaAppContracts.DTO.MovieDTOs
{
    public class MovieForReturningDTO
    {
        public MovieForReturningDTO()
        {
        }
        public MovieForReturningDTO(Guid id, string name, string originalName, float durationMinutes, ICollection<GenreForReturningDTO> genres, float rating, Guid posterImageId)
        {
            Id = id;
            Name = name;
            OriginalName = originalName;
            DurationMinutes = durationMinutes;
            Genres = genres;
            Rating = rating;
            PosterImageId = posterImageId;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public float DurationMinutes { get; set; }
        public ICollection<GenreForReturningDTO> Genres { get; set; } = new List<GenreForReturningDTO>();
        public virtual ICollection<MovieScreeningForReturningDTO> MovieScreenings { get; set; }
        public float Rating { get; set; }
        public Guid PosterImageId { get; set; }
    }
}
