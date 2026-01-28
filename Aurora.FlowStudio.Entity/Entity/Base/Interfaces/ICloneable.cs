namespace Aurora.FlowStudio.Entity.Entity.Base.Interfaces
{
    using Aurora.FlowStudio.Entity.Entity.Base;

    /// <summary>
    /// Interface for entities that support cloning/duplication
    /// </summary>
    public interface ICloneable<T> where T : BaseEntity
    {
        T Clone();
        T CloneWithNewId();
    }
}
