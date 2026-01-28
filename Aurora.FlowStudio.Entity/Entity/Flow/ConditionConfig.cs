using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("ConditionConfigs", Schema = "flow")]

        public class ConditionConfig
    {
        public List<Condition> Conditions { get; set; } = new();
        public ConditionOperator Operator { get; set; } = ConditionOperator.And;
    }
}