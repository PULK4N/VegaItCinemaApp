using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Request.GenreRequests
{
    public class GenreResponse
    {
        public GenreResponse()
        {

        }
        public GenreResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public String Name { get; set; }
    }
}
