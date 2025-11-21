namespace WebApplication_UN.Models
{
    public class ApiError
    {
        public string Message { get; set; }
        public string? Detail { get; set; }
        public string? TraceId { get; set; }
    }
}