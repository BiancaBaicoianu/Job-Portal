namespace JobPortal.API.Models.DTOs
{
        public class JobDTO
        {
            public int JobId { get; set; }
            public string JobTitle { get; set; }
            public string JobDescription { get; set; } 
            public JobDTO()
            {

            }
            public JobDTO(Job job)
                {
                    this.JobId = job.JobId;
                    this.JobTitle = job.JobTitle;
                    this.JobDescription = job.JobDescription;
                }
            
           }
    }
