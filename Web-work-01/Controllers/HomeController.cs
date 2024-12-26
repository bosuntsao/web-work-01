using System.Diagnostics;
using Azure.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Web_work_01.Models;


namespace Web_work_01.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly SignInManager<ExamContext> _signInManager;
        private readonly ExamContext _ExamContext;

        public HomeController(ILogger<HomeController> logger, ExamContext examContext)
        {
            _logger = logger;
            _ExamContext = examContext;
        }

        public void loginsource()
        {
            var Name = User.Identity.Name;  // 用戶名
            var department = User.FindFirst("Department")?.Value;
            ViewBag.Name = Name;
            ViewBag.Department = department;
        }
        
        [HttpPost]
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult login()
        {
            if (User.Identity.IsAuthenticated)
            {
                // 已登入
                return View("Index", "_Layout");
            }
            return View();
        }
        [Authorize]
        public IActionResult loginview()
        {
            loginsource();
            return View();
        }

        public IActionResult employeedepartmentcol()
        {
            loginsource();
            return View();
        }

        [HttpGet("employeedepartmentcol/departmentlist")]
        public IActionResult Departmentlist()
        {
            loginsource();
            return View();
        }

        [HttpGet("employeedepartmentcol/departmentlist/departmentset")]
        public IActionResult Departmentset()
        {
            loginsource();
            return View();
        }

        public IActionResult employeeset()
        {
            loginsource();
            return View();
        }

        public IActionResult departmentcol()
        {
            loginsource();
            return View();
        }

        
        [HttpPost]
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login"); // 登出後返回登入頁
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
