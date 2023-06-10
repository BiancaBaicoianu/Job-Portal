using JobPortal.API.Data;
using JobPortal.API.Models;
using JobPortal.API.Models.DTOs;
using JobPortal.API.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace JobPortal.API.Controllers
{
        [Route("api/[controller]")]
        //[Authorize]
        [ApiController]
        public class JobController : Controller
        {
            private readonly IUnitOfWork _unitOfWork;

            public JobController(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            // GET: api/Jobs  => READ
            [HttpGet]
            public async Task<ActionResult<IEnumerable<JobDTO>>> GetJobs()
            {
                var jobs = (await _unitOfWork.Jobs.GetAll()).Select(a => new JobDTO(a)).ToList();
                return jobs;
            }

            // GET: api/Jobs/id => READ
            [HttpGet("{id}")]
            public async Task<ActionResult<JobDTO>> GetJob(int id)
            {
                var job = await _unitOfWork.Jobs.GetById(id);

                if (job == null)
                {
                    return NotFound("Job with this id doesn't exist");
                }

                return new JobDTO(job);
            }

            // PUT: api/Jobs/id => UPDATE
            [HttpPut("{id}")]   
            //[Authorize(Roles = "admin")]
            public async Task<IActionResult> PutJob(int id, JobDTO job)
            {
                var jobInDb = await _unitOfWork.Jobs.GetById(id);

                if (jobInDb == null)
                {
                    return NotFound("Job with this id doesn't exist");
                }

                jobInDb.JobTitle = job.JobTitle;
                jobInDb.JobDescription = job.JobDescription;

                await _unitOfWork.Jobs.Update(jobInDb);
                _unitOfWork.Save();

                return Ok();
            }

            // POST: api/Jobs =>  CREATE
            [HttpPost]
            //[Authorize(Roles = "admin")]
            public async Task<ActionResult<JobDTO>> PostJob(JobDTO job)
            {
                var jobToAdd = new Job(job);

                await _unitOfWork.Jobs.Create(jobToAdd);
                _unitOfWork.Save();

                return Ok();
            }

            // DELETE: api/Jobs/id
            [HttpDelete("{id}")]
            //[Authorize(Roles = "admin")]
            public async Task<IActionResult> DeleteJob(int id)
            {
                var jobInDb = await _unitOfWork.Jobs.GetById(id);

                if (jobInDb == null)
                {
                    return NotFound("Job with this id doesn't exist");
                }

                await _unitOfWork.Jobs.Delete(jobInDb);
                _unitOfWork.Save();

                return Ok();
            }
        }
    }
