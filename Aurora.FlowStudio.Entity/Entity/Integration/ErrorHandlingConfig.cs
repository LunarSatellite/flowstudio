using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("ErrorHandlingConfigs", Schema = "integration")]

        public class ErrorHandlingConfig
    {
        [MaxLength(2000)]
        public string? ErrorCodePath { get; set; }
        [MaxLength(2000)]
        public string? ErrorMessagePath { get; set; }
        public Dictionary<string, string> ErrorMappings { get; set; } = new();
        public bool ThrowOnError { get; set; } = true;
    }
}