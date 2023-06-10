namespace JobPortal.API.Models.DTOs
{
    public class UserDto
    {
        private User user;


        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserDto(User user)
        {
            this.UserId = user.UserId;
            this.Username = user.Email.Substring(0,user.Email.IndexOf('@'));
            this.Email = user.Email;
            this.Password = user.PasswordHash;
        }
    }
}
