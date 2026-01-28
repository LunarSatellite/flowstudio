using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("ValidationRules", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class ValidationRule : TenantBaseEntity
    {
        [MaxLength(200)]
        public string? Pattern { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public object? MinValue { get; set; }
        public object? MaxValue { get; set; }
        [MaxLength(4000)]
        public string? ErrorMessage { get; set; }
    }
}