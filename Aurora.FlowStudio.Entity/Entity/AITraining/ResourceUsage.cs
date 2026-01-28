using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("ResourceUsages", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class ResourceUsage : TenantBaseEntity
    {
        public double PeakCPUUsage { get; set; }
        public double AverageCPUUsage { get; set; }
        public double PeakMemoryUsageMB { get; set; }
        public double AverageMemoryUsageMB { get; set; }
        public double? PeakGPUUsage { get; set; }
        public double? AverageGPUUsage { get; set; }
        public double? PeakGPUMemoryUsageMB { get; set; }
        public double? AverageGPUMemoryUsageMB { get; set; }
        public double? TotalComputeHours { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? EstimatedCost { get; set; }
    }
}