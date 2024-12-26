using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_work_01.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {
            string filePath = "C:\\test\\a.txt";
            System.IO.Directory.CreateDirectory("C:\\test");
            System.IO.File.WriteAllText(filePath,"test");
            System.IO.File.ReadAllText(filePath);
        }
    }
}
