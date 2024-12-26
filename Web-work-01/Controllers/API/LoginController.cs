using System.Security.Claims;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_work_01.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Web_work_01.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class LoginController : Controller
    {
        private readonly ExamContext _examContext;

        public LoginController(ExamContext examContext)
        {
            _examContext = examContext;
        }
        
        [HttpPost]
        
        public async Task<ApiResult> Login([FromBody] Employee value)
        {
            ApiResult apiResult = new ApiResult
            {
                Status = 403,
                Message = "Unauthorized",
                Result = false
            };

            
            //Cookie
            HttpContext.Response.Cookies.Append("AccountID", value.Name);
            string accountId = HttpContext.Request.Cookies["AccountID"];

            var user = await _examContext.Employees
                .SingleOrDefaultAsync(a => a.Name == value.Name && a.Password == value.Password);
            var userdepartment = await _examContext.Departments.SingleOrDefaultAsync(a =>a.DepartmentId==user.DepartmentId);
            if (user != null)
            {
                // 設置Claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("Department", userdepartment.department)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddDays(365),
                    IsPersistent = true // 設定為「記住我」
                };                
                // 登入
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                Console.WriteLine(HttpContext.Response.Headers["Set-Cookie"]);

                apiResult = new ApiResult()
                {
                    Status = 200,
                    Message = "success",
                    Result = true
                };
                
            }

            return apiResult;
        }
    }


}
