using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("QueryDefinitions", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class QueryDefinition : TenantBaseEntity
    {
        // SQL-like structure
        public List<string> SelectFields { get; set; } = new();
        [MaxLength(200)]
        public string? FromTable { get; set; }
        public List<JoinClause> Joins { get; set; } = new();
        public List<WhereClause> WhereConditions { get; set; } = new();
        public List<string> GroupByFields { get; set; } = new();
        public List<WhereClause> HavingConditions { get; set; } = new();
        public List<OrderByClause> OrderBy { get; set; } = new();
        public int? Limit { get; set; }
        public int? Offset { get; set; }

        // Raw Query (for complex queries)
        [MaxLength(200)]
        public string? RawQuery { get; set; }

        // API Query
        [MaxLength(2000)]
        public string? APIPath { get; set; }
        [MaxLength(200)]
        public string? HTTPMethod { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new();
        public Dictionary<string, string> QueryParams { get; set; } = new();
        [MaxLength(4000)]
        public string? RequestBody { get; set; }
    }
}