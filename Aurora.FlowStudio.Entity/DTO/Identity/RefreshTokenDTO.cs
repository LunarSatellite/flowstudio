using System;

namespace Aurora.FlowStudio.DTO.Identity
{
    public class RefreshTokenDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string Token { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsUsed { get; set; }
        public string DeviceId { get; set; }
        public bool IsValid { get; set; }
    }
}
