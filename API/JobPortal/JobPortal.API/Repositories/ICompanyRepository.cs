using JobPortal.API.Models;

namespace JobPortal.API.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<Company?> GetCompanyByName(string name);
        Task<JobOffer?> GetJobOfferByJobId(int jobId);
    }
}