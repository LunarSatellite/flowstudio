using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class CreateCustomerRequest
    {
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }
        public CustomerType Type { get; set; } = CustomerType.Individual;
        public string? Source { get; set; }
        public Dictionary<string, object> CustomFields { get; set; } = new();
    }
}