using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("QueryConfigs", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class QueryConfig : TenantBaseEntity
    {
        public int MaxRows { get; set; } = 1000;
        public int TimeoutSeconds { get; set; } = 30;
        public bool EnablePagination { get; set; } = true;
        public int DefaultPageSize { get; set; } = 100;
        public bool EnableFiltering { get; set; } = true;
        public bool EnableSorting { get; set; } = true;
        public bool EnableAggregation { get; set; } = true;
        public QueryOptimization Optimization { get; set; } = new();
    }
}