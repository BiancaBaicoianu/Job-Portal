using JobPortal.API.Helpers.JwtUtils;
using JobPortal.API.Helpers.Seeder;
using JobPortal.API.Repositories;
using JobPortal.API.Repositories.UnitOfWork;
using JobPortal.API.Services;

namespace JobPortal.API.Helpers.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IJobOfferRepository, JobOfferRepository>();
            services.AddTransient<IEmployeeApplyingForJobRepository, EmployeeApplyingForJobRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }

         public static IServiceCollection AddServices(this IServiceCollection services)
         {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            return services;
         }
       
        public static IServiceCollection AddSeeders(this IServiceCollection services)
        {
            services.AddScoped<UserSeeder>();
            return services;
        }
        public static IServiceCollection AddUtils(this IServiceCollection services)
        {
            services.AddScoped<IJwtUtils,JwtUtils.JwtUtils>();
            return services;
        }
    }
}
