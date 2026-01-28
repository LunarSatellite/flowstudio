using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("ResultColumns", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class ResultColumn : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? DisplayName { get; set; }
        [MaxLength(200)]
        public string DataType { get; set; } = string.Empty;
        public bool IsVisible { get; set; } = true;
        public int Order { get; set; } = 0;
    }
}