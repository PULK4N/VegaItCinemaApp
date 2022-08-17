using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CinemaAppContracts.Wrappers
{
    public class MovieCustomerFilter : PaginationFilter
    {
        public MovieCustomerFilter()
        {
            if (MovieDay == DateTime.MinValue)
                MovieDay = DateTime.Now;
        }

        public DateTime MovieDay { get; set; }
        public ICollection<Guid> GenreIds { get; set; } = new List<Guid>();
        public bool SortAlphabetically { get; set; }
        public bool SortMovieScreeningsChronologically { get; set; }
    }
}
