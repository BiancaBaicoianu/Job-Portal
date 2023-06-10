using JobPortal.API.Models.DTOs;

namespace JobPortal.API.Models
{
    public class EmployeeApplyingForJob
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int JobId { get; set; }
        public Job? Job { get; set; }

        public EmployeeApplyingForJob()
        {
        }
        public EmployeeApplyingForJob(EmployeeApplyingForJobDTO employeeApplyingForJob)
        {
            this.EmployeeId = employeeApplyingForJob.EmployeeId;
            this.JobId = employeeApplyingForJob.JobId;
        }
    }
}

