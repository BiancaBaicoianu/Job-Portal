using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using JobPortal.API.Models.DTOs;

namespace JobPortal.API.Models
{
    // One to Many relationship between Company and JobOffer
    [Table("Company")]
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = "";
        public string CompanyDescription { get; set; } = "";
        public string CompanyAddress { get; set; } = "";
        public string CompanyPhone { get; set; } = "";
        public string CompanyEmail { get; set; } = "";
        public DateTime CompanyCreationDate { get; set; } = DateTime.Now;
        public IEnumerable<JobOffer> JobsOffers { get; set; } = new HashSet<JobOffer>();
        public Company()
        {

        }
        public Company(CompanyDTO company)
        {
            this.CompanyName = company.CompanyName;
            this.CompanyDescription = company.CompanyDescription;
            this.CompanyAddress = company.CompanyAddress;
            this.CompanyPhone = company.CompanyPhone;
            this.CompanyEmail = company.CompanyEmail;
        }

    }
}
