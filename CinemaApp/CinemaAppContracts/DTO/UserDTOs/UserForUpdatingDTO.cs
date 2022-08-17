using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.DTO.UserDTOs
{
    public class UserForUpdatingDTO
    {
        public UserForUpdatingDTO()
        {

        }
        public UserForUpdatingDTO(string username, string email, DateTime dateOfBirth)
        {
            Username = username;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
