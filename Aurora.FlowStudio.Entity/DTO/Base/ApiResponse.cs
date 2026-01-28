
namespace Aurora.FlowStudio.Entity.DTO.Base
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? RequestId { get; set; }
    }
}