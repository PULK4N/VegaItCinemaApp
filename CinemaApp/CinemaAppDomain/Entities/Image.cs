using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Entities
{
    public class Image : BaseEntity
    {

        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public byte[] Data { get; set; }
    }
}
