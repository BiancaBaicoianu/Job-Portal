namespace JobPortal.API.Models.DTOs
{
    public class EmployeeApplyingForJobDTO
    {
        public int EmployeeId { get; set; }

        public int JobId { get; set; }
        public EmployeeApplyingForJobDTO(EmployeeApplyingForJob employeeApplyingForJob)
        {
            this.EmployeeId = employeeApplyingForJob.EmployeeId;
            this.JobId = employeeApplyingForJob.JobId;
        }
        public EmployeeApplyingForJobDTO()
        {
        }
    }
}
