using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("MenuItems", Schema = "flow")]

    [Index(nameof(TenantId), nameof(IsDeleted))]
[Index(nameof(CreatedAt))]

    public class MenuItem : TenantBaseEntity
    {
        public Guid MenuId { get; set; }
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(4000)]
        public string? Description { get; set; }
        [MaxLength(2000)]
        public string? IconUrl { get; set; }
        [MaxLength(2000)]
        public string? ImageUrl { get; set; }
        public int Order { get; set; } = 0;
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set; } = true;

        // Item Type & Target
        public MenuItemType Type { get; set; } = MenuItemType.Flow;
        public Guid? TargetFlowId { get; set; } // Links to a Flow
        public Guid? TargetMenuId { get; set; } // Links to another Menu (SubMenu)
        [MaxLength(2000)]
        public string? ExternalUrl { get; set; } // External link
        [MaxLength(200)]
        public string? CustomAction { get; set; } // Custom action identifier

        // Display Configuration
        [MaxLength(4000)]
        public string? DisplayText { get; set; } // Text shown to user
        [MaxLength(100)]
        public string? ShortCode { get; set; } // Quick access code (e.g., "1", "A")
        public List<string> Keywords { get; set; } = new(); // NLU keywords for matching
        [MaxLength(200)]
        public string? Color { get; set; }
        public Dictionary<string, object> Style { get; set; } = new();

        // Conditional Display
        public bool HasConditions { get; set; } = false;
        public ConditionConfig? Conditions { get; set; }

        // Access Control
        public bool RequiresAuthentication { get; set; } = false;
        public List<string> RequiredRoles { get; set; } = new();
        public List<string> RequiredPermissions { get; set; } = new();

        // Analytics
        public int ClickCount { get; set; } = 0;
        [Column(TypeName = "datetime2")]
        public DateTime? LastClickedAt { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();

        // Navigation properties
        public Menu Menu { get; set; } = null!;
        public Flow? TargetFlow { get; set; }
        public Menu? TargetMenu { get; set; }
    }
}