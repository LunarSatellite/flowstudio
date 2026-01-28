using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("DataSourceSchemas", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class DataSourceSchema : TenantBaseEntity
    {
        public List<TableSchema> Tables { get; set; } = new();
        public List<ViewSchema> Views { get; set; } = new();
        public List<ProcedureSchema> Procedures { get; set; } = new();
        public List<APIEndpointSchema> Endpoints { get; set; } = new();
        [Column(TypeName = "datetime2")]
        public DateTime LastUpdated { get; set; }
    }
}