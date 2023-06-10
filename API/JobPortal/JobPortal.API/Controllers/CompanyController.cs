using JobPortal.API.Models.DTOs;
using JobPortal.API.Models;
using JobPortal.API.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // END-POINTS

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetCompanies()
        {
            var companies = (await _unitOfWork.Companies.GetAll()).Select(a => new CompanyDTO(a)).ToList();
            return companies;
        }

        // GET: api/Company/id
        [HttpGet("{id}"), Authorize(Roles = "user")]
        public async Task<ActionResult<CompanyDTO>> GetCompany(int id)
        {
            var company = await _unitOfWork.Companies.GetById(id);

            if (company == null)
            {
                return NotFound("Company with this id doesn't exist");
            }

            return new CompanyDTO(company);
        }


        // PUT: api/Companies/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutCompany(int id, CompanyDTO company)
        {
            var companyInDb = await _unitOfWork.Companies.GetById(id);

            if (companyInDb == null)
            {
                return NotFound("Company with this id doesn't exist");
            }

            companyInDb.CompanyName = company.CompanyName;
            companyInDb.CompanyDescription = company.CompanyDescription;


            await _unitOfWork.Employees.Update(companyInDb);
            _unitOfWork.Save();

            return Ok();
        }

        // POST: api/Companies
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CompanyDTO>> PostCompany(CompanyDTO company)
        {

            var companyToAdd = new Company(company);

            await _unitOfWork.Companies.Create(companyToAdd);
            _unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/Companies/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCompanies(int id)
        {
            var companyInDb = await _unitOfWork.Companies.GetById(id);

            if (companyInDb == null)
            {
                return NotFound("Company with this id doesn't exist");
            }

            await _unitOfWork.Companies.Delete(companyInDb);
            _unitOfWork.Save();

            return Ok();
        }
    }
}

