namespace Aurora.FlowStudio.Entity.Entity.Base.Interfaces
{
    /// <summary>
    /// Interface for entities that support versioning
    /// </summary>
    public interface IVersionable
    {
        int Version { get; set; }
        Guid? ParentId { get; set; }
        bool IsLatestVersion { get; set; }
        string? VersionLabel { get; set; }
    }
}
