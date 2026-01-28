using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("ListItemConfigs", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class ListItemConfig : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Subtitle { get; set; }
        [MaxLength(2000)]
        public string? ImageUrl { get; set; }
        [MaxLength(200)]
        public string? Value { get; set; }
        public List<ButtonConfig> Buttons { get; set; } = new();
        public Dictionary<string, object> Payload { get; set; } = new();
    }
}