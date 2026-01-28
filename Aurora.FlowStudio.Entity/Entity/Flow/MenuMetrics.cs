using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Flow
{
    [Table("MenuMetricses", Schema = "flow")]

        public class MenuMetrics
    {
        public int TotalViews { get; set; }
        public int UniqueVisitors { get; set; }
        public int TotalSelections { get; set; }
        public double AverageTimeSpentSeconds { get; set; }
        public Dictionary<Guid, int> ItemSelectionCount { get; set; } = new();
        public Dictionary<string, object> CustomMetrics { get; set; } = new();
    }
}