using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("CustomerTags", Schema = "conv")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class CustomerTag : TenantBaseEntity
    {
        public Guid CustomerId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Color { get; set; }
        [MaxLength(200)]
        public string? Category { get; set; }

        // Navigation properties
        public Customer Customer { get; set; } = null!;
    }
}