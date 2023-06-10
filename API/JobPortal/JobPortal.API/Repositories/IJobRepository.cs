using JobPortal.API.Models;

namespace JobPortal.API.Repositories
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<Job?> GetJobByTitle(string title);
    }
}