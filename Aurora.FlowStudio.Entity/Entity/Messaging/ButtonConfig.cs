using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("ButtonConfigs", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class ButtonConfig : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        public ButtonType Type { get; set; } = ButtonType.Postback;
        [MaxLength(200)]
        public string? Value { get; set; }
        [MaxLength(2000)]
        public string? Url { get; set; }
        [MaxLength(200)]
        public string? PhoneNumber { get; set; }
        [MaxLength(2000)]
        public string? IconUrl { get; set; }
        public Dictionary<string, object> Payload { get; set; } = new();
    }
}