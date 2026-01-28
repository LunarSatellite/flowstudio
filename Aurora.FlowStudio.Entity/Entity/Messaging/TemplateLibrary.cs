using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("TemplateLibraries", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class TemplateLibrary : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Category { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Industry { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public bool IsPublic { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        public int DownloadCount { get; set; } = 0;
        public double Rating { get; set; } = 0;
        public List<string> Tags { get; set; } = new();
        [MaxLength(2000)]
        public string? ThumbnailUrl { get; set; }
        [MaxLength(2000)]
        public string? PreviewUrl { get; set; }
        [MaxLength(200)]
        public string TemplateData { get; set; } = string.Empty; // JSON template

        // Navigation properties
        public ICollection<TemplateLibraryReview> Reviews { get; set; } = new List<TemplateLibraryReview>();
    }
}