namespace Aurora.FlowStudio.Entity.Enums
{
    /// <summary>
    /// User status enumeration
    /// </summary>
    public enum UserStatus
    {
        Active,
        Inactive,
        Suspended,
        Locked,
        PendingVerification,
        PendingApproval
    }

    /// <summary>
    /// Role level hierarchy
    /// </summary>
    public enum RoleLevel
    {
        SuperAdmin = 0,      // Platform super admin
        TenantAdmin = 1,     // Company/Tenant admin
        Manager = 2,         // Department manager
        Supervisor = 3,      // Team supervisor
        FlowDesigner = 4,    // Creates and manages flows
        Agent = 5,           // Customer service agent
        Analyst = 6,         // Analytics and reporting
        User = 7,            // Basic user
        ReadOnly = 8         // Read-only access
    }

    /// <summary>
    /// Permission scope
    /// </summary>
    public enum PermissionScope
    {
        System,     // Super admin only
        Tenant,     // Tenant-wide
        Department, // Department level
        Team,       // Team level
        Own         // Only own resources
    }

    /// <summary>
    /// Session status
    /// </summary>
    public enum SessionStatus
    {
        Active,
        Expired,
        Revoked,
        LoggedOut
    }

    /// <summary>
    /// User activity action types
    /// </summary>
    public enum ActivityAction
    {
        Create,
        Read,
        Update,
        Delete,
        Execute,
        Publish,
        Archive,
        Restore,
        Export,
        Import,
        Share,
        Login,
        Logout,
        PasswordChange,
        SettingsChange,
        Custom
    }

    /// <summary>
    /// Log severity levels
    /// </summary>
    public enum LogSeverity
    {
        Info,
        Warning,
        Error,
        Critical,
        Security
    }

    /// <summary>
    /// FIDO2 Credential type
    /// </summary>
    public enum CredentialType
    {
        PlatformAuthenticator,      // Built-in (Touch ID, Face ID, Windows Hello)
        CrossPlatformAuthenticator, // External (YubiKey, Security Key)
        Hybrid                      // Hybrid authenticators
    }

    /// <summary>
    /// FIDO2 Credential status
    /// </summary>
    public enum CredentialStatus
    {
        Active,
        Revoked,
        Suspended,
        Expired
    }
}

    /// <summary>
    /// User activity action types
    /// </summary>
    public enum ActivityAction
    {
        Login,
        Logout,
        View,
        Create,
        Update,
        Delete,
        Export,
        Import,
        Download,
        Upload,
        Share,
        PasswordChange,
        ProfileUpdate,
        AccessDenied
    }
