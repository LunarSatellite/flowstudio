using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Identity
{
    public class FIDO2CredentialDTO : BaseDTO
    {
        public string CredentialIdBase64 { get; set; } = string.Empty;
        public string? DeviceName { get; set; }
        public CredentialType Type { get; set; }
        public List<string> Transports { get; set; } = new();
        public DateTime EnrolledAt { get; set; }
        public DateTime? LastUsedAt { get; set; }
        public int UsageCount { get; set; }
        public bool IsPrimary { get; set; }
        public CredentialStatus Status { get; set; }
    }
}