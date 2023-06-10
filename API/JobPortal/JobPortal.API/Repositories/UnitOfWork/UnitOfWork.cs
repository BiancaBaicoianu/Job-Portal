using JobPortal.API.Data;
using JobPortal.API.Models;
using JobPortal.API.Repositories;

namespace JobPortal.API.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private PortalContext context;
        public UnitOfWork(PortalContext context)
        {
            this.context = context;

            Employees = new EmployeeRepository(context);
            Users = new UserRepository(context);
            Jobs = new JobRepository(context);
            Companies = new CompanyRepository(context);
            JobOffers = new JobOfferRepository(context);


        }
        public IEmployeeRepository Employees
        {
            get;
            private set;
        }
        public ICompanyRepository Companies
        {
            get;
            private set;
        }
        public IJobOfferRepository JobOffers
        {
            get;
            private set;
        }
        public IUserRepository Users
        {
            get;
            private set;
        }
        public IJobRepository Jobs
        {
            get;
            private set;
        }

        public void Dispose()
        {
            context.Dispose();
        }
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
