using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.DTO.GenreDTOs
{
    public class GenreForCreatingDTO
    {
        public GenreForCreatingDTO(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
