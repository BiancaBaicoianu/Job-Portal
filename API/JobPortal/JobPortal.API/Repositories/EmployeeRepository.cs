using JobPortal.API.Data;
using JobPortal.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.API.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(PortalContext context) : base(context)
        {
        }
        public async Task<Employee?> GetEmployeeByFullName(string firstName, string lastName)
        {
            if (firstName == null || lastName == null)
            {
                return null;
            }

            return await _context.Employees.Where(a => a.FirstName.ToLower().Equals(firstName.ToLower())
            && a.LastName.ToLower().Equals(lastName.ToLower())).FirstOrDefaultAsync();
        }
        public async Task Update(Company companyinDb)
        {
            _context.Companies.Update(companyinDb);
            await _context.SaveChangesAsync();
        }
        //get all (include)
        public async Task<IEnumerable<Employee>> GetAllWithInclude()
        {
            return await _context.Employees.Include(a => a.User).ToListAsync();
        }
    }
}
