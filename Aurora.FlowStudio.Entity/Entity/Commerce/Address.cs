using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Commerce
{
    /// <summary>
    /// Address entity with GPS coordinates and comprehensive location data
    /// </summary>
    [Table("Addresses", Schema = "commerce")]
    [Index(nameof(TenantId), nameof(IsDeleted))]
    [Index(nameof(CreatedAt))]
    [Index(nameof(Latitude), nameof(Longitude))]
    [Index(nameof(Country), nameof(State), nameof(City))]
    public class Address : TenantBaseEntity
    {
        // Personal Information
        [Required]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string LastName { get; set; } = string.Empty;
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? Company { get; set; }
        
        // Address Lines
        [Required]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string Address1 { get; set; } = string.Empty;
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? Address2 { get; set; }
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? Address3 { get; set; }
        
        // Location Details
        [Required]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string City { get; set; } = string.Empty;
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? State { get; set; }
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? Province { get; set; }
        
        [Required]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string PostalCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string Country { get; set; } = string.Empty;
        
        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? CountryCode { get; set; } // ISO 3166-1 alpha-2 (US, GB, etc.)
        
        // GPS Coordinates
        [Column(TypeName = "decimal(10,8)")]
        public decimal? Latitude { get; set; } // Range: -90 to 90
        
        [Column(TypeName = "decimal(11,8)")]
        public decimal? Longitude { get; set; } // Range: -180 to 180
        
        [Column(TypeName = "decimal(8,2)")]
        public decimal? Altitude { get; set; } // Meters above sea level
        
        [Column(TypeName = "decimal(6,2)")]
        public decimal? Accuracy { get; set; } // GPS accuracy in meters
        
        [Column(TypeName = "datetime2")]
        public DateTime? GpsLastUpdated { get; set; }
        
        [MaxLength(2000)]
        [Column(TypeName = "nvarchar(2000)")]
        public string? GoogleMapsUrl { get; set; }
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? GooglePlaceId { get; set; }
        
        // Contact Information
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Phone]
        public string? Phone { get; set; }
        
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Phone]
        public string? AlternatePhone { get; set; }
        
        [MaxLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        [EmailAddress]
        public string? Email { get; set; }
        
        // Address Type & Classification
        public AddressType Type { get; set; } = AddressType.Shipping;
        
        [Required]
        public bool IsDefault { get; set; } = false;
        
        public bool IsBillingAddress { get; set; } = false;
        public bool IsShippingAddress { get; set; } = true;
        public bool IsResidential { get; set; } = true;
        public bool IsCommercial { get; set; } = false;
        
        // Verification & Validation
        public bool IsVerified { get; set; } = false;
        
        [Column(TypeName = "datetime2")]
        public DateTime? VerifiedAt { get; set; }
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? VerifiedBy { get; set; }
        
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? VerificationService { get; set; } // USPS, Google, etc.
        
        public bool IsPoBox { get; set; } = false;
        public bool IsMilitaryAddress { get; set; } = false;
        
        // Additional Location Info
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? District { get; set; }
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? County { get; set; }
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? Neighborhood { get; set; }
        
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? Landmark { get; set; }
        
        [MaxLength(4000)]
        [Column(TypeName = "nvarchar(4000)")]
        public string? DeliveryInstructions { get; set; }
        
        [MaxLength(4000)]
        [Column(TypeName = "nvarchar(4000)")]
        public string? Notes { get; set; }
        
        // Timezone
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? Timezone { get; set; } // IANA timezone (America/New_York)
        
        public int? UtcOffset { get; set; } // Offset in minutes
        
        // Formatting
        [MaxLength(4000)]
        [Column(TypeName = "nvarchar(4000)")]
        public string? FormattedAddress { get; set; } // Fully formatted single-line address
        
        [MaxLength(4000)]
        [Column(TypeName = "nvarchar(4000)")]
        public string? FormattedAddressLocal { get; set; } // Formatted in local language/format
        
        // Service Availability
        public bool IsDeliveryAvailable { get; set; } = true;
        public bool IsPickupAvailable { get; set; } = false;
        public bool IsRestrictedArea { get; set; } = false;
        
        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string? RestrictionReason { get; set; }
        
        // Relationships
        public Guid? CustomerId { get; set; }
        public Guid? UserId { get; set; }
        
        // Usage Tracking
        [Column(TypeName = "datetime2")]
        public DateTime? LastUsedAt { get; set; }
        
        public int UsageCount { get; set; } = 0;
        
        // External IDs
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? ExternalAddressId { get; set; }
        
        public Dictionary<string, string> ExternalIds { get; set; } = new();
    }
}
