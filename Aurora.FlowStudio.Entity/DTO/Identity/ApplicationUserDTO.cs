using System;
using System.Collections.Generic;

namespace Aurora.FlowStudio.DTO.Identity
{
    public class ApplicationUserDTO
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Timezone { get; set; }
        public string Language { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public string LastLoginIp { get; set; }
        public int LoginCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool EmailNotifications { get; set; }
        public bool SmsNotifications { get; set; }
        public bool PushNotifications { get; set; }
        public List<string> Roles { get; set; }
    }
}
