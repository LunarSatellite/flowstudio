namespace Aurora.FlowStudio.Entity.Entity.Base.Interfaces
{
    /// <summary>
    /// Interface for entities that support bookmarking/favoriting
    /// </summary>
    public interface IBookmarkable
    {
        int BookmarkCount { get; set; }
        bool IsBookmarkedByCurrentUser { get; set; }
    }
}
