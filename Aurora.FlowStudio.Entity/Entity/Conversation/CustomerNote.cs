using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Conversation
{
    [Table("CustomerNotes", Schema = "conv")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class CustomerNote : TenantBaseEntity
    {
        public Guid CustomerId { get; set; }
        public Guid AuthorId { get; set; }
        [MaxLength(4000)]
        public string Content { get; set; } = string.Empty;
        public NoteType Type { get; set; } = NoteType.General;
        public bool IsPinned { get; set; } = false;

        // Navigation properties
        public Customer Customer { get; set; } = null!;
        public Core.User Author { get; set; } = null!;
    }
}