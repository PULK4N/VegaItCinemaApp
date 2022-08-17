using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Request.UserRequests
{
    public class UserUpdateRequest
    {
        public UserUpdateRequest()
        {

        }
        public UserUpdateRequest(Guid id, string username, string email, DateTime dateOfBirth)
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
