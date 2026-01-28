using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Identity
{
    public class UpdateUserProfileRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? JobTitle { get; set; }
        public string? Department { get; set; }
        public string? TimeZone { get; set; }
        public string? Language { get; set; }
    }
}