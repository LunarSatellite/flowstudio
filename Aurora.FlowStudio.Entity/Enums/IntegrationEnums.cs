namespace Aurora.FlowStudio.Entity.Enums
{
    /// <summary>
    /// Connector type - 50+ types
    /// </summary>
    public enum ConnectorType
    {
        // API Types
        RestAPI,
        GraphQL,
        SOAP,
        gRPC,
        WebSocket,
        
        // Database Types
        MySQL,
        PostgreSQL,
        MongoDB,
        SQLServer,
        Oracle,
        Redis,
        Elasticsearch,
        Cassandra,
        DynamoDB,
        CosmosDB,
        
        // Cloud Storage
        S3,
        AzureBlob,
        GoogleCloudStorage,
        
        // Message Queue
        RabbitMQ,
        Kafka,
        AzureServiceBus,
        AmazonSQS,
        
        // File Systems
        FTP,
        SFTP,
        LocalFileSystem,
        
        // Business Apps
        Salesforce,
        HubSpot,
        Zendesk,
        Shopify,
        Stripe,
        PayPal,
        Twilio,
        SendGrid,
        
        // Custom
        Custom,
        Webhook
    }

    /// <summary>
    /// Connector category
    /// </summary>
    public enum ConnectorCategory
    {
        Database,
        API,
        CRM,
        ERP,
        Payment,
        Communication,
        Storage,
        Analytics,
        Marketing,
        Custom
    }

    /// <summary>
    /// Connector status
    /// </summary>
    public enum ConnectorStatus
    {
        Active,
        Inactive,
        Testing,
        Error,
        Deprecated
    }

    /// <summary>
    /// Health status
    /// </summary>
    public enum HealthStatus
    {
        Healthy,
        Degraded,
        Unhealthy,
        Unknown
    }

    /// <summary>
    /// Authentication type
    /// </summary>
    public enum AuthenticationType
    {
        None,
        Basic,
        ApiKey,
        Bearer,
        OAuth2,
        OAuth2ClientCredentials,
        OAuth2AuthorizationCode,
        JWT,
        Certificate,
        NTLM,
        Kerberos,
        AWSSignature,
        Custom
    }

    /// <summary>
    /// API key location
    /// </summary>
    public enum ApiKeyLocation
    {
        Header,
        QueryParameter,
        Cookie
    }

    /// <summary>
    /// Rate limit strategy
    /// </summary>
    public enum RateLimitStrategy
    {
        FixedWindow,
        SlidingWindow,
        TokenBucket,
        LeakyBucket
    }

    /// <summary>
    /// Retry strategy
    /// </summary>
    public enum RetryStrategy
    {
        FixedDelay,
        ExponentialBackoff,
        LinearBackoff,
        Custom
    }

    /// <summary>
    /// Endpoint type
    /// </summary>
    public enum EndpointType
    {
        Query,
        Command,
        Event,
        Subscription
    }

    /// <summary>
    /// HTTP method
    /// </summary>
    public enum HttpMethodType
    {
        GET,
        POST,
        PUT,
        PATCH,
        DELETE,
        HEAD,
        OPTIONS
    }

    /// <summary>
    /// Database query type
    /// </summary>
    public enum QueryType
    {
        Select,
        Insert,
        Update,
        Delete,
        StoredProcedure,
        Function
    }

    /// <summary>
    /// Parameter location
    /// </summary>
    public enum ParameterLocation
    {
        Path,
        Query,
        Header,
        Body,
        Cookie
    }

    /// <summary>
    /// Parameter type
    /// </summary>
    public enum ParameterType
    {
        String,
        Number,
        Integer,
        Boolean,
        Date,
        DateTime,
        Array,
        Object,
        File
    }

    /// <summary>
    /// Content type
    /// </summary>
    public enum ContentType
    {
        JSON,
        XML,
        FormUrlEncoded,
        MultipartFormData,
        Text,
        Binary
    }

    /// <summary>
    /// Cache strategy
    /// </summary>
    public enum CacheStrategy
    {
        TimeToLive,
        LeastRecentlyUsed,
        LeastFrequentlyUsed,
        FirstInFirstOut
    }

    /// <summary>
    /// Log level
    /// </summary>
    public enum LogLevel
    {
        Trace,
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }

    /// <summary>
    /// Parameter direction
    /// </summary>
    public enum ParameterDirection
    {
        Input,
        Output,
        InputOutput
    }
}
