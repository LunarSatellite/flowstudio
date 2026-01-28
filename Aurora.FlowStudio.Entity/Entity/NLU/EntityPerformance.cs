using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("EntityPerformances", Schema = "nlu")]

    [Index(nameof(CreatedAt))]

    public class EntityPerformance : TenantBaseEntity
    {
        [MaxLength(200)]
        public string EntityName { get; set; } = string.Empty;
        public double Precision { get; set; }
        public double Recall { get; set; }
        public double F1Score { get; set; }
        public int CorrectExtractions { get; set; }
        public int MissedExtractions { get; set; }
        public int IncorrectExtractions { get; set; }
    }
}