using JobPortal.API.Models;
using JobPortal.API.Models.DTOs;
using JobPortal.API.Models.Enums;

namespace JobPortal.API.Services
{
    public interface IAuthenticationService
    {
        Task<Token?> Authenticate(UserLoginDTO? user);
        Task<User> Register(UserRegisterDTO user);
        string GenerateSalt();
        string HashPassword(string password, string salt);
        object GetById(int userId);
        
        //object GetById(Guid userId);
    }
}
