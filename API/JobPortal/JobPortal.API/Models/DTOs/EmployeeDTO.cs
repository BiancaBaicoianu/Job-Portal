namespace JobPortal.API.Models.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public string? PhoneNumber { get; set; }
        public int Age { get; set; }

        public EmployeeDTO(Employee employee)
        {
            this.EmployeeId = employee.EmployeeId;
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.Email = employee.EmailAddress;
            //this.PhoneNumber = employee.PhoneNumber;
            this.Age = employee.Age;
        }
        public EmployeeDTO()
        {

        }
    }
}
