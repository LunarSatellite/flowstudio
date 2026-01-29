using System;
using System.Collections.Generic;
using Aurora.FlowStudio.Entity.DTO.Base;
using Aurora.FlowStudio.Entity.Enums;

namespace Aurora.FlowStudio.Entity.DTO.Tenant
{
    public class CostAlertDTO : TenantBaseDTO
    {
        public string Name { get; set; }
        public AlertType Type { get; set; }
        public bool IsEnabled { get; set; }
        
        public UsageType? UsageType { get; set; }
        public decimal ThresholdAmount { get; set; }
        public int? ThresholdQuantity { get; set; }
        public decimal ThresholdPercent { get; set; }
        public AlertPeriod Period { get; set; }
        
        public bool SendEmail { get; set; }
        public bool SendSMS { get; set; }
        public bool ShowInDashboard { get; set; }
        public List<string> NotifyEmails { get; set; }
        
        public DateTime? LastTriggeredAt { get; set; }
        public int TriggerCount { get; set; }
        public decimal CurrentAmount { get; set; }
        public int CurrentQuantity { get; set; }
    }
}
