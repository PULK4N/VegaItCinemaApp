using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CinemaAppServices.Entities
{
    public class Movie : BaseEntity
    {
        //[Required(ErrorMessage = "Name is required")]
        //[StringLength(240, ErrorMessage = "Name can't be longer than 240 characters")]
        public String Name { get; set; }
        //[Required(ErrorMessage = "Original name is required")]
        //[StringLength(240, ErrorMessage = "Original name can't be longer than 240 characters")]
        public String OriginalName { get; set; }

        //[Required(ErrorMessage = "Movie duration is required")]
        public float DurationMinutes { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<MovieScreening> MovieScreenings { get; set; }
        public float Rating { get; set; }
        public Guid PosterImageId { get; set; }
    }
}
