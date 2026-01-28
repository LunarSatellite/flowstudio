using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("OrderByClauses", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class OrderByClause : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Field { get; set; } = string.Empty;
        public SortDirection Direction { get; set; } = SortDirection.Ascending;
    }
}