using CinemaAppServices.Entities;

namespace CinemaAppContracts.DTO.GenreDTOs
{
    public class GenreForUpdatingDTO
    {
        public GenreForUpdatingDTO()
        {

        }
        public GenreForUpdatingDTO(Genre genre)
        {
            //TODO: check to see if empty string causes an error here
            Name = genre.Name;
        }
        public GenreForUpdatingDTO(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
