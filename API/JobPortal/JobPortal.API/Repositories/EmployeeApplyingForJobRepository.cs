using JobPortal.API.Data;
using JobPortal.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.API.Repositories
{
    public class EmployeeApplyingForJobRepository : GenericRepository<EmployeeApplyingForJob>, IEmployeeApplyingForJobRepository
    {
        public EmployeeApplyingForJobRepository(PortalContext context) : base(context)
        {
        }
        public async Task<EmployeeApplyingForJob?> GetByBothIds(int EmployeeId, int JobId)
        {
            return await _context.EmployeesApplyingForJobs.Where(a => a.EmployeeId == EmployeeId
            && a.JobId == JobId).FirstOrDefaultAsync();
        }
    }
}
