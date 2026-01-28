using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class UpdateCustomerRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Company { get; set; }
        public string? JobTitle { get; set; }
        public CustomerStatus? Status { get; set; }
        public Dictionary<string, object>? CustomFields { get; set; }
    }
}