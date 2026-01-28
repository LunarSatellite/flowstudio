using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("APIResponseSchemas", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class APIResponseSchema : TenantBaseEntity
    {
        [MaxLength(4000)]
        public string ContentType { get; set; } = string.Empty;
        public Dictionary<string, string> Schema { get; set; } = new();
    }
}