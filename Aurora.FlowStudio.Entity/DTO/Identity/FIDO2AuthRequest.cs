using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Identity
{
    public class FIDO2AuthRequest
    {
        public string AssertionResponse { get; set; } = string.Empty;
        public string CredentialId { get; set; } = string.Empty;
    }
}