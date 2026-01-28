using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("RelationshipSchemas", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class RelationshipSchema : TenantBaseEntity
    {
        [MaxLength(200)]
        public string RelatedTable { get; set; } = string.Empty;
        [MaxLength(200)]
        public string LocalColumn { get; set; } = string.Empty;
        [MaxLength(200)]
        public string ForeignColumn { get; set; } = string.Empty;
        public RelationshipType Type { get; set; } = RelationshipType.OneToMany;
    }
}