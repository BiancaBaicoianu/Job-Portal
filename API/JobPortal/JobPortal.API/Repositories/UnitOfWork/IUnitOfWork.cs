using JobPortal.API.Repositories;
namespace JobPortal.API.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        ICompanyRepository Companies { get; }
        IJobOfferRepository JobOffers { get; }
        IUserRepository Users { get; }
        IJobRepository Jobs { get; }
        int Save();
    }

}
