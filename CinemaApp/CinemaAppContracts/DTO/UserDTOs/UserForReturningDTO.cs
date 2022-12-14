using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.DTO.UserDTOs
{
    public class UserForReturningDTO
    {
        public UserForReturningDTO()
        {

        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsBlocked { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
