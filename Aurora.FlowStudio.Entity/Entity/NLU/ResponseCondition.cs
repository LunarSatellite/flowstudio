using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.NLU
{
    [Table("ResponseConditions", Schema = "nlu")]

    [Index(nameof(CreatedAt))]

    public class ResponseCondition : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Expression { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string AlternativeContent { get; set; } = string.Empty;
    }
}