using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Conversation
{
    public class CustomerAuthRequest
    {
        public CustomerAuthType AuthType { get; set; }
        public string? Identifier { get; set; }
        public string? OtpCode { get; set; }
        public string? Token { get; set; }
        public Dictionary<string, string> AuthData { get; set; } = new();
    }
}