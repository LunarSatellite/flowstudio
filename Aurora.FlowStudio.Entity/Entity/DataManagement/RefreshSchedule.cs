using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.DataManagement
{
    [Table("RefreshSchedules", Schema = "data")]

    [Index(nameof(CreatedAt))]

    public class RefreshSchedule : TenantBaseEntity
    {
        public ScheduleType Type { get; set; } = ScheduleType.Daily;
        [MaxLength(200)]
        public string? CronExpression { get; set; }
        public int? IntervalMinutes { get; set; }
        public TimeSpan? TimeOfDay { get; set; }
        public List<DayOfWeek> DaysOfWeek { get; set; } = new();
        public bool Enabled { get; set; } = true;
    }
}