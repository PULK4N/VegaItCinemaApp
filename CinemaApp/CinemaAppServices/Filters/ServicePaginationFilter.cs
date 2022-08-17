using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Filters
{
    public class ServicePaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ServicePaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public ServicePaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
