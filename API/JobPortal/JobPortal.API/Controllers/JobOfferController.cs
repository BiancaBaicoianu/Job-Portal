using JobPortal.API.Models.DTOs;
using JobPortal.API.Models;
using JobPortal.API.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class JobOfferController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobOfferController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/JobOffers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobOfferDTO>>> GetJobs()
        {
            var jobs = (await _unitOfWork.JobOffers.GetAll()).Select(a => new JobOfferDTO(a)).ToList();
            return jobs;
        }

        // GET: api/JobOffer/id
        [HttpGet("{id}")]
        public async Task<ActionResult<JobOfferDTO>> GetJob(int id)
        {
            var job = await _unitOfWork.JobOffers.GetById(id);

            if (job == null)
            {
                return NotFound("Job with this id doesn't exist");
            }

            return new JobOfferDTO(job);
        }

        // PUT: api/JobOffer/id
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutJob(int id, JobOfferDTO jobOffer)
        {
            var jobOfferInDb = await _unitOfWork.JobOffers.GetById(id);

            if (jobOfferInDb == null)
            {
                return NotFound("Job with this id doesn't exist");
            }
            jobOfferInDb.Benefits = jobOffer.Benefits;
            jobOfferInDb.NoOfPositions = jobOffer.NoOfPositions;
            jobOfferInDb.MinimumSalary = jobOffer.MinimumSalary;

            await _unitOfWork.JobOffers.Update(jobOfferInDb);
            _unitOfWork.Save();

            return Ok();
        }


        // POST: api/JobOffers
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<JobOfferDTO>> PostJob(JobOfferDTO job)

        {
            var jobOfferToAdd = new JobOffer(job);

            //await _unitOfWork.Jobs.Create(jobOfferToAdd);
            //_unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/JobOffers/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteJobOffer(int id)
        {
            var jobOfferInDb = await _unitOfWork.JobOffers.GetById(id);

            if (jobOfferInDb == null)
            {
                return NotFound("Job offer with this id doesn't exist");
            }

            //await _unitOfWork.Jobs.Delete(jobOfferInDb);
            _unitOfWork.Save();

            return Ok();
        }
    }
}
