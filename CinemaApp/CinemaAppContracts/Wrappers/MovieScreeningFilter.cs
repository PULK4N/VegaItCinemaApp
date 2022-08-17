using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Wrappers
{
    public class MovieScreeningFilter : PaginationFilter
    {
        public ICollection<char> FirstLetters { get; set; } = new List<char>();
        public string Name { get; set; } = string.Empty;
    }
}