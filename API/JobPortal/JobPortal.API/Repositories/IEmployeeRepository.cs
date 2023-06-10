using JobPortal.API.Models;

namespace JobPortal.API.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee?> GetEmployeeByFullName(string firstName, string lastName);
        Task Update(Company companyInDb);
    }
}
