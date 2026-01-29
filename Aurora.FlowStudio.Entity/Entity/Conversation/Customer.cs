using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aurora.FlowStudio.Entity.Base;

namespace Aurora.FlowStudio.Entity.Conversation
{
    /// <summary>
    /// End-user (your client's customer)
    /// </summary>
    [Table("Customers")]
    public class Customer : TenantBaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }
        
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }
        
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; }
        
        [MaxLength(100)]
        public string ExternalId { get; set; }
        
        [MaxLength(10)]
        public string Language { get; set; }
        
        [MaxLength(100)]
        public string Country { get; set; }
        
        [MaxLength(50)]
        public string Timezone { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> Profile { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, object> Preferences { get; set; }
        
        [Required]
        public DateTime FirstContactAt { get; set; }
        
        public DateTime LastContactAt { get; set; }
        
        public int TotalConversations { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSpent { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal LifetimeValue { get; set; }
        
        [Column(TypeName = "jsonb")]
        public List<string> Tags { get; set; }
    }
}
