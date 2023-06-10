using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using JobPortal.API.Models.Enums;
using JobPortal.API.Models.DTOs;

namespace JobPortal.API.Models
{
    [Table("User")]
    public class User
    {
        private UserDto user;

        public User(UserDto user)
        {
            this.user = user;
        }

        [Key]
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string? PhoneNumber { get; set; }
        public Role Role { get; set; } = Role.user;
        //public string RefreshToken { get; set; }
        //public DateTime Created { get; set; }
        //public DateTime Expires { get; set; }
        public User()
        {

        }

        public User(int employeeId, string email, string passwordHash, string passwordSalt, Role role)
        {
            this.EmployeeId = employeeId;
            this.Email = email;
            this.PasswordHash = passwordHash;
            this.PasswordSalt = passwordSalt;
            this.Role = role;
        }

        public ICollection<JobOffer> JobOffers { get; set; }

    }
}
