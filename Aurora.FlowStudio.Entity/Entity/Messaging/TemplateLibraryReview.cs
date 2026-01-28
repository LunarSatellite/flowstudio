using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Messaging
{
    [Table("TemplateLibraryReviews", Schema = "msg")]

    [Index(nameof(CreatedAt))]

    public class TemplateLibraryReview : BaseEntity
    {
        public Guid TemplateLibraryId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        [MaxLength(200)]
        public string? Comment { get; set; }
        public bool IsVerified { get; set; } = false;

        // Navigation properties
        public TemplateLibrary TemplateLibrary { get; set; } = null!;
    }
}