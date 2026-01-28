
namespace Aurora.FlowStudio.Entity.DTO.Base
{
    public class NextAction
    {
        public string Type { get; set; } = string.Empty; // Input, Button, Menu, End, etc.
        public string? Label { get; set; }
        public string? Value { get; set; }
        public Dictionary<string, object> Properties { get; set; } = new();
    }
}