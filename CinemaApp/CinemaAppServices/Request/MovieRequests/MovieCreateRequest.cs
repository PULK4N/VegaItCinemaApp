using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Request.MovieRequests
{
    public class MovieCreateRequest
    {
        public MovieCreateRequest()
        {

        }
        public MovieCreateRequest(string name, string originalName, float durationMinutes, ICollection<Guid> genreIds, IFormFile posterImage)
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
