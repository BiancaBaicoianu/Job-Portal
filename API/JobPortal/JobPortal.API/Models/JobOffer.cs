using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using JobPortal.API.Models.DTOs;

namespace JobPortal.API.Models
{
    [Table("JobOffer")]
    public class JobOffer
    {
        private JobOfferDTO job;

        public JobOffer()
        {

        }
        public JobOffer(JobOfferDTO job)
        {
            this.job = job;
        }

        [Key]
        public int JobOfferId { get; set; }
        public int NoOfPositions { get; set; }
        public int MinimumSalary { get; set; }
        public string Benefits { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
