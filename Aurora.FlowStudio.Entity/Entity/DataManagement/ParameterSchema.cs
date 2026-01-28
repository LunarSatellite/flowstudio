using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("ParameterSchemas", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class ParameterSchema : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DataType { get; set; } = string.Empty;
        public ParameterDirection Direction { get; set; } = ParameterDirection.Input;
        public bool IsNullable { get; set; } = true;
        public object? DefaultValue { get; set; }
    }
}