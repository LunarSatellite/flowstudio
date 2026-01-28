using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.AI
{
    [Table("RateLimitInfos", Schema = "ai")]

        public class RateLimitInfo
    {
        public int RequestsPerMinute { get; set; } = 60;
        public int RequestsPerDay { get; set; } = 10000;
        public int TokensPerMinute { get; set; } = 90000;
        public int ConcurrentRequests { get; set; } = 10;
    }
}