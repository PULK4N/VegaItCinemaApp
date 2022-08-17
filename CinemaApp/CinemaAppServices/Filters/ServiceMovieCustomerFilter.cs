using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Filters
{
    public class ServiceMovieCustomerFilter : ServicePaginationFilter
    {
        public ServiceMovieCustomerFilter()
        {
            if (MovieDay == DateTime.MinValue)
                MovieDay = DateTime.Now;
        }
        public ServiceMovieCustomerFilter(DateTime dateTime, ICollection<Guid> genres, bool sortAlphabetically = false,
    bool sortMovieScreeningsChronologically = false, int pageNumber = 0, int pageSize = 0) : base(pageNumber, pageSize)
        {
            MovieDay = dateTime;
            GenreIds = genres;
            SortAlphabetically = sortAlphabetically;
            SortMovieScreeningsChronologically = sortMovieScreeningsChronologically;
        }

        public DateTime MovieDay { get; set; } = DateTime.Now;
        public ICollection<Guid> GenreIds { get; set; } = new List<Guid>();
        public bool SortAlphabetically { get; set; }
        public bool SortMovieScreeningsChronologically { get; set; }
    }
}
