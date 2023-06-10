using JobPortal.API.Models;

namespace JobPortal.API.Repositories
{
    public interface IJobOfferRepository : IGenericRepository<JobOffer>
    {
        Task<JobOffer> GetJobOfferById(int id);
    }
}
