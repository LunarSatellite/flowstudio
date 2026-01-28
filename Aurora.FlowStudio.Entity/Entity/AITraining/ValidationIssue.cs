using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AITraining
{
    [Table("ValidationIssues", Schema = "train")]

    [Index(nameof(CreatedAt))]

    public class ValidationIssue : TenantBaseEntity
    {
        public IssueSeverity Severity { get; set; }
        [MaxLength(200)]
        public string IssueType { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? SuggestedFix { get; set; }
        [MaxLength(100)]
        public string? SampleId { get; set; }
    }
}