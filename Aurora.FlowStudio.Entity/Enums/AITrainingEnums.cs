namespace Aurora.FlowStudio.Entity.Enums
{
    public enum DatasetStatus
    {
        Draft,
        Ready,
        Processing,
        Invalid,
        Archived
    }

    public enum DatasetType
    {
        Training,
        Validation,
        Test,
        Production,
        IntentClassification
    }

    public enum DataSplit
    {
        Train,
        Training,
        Validation,
        Test
    }

    public enum DeploymentEnvironment
    {
        Development,
        Staging,
        Production,
        Testing
    }

    public enum DeploymentStatus
    {
        Pending,
        Deploying,
        Active,
        Failed,
        Rollback,
        Decommissioned
    }

    public enum ExperimentRunStatus
    {
        Pending,
        Running,
        Completed,
        Failed,
        Cancelled
    }

    public enum ExperimentStatus
    {
        Draft,
        Active,
        Running,
        Completed,
        Archived
    }

    public enum IssueSeverity
    {
        Info,
        Warning,
        Error,
        Critical
    }

    public enum ModelRegistryStatus
    {
        Registered,
        Approved,
        Deprecated,
        Archived
    }

    public enum TrainingJobStatus
    {
        Pending,
        Initializing,
        Training,
        Evaluating,
        Completed,
        Failed,
        Cancelled,
        Stopping
    }

    public enum ValidationStatus
    {
        Pending,
        Passed,
        Failed,
        Warning
    }

    public enum ValidationType
    {
        Automatic,
        Schema,
        DataQuality,
        Distribution,
        Completeness,
        Custom
    }
}

/// <summary>
/// Model type for training
/// </summary>
public enum ModelType
{
    Classification,
    Regression,
    Clustering,
    IntentClassification,
    EntityRecognition,
    Custom,
    ChatCompletion
}

/// <summary>
/// Data source for training
/// </summary>
public enum DataSourceEnum
{
    Manual,
    Imported,
    Generated,
    Synthetic,
    External
}
