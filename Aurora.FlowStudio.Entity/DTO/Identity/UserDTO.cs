using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Identity
{
    public class UserDTO : BaseDTO
    {
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
        public Guid TenantId { get; set; }
        public string? TenantName { get; set; }
        public string? JobTitle { get; set; }
        public string? Department { get; set; }
        public UserStatus Status { get; set; }
        public bool FIDO2Enabled { get; set; }
        public bool HasFIDO2Credential { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public List<string> Roles { get; set; } = new();
        public List<string> Permissions { get; set; } = new();
    }
}