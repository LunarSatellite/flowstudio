using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("PersonalizationConfigs", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class PersonalizationConfig : TenantBaseEntity
    {
        public bool EnablePersonalization { get; set; } = true;
        public List<PersonalizationRule> Rules { get; set; } = new();
        [MaxLength(4000)]
        public string? FallbackContent { get; set; }
    }
}