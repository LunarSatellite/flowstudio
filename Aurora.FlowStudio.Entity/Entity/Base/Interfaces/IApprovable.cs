namespace Aurora.FlowStudio.Entity.Entity.Base.Interfaces
{
    /// <summary>
    /// Interface for entities that support approval workflow
    /// </summary>
    public interface IApprovable
    {
        string? ApprovalStatus { get; set; }
        Guid? ApprovedBy { get; set; }
        DateTime? ApprovedAt { get; set; }
        string? ApprovalComments { get; set; }
        Guid? RejectedBy { get; set; }
        DateTime? RejectedAt { get; set; }
        string? RejectionReason { get; set; }
    }
}
