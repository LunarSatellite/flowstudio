using System;

namespace Aurora.FlowStudio.Entity.DTO.Base
{
    /// <summary>
    /// Base DTO with common fields
    /// </summary>
    public abstract class BaseDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
