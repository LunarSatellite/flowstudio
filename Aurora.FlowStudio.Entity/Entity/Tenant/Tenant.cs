using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Tenant
{
    /// <summary>
    /// B2B Customer - Company using your platform
    /// </summary>
    [Table("Tenants")]
    public class Tenant : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Domain { get; set; }
        
        [Required]
        public TenantStatus Status { get; set; }
        
        [Required]
        public TenantPlan Plan { get; set; }
        
        public DateTime? TrialEndsAt { get; set; }
        
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string ContactEmail { get; set; }
        
        [Phone]
        [MaxLength(20)]
        public string ContactPhone { get; set; }
        
        [MaxLength(500)]
        public string Address { get; set; }
        
        [MaxLength(100)]
        public string Country { get; set; }
        
        [MaxLength(50)]
        public string Timezone { get; set; }
    }
}
