using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_work_01.Models;
using Web_work_01.Services;

namespace Web_work_01.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly ExamContext _context;

        public EmployeeController(ExamContext context)
        {
            _context = context;
        }
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {                
                return BadRequest("Invalid employee data.");
            }
            EmployeeService.Insert(employee);

            return Json(new
            {
                status = 200,
                message = "success"
            });
        }

        [HttpDelete("delete/{name}")]
        public async Task<IActionResult> DeleteEmployee(string name)
        {
            Employee employee = await _context.Employees.FirstOrDefaultAsync(e => e.Name == name);
            if (employee == null)
            {
                return NotFound("Invalid employee data.");
            }
            EmployeeService.Delete(employee);

            return Json(new
            {
                status = 200,
                message = "success"
            });
        }
    }
}
