namespace Aurora.FlowStudio.Entity.Entity.Base.Interfaces
{
    /// <summary>
    /// Interface for entities that support real-time collaboration
    /// </summary>
    public interface ICollaborative
    {
        List<Guid> ActiveEditors { get; set; }
        Dictionary<Guid, DateTime> LastEditedBy { get; set; }
        bool IsCurrentlyBeingEdited { get; set; }
        string? LockToken { get; set; }
    }
}
