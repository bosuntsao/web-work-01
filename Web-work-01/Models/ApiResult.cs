namespace Web_work_01.Models
{
    public class ApiResult
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }
    }
}
