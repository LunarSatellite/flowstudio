using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("ResponseConfigs", Schema = "integration")]

        public class ResponseConfig
    {
        public ContentType ExpectedContentType { get; set; } = ContentType.JSON;
        [MaxLength(2000)]
        public string? DataPath { get; set; } // JSONPath or XPath to extract data
        public Dictionary<string, string> FieldMappings { get; set; } = new();
        public ErrorHandlingConfig ErrorHandling { get; set; } = new();
    }
}