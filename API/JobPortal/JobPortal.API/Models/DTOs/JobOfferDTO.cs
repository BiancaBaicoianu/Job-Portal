namespace JobPortal.API.Models.DTOs
{
    public class JobOfferDTO
    {
        public int JobOfferId { get; set; }
        public int NoOfPositions { get; set; }
        public int MinimumSalary { get; set; }
        public string Benefits { get; set; }
        public JobOfferDTO(JobOffer joboffer)
        {
            JobOfferId = joboffer.JobOfferId;
            NoOfPositions = joboffer.NoOfPositions;
            MinimumSalary = joboffer.MinimumSalary;
            Benefits = joboffer.Benefits;
        }
        public JobOfferDTO()
        {
        }
    }
}
