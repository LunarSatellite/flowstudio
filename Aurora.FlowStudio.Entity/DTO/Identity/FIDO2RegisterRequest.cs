using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Identity
{
    public class FIDO2RegisterRequest
    {
        public string AttestationResponse { get; set; } = string.Empty;
        public string? DeviceName { get; set; }
        public bool SetAsPrimary { get; set; } = false;
    }
}