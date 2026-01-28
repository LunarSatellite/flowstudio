namespace Aurora.FlowStudio.Entity.Enums
{
    public enum ButtonType
    {
        Reply,
        Url,
        Call,
        Postback,
        Share
    }

    public enum EntityMatchMode
    {
        Exact,
        Fuzzy,
        Regex,
        Pattern
    }

    public enum EntityStatus
    {
        Active,
        Draft,
        Archived
    }

    public enum EntityType
    {
        System,
        Custom,
        Regex,
        List,
        Composite
    }

    public enum EvaluationType
    {
        CrossValidation,
        HoldOut,
        Manual,
        TestSet
    }

    public enum IntentStatus
    {
        Active,
        Draft,
        Archived,
        Testing
    }

    public enum ModelStatus
    {
        Draft,
        Training,
        Trained,
        Deployed,
        Failed,
        Archived
    }

    public enum PhraseSource
    {
        Manual,
        Generated,
        Imported,
        Synthetic
    }

    public enum PhraseType
    {
        Template,
        Example,
        Pattern
    }

    public enum ResponseType
    {
        Text,
        RichContent,
        Custom,
        Redirect
    }

    public enum TrainingAlgorithm
    {
        NeuralNetwork,
        SVM,
        RandomForest,
        Transformer
    }

    public enum TrainingStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Failed,
        Cancelled
    }
}
