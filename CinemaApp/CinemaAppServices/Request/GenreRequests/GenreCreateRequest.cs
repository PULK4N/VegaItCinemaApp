using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Request.GenreRequests
{
    public class GenreCreateRequest
    {
        public GenreCreateRequest(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
