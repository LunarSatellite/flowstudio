using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("EntityAnnotations", Schema = "nlu")]

    [Index(nameof(CreatedAt))]

    public class EntityAnnotation : TenantBaseEntity
    {
        public Guid EntityId { get; set; }
        [MaxLength(200)]
        public string EntityName { get; set; } = string.Empty;
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
        [MaxLength(4000)]
        public string Text { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Alias { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}