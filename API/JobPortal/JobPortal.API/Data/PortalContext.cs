using JobPortal.API.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobPortal.API.Data
{

    // context = parte din entity frame
    public class PortalContext : DbContext  // DbContext = clasa din entity frame
    {
        //Tabels
        public DbSet<User> Users { get; set; }  // Users = numele tabelei
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<EmployeeApplyingForJob> EmployeesApplyingForJobs { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public PortalContext(DbContextOptions<PortalContext> options) : base(options)   // constructor
        {
        }
        
            // are prioritate, fiind ovveride
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // cheie compusa
                modelBuilder.Entity<EmployeeApplyingForJob>()
                            .HasKey(mr => new { mr.EmployeeId, mr.JobId });

                // Many to Many : Employee - Job
                modelBuilder.Entity<EmployeeApplyingForJob>()
                            .HasOne(mr => mr.Employee)
                            .WithMany(m => m.EmployeesApplyingForJobs)
                            .HasForeignKey(mr => mr.EmployeeId);
                modelBuilder.Entity<EmployeeApplyingForJob>()
                            .HasOne(mr => mr.Job)
                            .WithMany(r => r.EmployeesApplyingForJobs)
                            .HasForeignKey(mr => mr.JobId);

                //One to Many : Company - JobOffer
                modelBuilder.Entity<JobOffer>()
                            .HasOne(mr => mr.Company)
                            .WithMany(m => m.JobsOffers)
                            .HasForeignKey(mr => mr.CompanyId);

                // One to One : Employee - User
                modelBuilder.Entity<Employee>()
                            .HasOne(mr => mr.User)
                            .WithOne(r => r.Employee)
                            .HasForeignKey<User>("EmployeeId");

                // One to One : Job - JobOffer
                modelBuilder.Entity<JobOffer>()
                            .HasOne(mr => mr.Job)
                            .WithOne(r => r.JobOffer)
                            .HasForeignKey<JobOffer>(mr => mr.JobId);


                //modelBuilder.AddConfigurations();
                base.OnModelCreating(modelBuilder);


            }

        }
        

    }

