using CinemaAppContracts.Request.GenreRequests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Request.MovieRequests
{
    public class MovieUpdateRequest : MovieCreateRequest
    {
        public MovieUpdateRequest()
        {

        }
        public MovieUpdateRequest(string name, string originalName, 
            float durationMinutes, ICollection<Guid> genresToAdd, ICollection<Guid> genresToRemove, 
            float rating, IFormFile posterImage)
        {
            Name = name;
            OriginalName = originalName;
            DurationMinutes = durationMinutes;
            GenreIdsToAdd = genresToAdd;
            GenreIdsToRemove = genresToRemove;
            PosterImage = posterImage;
        }

        public ICollection<Guid> GenreIdsToAdd { get; set; } = new List<Guid>();
        public ICollection<Guid> GenreIdsToRemove { get; set; } = new List<Guid>();
    }
}
