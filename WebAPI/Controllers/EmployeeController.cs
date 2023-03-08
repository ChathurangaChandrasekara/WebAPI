using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IWebHostEnvironment env)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _env = env;
        }

        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employeeRepository.GetEmployees());
        }
        [HttpGet]
        [Route("GetEmployeeByID/{Id}")]
        public async Task<IActionResult> GetEmpByID(int Id)
        {
            return Ok(await _employeeRepository.GetEmployeeByID(Id));
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post(Employee emp)
        {
            var result = await _employeeRepository.InsertEmployee(emp);
            if (result.EmployeeID == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> Put(Employee emp)
        {
            await _employeeRepository.UpdateEmployee(emp);
            return Ok("Updated Successfully");
        }
        [HttpDelete]
        [Route("DeleteEmployee")]
        public JsonResult Delete(int id)
        {
            var result = _employeeRepository.DeleteEmployee(id);
            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    stream.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IActionResult> GetAllDepartmentNames()
        {
            return Ok(await _departmentRepository.GetDepartment());
        }
    }
}
