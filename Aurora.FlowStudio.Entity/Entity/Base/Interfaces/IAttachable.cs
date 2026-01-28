namespace Aurora.FlowStudio.Entity.Entity.Base.Interfaces
{
    /// <summary>
    /// Interface for entities that support file attachments
    /// </summary>
    public interface IAttachable
    {
        int AttachmentCount { get; set; }
        long? TotalAttachmentSize { get; set; }
        List<Guid> AttachmentIds { get; set; }
    }
}
