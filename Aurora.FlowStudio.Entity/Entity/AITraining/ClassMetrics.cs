using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("ClassMetricses", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class ClassMetrics : TenantBaseEntity
    {
        public int Support { get; set; }
        public double Precision { get; set; }
        public double Recall { get; set; }
        public double F1Score { get; set; }
    }
}