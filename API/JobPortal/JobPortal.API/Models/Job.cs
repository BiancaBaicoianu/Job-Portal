using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using JobPortal.API.Models.DTOs;
using JobPortal.API.Models.Base;

namespace JobPortal.API.Models
{

    [Table("Job")]
    public class Job
    {
        private JobDTO job;
        public Job()
        {
        }

        [Key]
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; } 

        public IEnumerable<EmployeeApplyingForJob> EmployeesApplyingForJobs { get; set; } = new HashSet<EmployeeApplyingForJob>();
        public int JobOfferId { get; set; }
        public JobOffer? JobOffer { get; set; }


        public Job(JobDTO job)
        {
            this.JobId = job.JobId;
            this.JobTitle = job.JobTitle;
            this.JobDescription = job.JobDescription;
        }

    }
}
