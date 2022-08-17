using CinemaAppServices.Entities;

namespace CinemaAppContracts.Request
{
    public class UserDTO
    {
        public UserDTO()
        {

        }
        public UserDTO(User user)
        {
            Id = user.Id;
            Username = user.UserName;
            Password = user.PasswordHash;
            DateOfBirth = user.DateOfBirth;
            Email = user.Email;
            EmailConfirmed = user.EmailConfirmed;
            IsBlocked = user.IsBlocked;
        }

        public UserDTO(Guid id, string username, string password, DateTime dateOfBirth, string email, bool emailConfirmed, bool isBlocked)
        {
            Id = id;
            Username = username;
            Password = password;
            DateOfBirth = dateOfBirth;
            Email = email;
            EmailConfirmed = emailConfirmed;
            IsBlocked = isBlocked;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Email { get; set; }
        public Boolean EmailConfirmed { get; set; } = false;
        public Boolean IsBlocked { get; set; } = false;
    }
}
