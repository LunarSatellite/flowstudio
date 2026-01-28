using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("APIEndpointSchemas", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class APIEndpointSchema : TenantBaseEntity
    {
        [MaxLength(2000)]
        public string Path { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Method { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public List<APIParameterSchema> Parameters { get; set; } = new();
        public APIResponseSchema? Response { get; set; }
    }
}