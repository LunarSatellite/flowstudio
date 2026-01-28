namespace Aurora.FlowStudio.Entity.Entity.Base.Interfaces
{
    /// <summary>
    /// Interface for entities that support data import/export
    /// </summary>
    public interface IImportExportable
    {
        string ExportToJson();
        string ExportToXml();
        string ExportToCsv();
        void ImportFromJson(string json);
    }
}
