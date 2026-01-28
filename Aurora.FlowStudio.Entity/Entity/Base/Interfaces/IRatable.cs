namespace Aurora.FlowStudio.Entity.Entity.Base.Interfaces
{
    /// <summary>
    /// Interface for entities that support rating
    /// </summary>
    public interface IRatable
    {
        double? AverageRating { get; set; }
        int RatingCount { get; set; }
        DateTime? LastRatedAt { get; set; }
    }
}
