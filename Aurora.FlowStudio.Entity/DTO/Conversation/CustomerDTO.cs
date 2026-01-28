using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class CustomerDTO : BaseDTO
    {
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Company { get; set; }
        public CustomerStatus Status { get; set; }
        public CustomerType Type { get; set; }
        public string? Source { get; set; }
        public DateTime? FirstContactDate { get; set; }
        public DateTime? LastContactDate { get; set; }
        public List<string> Tags { get; set; } = new();
        public Dictionary<string, object> CustomFields { get; set; } = new();
    }
}