using JobPortal.API.Data;
using JobPortal.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.API.Repositories
{
    public class JobOfferRepository : GenericRepository<JobOffer>, IJobOfferRepository
    {
        public JobOfferRepository(PortalContext context) : base(context)
        {
        }
        public async Task<JobOffer> GetJobOfferById(int id)
        {
            return await _context.JobOffers.Where(a => a.JobOfferId == id).FirstOrDefaultAsync();
        }
    }
}
