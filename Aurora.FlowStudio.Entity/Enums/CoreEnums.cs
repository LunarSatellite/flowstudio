namespace Aurora.FlowStudio.Entity.Enums
{
    /// <summary>
    /// Tenant status enumeration
    /// </summary>
    public enum TenantStatus
    {
        Active,
        Suspended,
        Trial,
        Expired,
        Cancelled
    }

    /// <summary>
    /// Tenant subscription plan
    /// </summary>
    public enum TenantPlan
    {
        Trial,
        Starter,
        Professional,
        Enterprise,
        Custom
    }

    /// <summary>
    /// Setting type for tenant settings
    /// </summary>
    public enum SettingType
    {
        String,
        Number,
        Boolean,
        Json,
        Encrypted
    }

    /// <summary>
    /// Sort direction
    /// </summary>
    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
