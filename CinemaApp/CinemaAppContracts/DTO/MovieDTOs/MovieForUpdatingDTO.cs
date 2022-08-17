using CinemaAppContracts.DTO.GenreDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.DTO.MovieDTOs
{
    public class MovieForUpdatingDTO
    {
        public MovieForUpdatingDTO()
        {
        }
        public MovieForUpdatingDTO(Guid id, string name, string originalName, 
            float durationMinutes, ICollection<Guid> genresToAdd, ICollection<Guid> genresToRemove, 
            IFormFile posterImage)
        {
            Name = name;
            OriginalName = originalName;
            DurationMinutes = durationMinutes;
            GenreIdsToAdd = genresToAdd;
            GenreIdsToRemove = genresToRemove;
            PosterImage = posterImage;
        }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public float DurationMinutes { get; set; }
        public IFormFile PosterImage { get; set; }
        public ICollection<Guid> GenreIdsToAdd { get; set; } = new List<Guid>();
        public ICollection<Guid> GenreIdsToRemove { get; set; } = new List<Guid>();
    }
}
