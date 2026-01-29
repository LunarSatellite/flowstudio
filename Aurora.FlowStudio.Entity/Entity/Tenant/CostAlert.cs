using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.Tenant
{
    /// <summary>
    /// Proactive cost alerts to prevent surprise bills
    /// Notifies clients when approaching spending limits
    /// </summary>
    [Table("CostAlerts")]
    public class CostAlert : TenantBaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [Required]
        public AlertType Type { get; set; }
        
        public bool IsEnabled { get; set; }
        
        // Threshold settings
        public UsageType? UsageType { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ThresholdAmount { get; set; }
        
        public int? ThresholdQuantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ThresholdPercent { get; set; }
        
        [Required]
        public AlertPeriod Period { get; set; }
        
        // Alert channels
        public bool SendEmail { get; set; }
        
        public bool SendSMS { get; set; }
        
        public bool ShowInDashboard { get; set; }
        
        [Column(TypeName = "jsonb")]
        public List<string> NotifyEmails { get; set; }
        
        // Tracking
        public DateTime? LastTriggeredAt { get; set; }
        
        public int TriggerCount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentAmount { get; set; }
        
        public int CurrentQuantity { get; set; }
    }
}
