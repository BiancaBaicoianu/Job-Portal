using JobPortal.API.Data;
using JobPortal.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.API.Repositories
{
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        public JobRepository(PortalContext context) : base(context)
        {
        }
        public async Task<Job?> GetJobByTitle(string title)
        {
            return await _context.Jobs.FirstOrDefaultAsync(job => job.JobTitle.ToLower() == title.ToLower());
        }
    }

}

