namespace Aurora.FlowStudio.Entity.Entity.Base.Interfaces
{
    /// <summary>
    /// Interface for entities that support full-text search
    /// </summary>
    public interface ISearchable
    {
        string GetSearchableContent();
        Dictionary<string, int> GetSearchWeights();
        List<string> GetSearchTags();
    }
}
