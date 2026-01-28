using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("QueryOptimizations", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class QueryOptimization : TenantBaseEntity
    {
        public bool EnableQueryCache { get; set; } = true;
        public bool EnableIndexHints { get; set; } = false;
        public bool EnableParallelExecution { get; set; } = false;
        public Dictionary<string, object> CustomSettings { get; set; } = new();
    }
}