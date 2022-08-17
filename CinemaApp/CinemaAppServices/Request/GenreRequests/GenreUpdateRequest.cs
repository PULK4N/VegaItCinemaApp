using CinemaAppServices.Entities;

namespace CinemaAppContracts.Request.GenreRequests
{
    public class GenreUpdateRequest
    {
        public GenreUpdateRequest()
        {

        }
        public GenreUpdateRequest(Genre genre)
        {
            Id = genre.Id;
            //TODO: check to see if empty string causes an error here
            Name = genre.Name;
        }
        public GenreUpdateRequest(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
