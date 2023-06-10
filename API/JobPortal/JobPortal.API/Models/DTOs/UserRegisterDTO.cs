namespace JobPortal.API.Models.DTOs
{
    public class UserRegisterDTO
    {
        public int EmployeeId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public UserRegisterDTO()
        {
        }

    }
}