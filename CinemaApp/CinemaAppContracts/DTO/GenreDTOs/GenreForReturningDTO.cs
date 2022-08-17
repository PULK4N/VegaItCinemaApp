using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.DTO.GenreDTOs
{
    public class GenreForReturningDTO
    {
        public GenreForReturningDTO()
        {

        }
        public GenreForReturningDTO(Genre genre)
        {
            Id = genre.Id;
            //TODO: check to see if empty string causes an error here
            Name = genre.Name;
        }
        public GenreForReturningDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
