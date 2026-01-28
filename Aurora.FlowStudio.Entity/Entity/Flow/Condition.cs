using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("Conditions", Schema = "flow")]

        public class Condition
    {
        [MaxLength(200)]
        public string Field { get; set; } = string.Empty;
        public ConditionComparison Comparison { get; set; } = ConditionComparison.Equals;
        public object Value { get; set; } = new();
    }
}