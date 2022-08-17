using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Entities
{
    public class Genre : BaseEntity
    {
        //[Required(ErrorMessage = "Name is required")]
        //[StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public String Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
