using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Filters
{
    public class ServiceGenreFilter : ServicePaginationFilter
    {
        public ICollection<char> FirstLetters { get; set; } = new List<char>();
        public string Name { get; set; } = "";
    }
}
