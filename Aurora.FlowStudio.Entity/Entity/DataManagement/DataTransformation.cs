using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("DataTransformations", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class DataTransformation : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        public TransformationType Type { get; set; } = TransformationType.Map;
        public bool Enabled { get; set; } = true;
        public int Order { get; set; } = 0;
        public TransformationConfig Configuration { get; set; } = new();
    }
}