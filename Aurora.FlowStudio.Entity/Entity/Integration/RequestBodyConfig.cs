using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("RequestBodyConfigs", Schema = "integration")]

        public class RequestBodyConfig
    {
        public ContentType ContentType { get; set; } = ContentType.JSON;
        [MaxLength(200)]
        public string? Template { get; set; }
        public Dictionary<string, object> DefaultData { get; set; } = new();
        public bool IsFormData { get; set; } = false;
    }
}