using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServices.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {

        }
        public User(string username, string email, DateTime dateOfBirth)
        {
            UserName = username;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public DateTime DateOfBirth { get; set; }
        public Boolean IsBlocked { get; set; } = false;

    }
}
