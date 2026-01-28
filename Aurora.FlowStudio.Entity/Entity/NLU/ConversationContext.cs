using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("ConversationContexts", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class ConversationContext : TenantBaseEntity
    {
        public Guid ConversationId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Value { get; set; } = string.Empty;
        public int Lifespan { get; set; } = 5; // Number of turns
        [Column(TypeName = "datetime2")]
        public DateTime ExpiresAt { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new();
    }
}