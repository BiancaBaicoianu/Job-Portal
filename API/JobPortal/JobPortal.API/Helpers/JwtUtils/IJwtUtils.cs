using JobPortal.API.Models;

namespace JobPortal.API.Helpers.JwtUtils
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public int ValidateJwtToken(string token);
    }
}
