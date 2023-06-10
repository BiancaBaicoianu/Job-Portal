using JobPortal.API.Models.DTOs;
using JobPortal.API.Models;
using JobPortal.API.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employees = (await _unitOfWork.Employees
                .GetAll()).Select(a => new EmployeeDTO(a)).ToList();
            return employees;
        }

        // GET: api/Employees/id
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
        {
            var employee = await _unitOfWork.Employees.GetById(id);

            if (employee == null)
            {
                return NotFound("Employee with this id doesn't exist");
            }

            return new EmployeeDTO(employee);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDTO employee)
        {
            var employeeInDb = await _unitOfWork.Employees.GetById(id);

            if (employeeInDb == null)
            {
                return BadRequest("Employee with this id doesn't exist");
            }

            employeeInDb.FirstName = employee.FirstName;
            employeeInDb.LastName = employee.LastName;
            employeeInDb.EmailAddress = employee.Email;
            //employeeInDb.PhoneNumber = employee.PhoneNumber;


            await _unitOfWork.Employees.Update(employeeInDb);
            _unitOfWork.Save();

            return Ok();
        }

        // POST: api/Employees
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeDTO employee)
        {

            var employeeToAdd = new Employee(employee);

            await _unitOfWork.Employees.Create(employeeToAdd);
            _unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/Employees/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employeeInDb = await _unitOfWork.Employees.GetById(id);

            if (employeeInDb == null)
            {
                return NotFound("Employee with this id doesn't exist");
            }

            await _unitOfWork.Employees.Delete(employeeInDb);
            _unitOfWork.Save();

            return Ok();
        }
    }
}

