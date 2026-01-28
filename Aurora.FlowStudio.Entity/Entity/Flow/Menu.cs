using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("Menus", Schema = "flow")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class Menu : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        public MenuType Type { get; set; } = MenuType.Main;
        public MenuStatus Status { get; set; } = MenuStatus.Active;

        // Welcome Message
        [MaxLength(4000)]
        public string WelcomeMessage { get; set; } = string.Empty;
        public MessageFormat WelcomeMessageFormat { get; set; } = MessageFormat.PlainText;
        [MaxLength(2000)]
        public string? WelcomeMediaUrl { get; set; } // Image/Video for welcome screen

        // Menu Configuration
        public MenuDisplayStyle DisplayStyle { get; set; } = MenuDisplayStyle.List;
        public int MaxOptionsPerPage { get; set; } = 10;
        public bool AllowSearch { get; set; } = true;
        public bool AllowBackNavigation { get; set; } = true;
        [MaxLength(4000)]
        public string? BackButtonText { get; set; } = "Back";

        // Timeout Configuration
        public int TimeoutSeconds { get; set; } = 300; // 5 minutes
        public Guid? TimeoutFlowId { get; set; } // Flow to redirect on timeout
        [MaxLength(4000)]
        public string? TimeoutMessage { get; set; }

        // Analytics
        public MenuMetrics Metrics { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public ICollection<MenuItem> Items { get; set; } = new List<MenuItem>();
        public ICollection<Conversation.Conversation> Conversations { get; set; } = new List<Conversation.Conversation>();
    }
}