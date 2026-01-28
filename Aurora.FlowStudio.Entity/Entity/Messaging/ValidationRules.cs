using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("ValidationRuleses", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class ValidationRules : TenantBaseEntity
    {
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public List<string> RequiredVariables { get; set; } = new();
        public List<string> ForbiddenWords { get; set; } = new();
        [MaxLength(100)]
        public string? CustomValidationScript { get; set; }
    }
}