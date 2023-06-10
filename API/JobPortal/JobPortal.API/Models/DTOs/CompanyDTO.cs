namespace JobPortal.API.Models.DTOs
{
    public class CompanyDTO
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = "";
        public string CompanyDescription { get; set; } = "";
        public string CompanyAddress { get; set; } = "";
        public string CompanyPhone { get; set; } = "";
        public string CompanyEmail { get; set; } = "";
        public DateTime CompanyCreationDate { get; set; } = DateTime.Now;
        public CompanyDTO(Company company)
        {
            CompanyId = company.CompanyId;
            CompanyName = company.CompanyName;
            CompanyDescription = company.CompanyDescription;
            CompanyAddress = company.CompanyAddress;
            CompanyPhone = company.CompanyPhone;
            CompanyEmail = company.CompanyEmail;
            CompanyCreationDate = company.CompanyCreationDate;
        }
        public CompanyDTO()
        {
        }

    }
}