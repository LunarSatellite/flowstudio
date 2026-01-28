using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("Customers", Schema = "conv")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Customer : TenantBaseEntity
    {
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? PhoneNumber { get; set; }
        [MaxLength(200)]
        public string? FirstName { get; set; }
        [MaxLength(200)]
        public string? LastName { get; set; }
        public string? FullName => $"{FirstName} {LastName}".Trim();
        [MaxLength(200)]
        public string? Company { get; set; }
        [MaxLength(200)]
        public string? JobTitle { get; set; }
        [MaxLength(2000)]
        public string? AvatarUrl { get; set; }
        public CustomerStatus Status { get; set; } = CustomerStatus.Active;
        public CustomerType Type { get; set; } = CustomerType.Individual;
        [MaxLength(200)]
        public string? Source { get; set; }
        [MaxLength(200)]
        public string? ReferralSource { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? FirstContactDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? LastContactDate { get; set; }
        [MaxLength(200)]
        public string? TimeZone { get; set; }
        [MaxLength(200)]
        public string? Language { get; set; }
        [MaxLength(200)]
        public string? Country { get; set; }
        [MaxLength(200)]
        public string? City { get; set; }
        [MaxLength(200)]
        public string? Address { get; set; }
        [MaxLength(100)]
        public string? PostalCode { get; set; }
        public Dictionary<string, object> CustomFields { get; set; } = new();
        public Dictionary<string, object> Preferences { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();
        public CustomerSegments Segments { get; set; } = new();

        // Navigation properties
        public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
        public ICollection<CustomerTag> Tags { get; set; } = new List<CustomerTag>();
        public ICollection<CustomerNote> Notes { get; set; } = new List<CustomerNote>();
        public ICollection<CustomerActivity> Activities { get; set; } = new List<CustomerActivity>();
        public ICollection<CustomerAuthentication> Authentications { get; set; } = new List<CustomerAuthentication>();
    }
}