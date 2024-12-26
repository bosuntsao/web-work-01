using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Web_work_01.Models;
using Web_work_01.Models.Departments;
using Web_work_01.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_work_01.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ExamContext _examContext;

        public DepartmentController(ExamContext examContext)
        {
            _examContext = examContext;
        }

        [HttpGet]
        public List<JObject> Get()
        {
            List<JObject> departments = (
            from d in _examContext.Departments
            select new JObject
            {
                {"departmentId", d.DepartmentId },
                {"departmentName", d.department }
            }).ToList();

            return departments;
        }
        [HttpGet("Departments")]
        public JsonResult Departments()
        {
            var Departments = _examContext.Departments.Where(x => x.department != null|| x.ParentID==null).ToList();

            return new JsonResult(Departments);
        }


        [HttpGet("Level")]        
        public JsonResult Level()
        {
            var Departments = _examContext.Departments.ToList();
            
            return new JsonResult(Departments);
        }


        [HttpGet("departmentlist")]        
        public JsonResult departmentlist()
        {
            var departments = _examContext.Departments.Select(d => new { d.DepartmentId,d.department})
                               .ToList();
            return new JsonResult(departments);
        }        

        [HttpPut("{DepartmentID}")]
        public IActionResult UpdateDepartment(int departmentId, [FromBody] DepartmentRequest requestData)
        {
            var department = _examContext.Departments.FirstOrDefault(e => e.DepartmentId == departmentId);

            // 更新人員所屬部門資訊
            department.department = requestData.department;
            DepartmentService.UpdateDepartment(department);
            // 返回成功訊息
            return new JsonResult(new
            {
                status = 200,
                message = "success"
            });
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest("Invalid department data.");
            }
            if(department.ParentID == null)
            {
                department.ParentID = 0;
            }

            DepartmentService.Insert(department);

            return new JsonResult(new
            {
                status = 200,
                message = "success"
            });
        }

        [HttpDelete("delete/{id}")]

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            Department department = await _examContext.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound(new { message = "Department not found" });
            }
            DepartmentService.Delete(department);

            return new JsonResult(new
            {
                status = 200,
                message = "success"
            });
        }
    }
}
