using CinemaAppContracts.Request.GenreRequests;
using CinemaAppContracts.Request.MovieScreeningRequests;
using CinemaAppServices.Entities;

namespace CinemaAppContracts.Request.MovieRequests
{
    public class MovieResponse
    {
        public MovieResponse()
        {

        }
        public MovieResponse(Guid id, string name, string originalName, float durationMinutes, ICollection<GenreResponse> genres, float rating, Guid posterImageId)
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
        public ICollection<GenreResponse> Genres { get; set; } = new List<GenreResponse>();
        public virtual ICollection<MovieScreeningResponse> MovieScreenings { get; set; } = new List<MovieScreeningResponse>();
        public float Rating { get; set; }
        public Guid PosterImageId { get; set; }
    }
}
