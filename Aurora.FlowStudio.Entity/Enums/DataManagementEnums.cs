namespace Aurora.FlowStudio.Entity.Enums
{
    public enum AggregationType
    {
        Sum,
        Average,
        Count,
        Min,
        Max,
        GroupBy
    }

    public enum CacheType
    {
        Memory,
        Distributed,
        Redis,
        None
    }

    public enum CacheEvictionPolicy
    {
        LRU,
        LFU,
        FIFO,
        TTL
    }

    public enum CheckSeverity
    {
        Info,
        Warning,
        Error,
        Critical
    }

    public enum ComparisonOperator
    {
        Equal,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Like,
        In,
        NotIn,
        Between,
        IsNull,
        IsNotNull
    }

    public enum DataOperation
    {
        Read,
        Write,
        Update,
        Delete,
        Execute
    }

    public enum DataSourceStatus
    {
        Active,
        Inactive,
        Error,
        Connecting
    }

    public enum DataSourceType
    {
        Database,
        API,
        File,
        Stream
    }

    public enum ExecutionStatus
    {
        Pending,
        Running,
        Completed,
        Success,
        Failed,
        Cancelled
    }

    public enum JoinType
    {
        Inner,
        Left,
        Right,
        Full,
        Cross
    }

    public enum LogicalOperator
    {
        And,
        Or,
        Not
    }

    public enum MaskingType
    {
        Full,
        Partial,
        Hash,
        Encrypt,
        Redact
    }

    public enum QualityAction
    {
        Allow,
        Warn,
        Block,
        Log
    }

    public enum QualityCheckType
    {
        Required,
        NotNull,
        Format,
        Range,
        UniqueConstraint,
        ForeignKey,
        Custom
    }

    public enum RefreshStrategy
    {
        Manual,
        Scheduled,
        OnDemand,
        Incremental,
        Full
    }

    public enum RelationshipType
    {
        OneToOne,
        OneToMany,
        ManyToOne,
        ManyToMany
    }

    public enum ScheduleType
    {
        Once,
        Daily,
        Weekly,
        Monthly,
        Cron
    }

    public enum TransformationType
    {
        Map,
        Filter,
        Aggregate,
        Join,
        Union,
        Custom
    }
}
