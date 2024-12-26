using System.Runtime.InteropServices.JavaScript;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_work_01.Models;
using Web_work_01.Services;

namespace Web_work_01.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeedepartmentController : Controller
    {
        private readonly ExamContext _examContext;
        
        public EmployeedepartmentController(ExamContext examContext)
        {
            _examContext = examContext;
        }
        [HttpGet("Getemployeedepartmentcol")]
        public JsonResult Getemployeedepartmentcol()
        {
            var employees = (
            from e in _examContext.Employees
            join d in _examContext.Departments
            on e.DepartmentId equals d.DepartmentId into ed
            from department in ed.DefaultIfEmpty() // 支援 Left Join，允許無部門的員工
            select new
            {
                //EmployeeId = e.EmployeeId,
                name = e.Name,
                Department = department != null ? department.department : "未分配" // 處理空部門
            }).ToList();

            return Json(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _examContext.Departments.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        [HttpPut("{employeeId}")]
        public IActionResult UpdateDepartmentByEmployee(string employeeId, [FromBody] UpdateDepartmentRequest requestData)
        {
            var employee = _examContext.Employees.FirstOrDefault(e => e.Name == employeeId);
            if (employee == null)
            {
                return NotFound(new { message = "找不到該員工" });
            }
            // 更新人員所屬部門資訊
            employee.DepartmentId = requestData.DepartmentID;
            DepartmentService.UpdateEmployeeDepartmentID(employee);
            // 返回成功訊息
            return Json(new { 
                status = 200,
                message = "success"
            });
        }        
    };
    
}
