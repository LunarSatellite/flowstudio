using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("IntentResponses", Schema = "nlu")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class IntentResponse : TenantBaseEntity
    {
        public Guid IntentId { get; set; }
        [MaxLength(4000)]
        public string Content { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Language { get; set; } = "en";
        public ResponseType Type { get; set; } = ResponseType.Text;
        public RichContentConfig? RichContent { get; set; }
        public int Priority { get; set; } = 0;
        public bool IsDefault { get; set; } = false;

        // Conditions
        public List<ResponseCondition> Conditions { get; set; } = new();

        // A/B Testing
        public int Weight { get; set; } = 100;
        public bool EnableABTesting { get; set; } = false;

        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Intent Intent { get; set; } = null!;
    }
}