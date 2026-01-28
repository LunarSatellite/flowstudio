namespace Aurora.FlowStudio.Entity.Entity.Base.Interfaces
{
    /// <summary>
    /// Interface for entities that support comments
    /// </summary>
    public interface ICommentable
    {
        int CommentCount { get; set; }
        DateTime? LastCommentedAt { get; set; }
        bool AllowComments { get; set; }
    }
}
