using JobPortal.API.Models;

namespace JobPortal.API.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByEmailAndHashedPassword(string email, string hash);
    }
}