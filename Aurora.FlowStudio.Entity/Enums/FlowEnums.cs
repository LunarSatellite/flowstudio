namespace Aurora.FlowStudio.Entity.Enums
{
    /// <summary>
    /// Flow type enumeration
    /// </summary>
    public enum FlowType
    {
        ChatBot,
        VoiceBot,
        Hybrid,
        Survey,
        Form,
        Appointment,
        Support,
        Sales,
        Marketing
    }

    /// <summary>
    /// Flow category
    /// </summary>
    public enum FlowCategory
    {
        CustomerService,
        Sales,
        Marketing,
        ECommerce,
        Healthcare,
        Finance,
        Education,
        HumanResources,
        RealEstate,
        Hospitality,
        Custom
    }

    /// <summary>
    /// Flow status
    /// </summary>
    public enum FlowStatus
    {
        Draft,
        Testing,
        Published,
        Archived,
        Deprecated
    }

    /// <summary>
    /// Node types - 40+ types for different purposes
    /// </summary>
    public enum NodeType
    {
        // Input nodes
        Message,
        Question,
        MultipleChoice,
        DatePicker,
        TimePicker,
        FileUpload,
        Location,
        Payment,
        Form,

        // Logic nodes
        Condition,
        Switch,
        Loop,
        SubFlow,
        Delay,

        // Action nodes
        ApiCall,
        DatabaseQuery,
        SendEmail,
        SendSMS,
        SendNotification,
        CreateTicket,
        UpdateCRM,
        Webhook,

        // AI nodes
        AIResponse,
        IntentRecognition,
        EntityExtraction,
        Sentiment,
        Translation,
        TextToSpeech,
        SpeechToText,

        // E-commerce nodes
        ProductSearch,
        AddToCart,
        Checkout,
        OrderStatus,
        ProductRecommendation,

        // Integration nodes
        Salesforce,
        Shopify,
        Stripe,
        Zapier,
        Slack,
        Teams,

        // Control flow
        Start,
        End,
        Error,
        Fallback
    }

    /// <summary>
    /// Connection type between nodes
    /// </summary>
    public enum ConnectionType
    {
        Default,
        Conditional,
        Success,
        Error,
        Timeout,
        Fallback
    }

    /// <summary>
    /// Node action type
    /// </summary>
    public enum ActionType
    {
        Custom,
        HttpRequest,
        DatabaseOperation,
        EmailSend,
        SmsSend,
        PushNotification,
        WebhookCall,
        ScriptExecution,
        DataTransformation,
        FileOperation,
        CacheOperation,
        QueueOperation
    }

    /// <summary>
    /// Flow variable type
    /// </summary>
    public enum VariableType
    {
        String,
        Number,
        Boolean,
        Date,
        DateTime,
        Array,
        Object,
        File
    }

    /// <summary>
    /// Variable scope
    /// </summary>
    public enum VariableScope
    {
        Flow,
        Session,
        User,
        Global
    }

    /// <summary>
    /// Menu type
    /// </summary>
    public enum MenuType
    {
        Main,           // Initial welcome menu
        SubMenu,        // Nested menu
        Dynamic,        // Generated based on data
        Conditional     // Shows based on conditions
    }

    /// <summary>
    /// Menu status
    /// </summary>
    public enum MenuStatus
    {
        Active,
        Inactive,
        Draft,
        Archived
    }

    /// <summary>
    /// Menu display style
    /// </summary>
    public enum MenuDisplayStyle
    {
        List,           // Numbered list
        Grid,           // Grid of cards
        Carousel,       // Swipeable carousel
        ButtonGroup,    // Button group
        QuickReplies    // Quick reply chips
    }

    /// <summary>
    /// Message format
    /// </summary>
    public enum MessageFormat
    {
        PlainText,
        Markdown,
        Html,
        SSML  // For voice
    }

    /// <summary>
    /// Menu item type
    /// </summary>
    public enum MenuItemType
    {
        Flow,           // Navigate to a flow
        SubMenu,        // Navigate to submenu
        ExternalUrl,    // Open external URL
        CustomAction,   // Trigger custom action
        Divider,        // Visual separator
        Information     // Display-only info
    }

    /// <summary>
    /// Condition operator for conditional logic
    /// </summary>
    public enum ConditionOperator
    {
        And,
        Or
    }

    /// <summary>
    /// Condition comparison types
    /// </summary>
    public enum ConditionComparison
    {
        Equals,
        NotEquals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        NotContains,
        StartsWith,
        EndsWith,
        IsEmpty,
        IsNotEmpty,
        In,
        NotIn
    }

    /// <summary>
    /// Integration type
    /// </summary>
    public enum IntegrationType
    {
        API,
        Database,
        Webhook,
        CRM,
        ERP,
        Payment,
        Analytics,
        Custom
    }

    /// <summary>
    /// Integration trigger
    /// </summary>
    public enum IntegrationTrigger
    {
        Manual,
        OnFlowStart,
        OnFlowComplete,
        OnNodeEnter,
        OnNodeExit,
        OnCondition,
        OnSchedule,
        OnWebhook
    }

    /// <summary>
    /// Response source type
    /// </summary>
    public enum ResponseSourceType
    {
        Static,         // Hardcoded response
        API,            // REST API call
        Database,       // Database query
        GraphQL,        // GraphQL query
        File,           // File content
        Template,       // Template with variables
        Script,         // Custom script execution
        Webhook,        // Webhook response
        LLMGenerated,   // Pure LLM generation
        Hybrid          // Combine multiple sources
    }

    /// <summary>
    /// Response format
    /// </summary>
    public enum ResponseFormat
    {
        Text,
        JSON,
        XML,
        HTML,
        Markdown,
        SSML,
        Custom
    }

    /// <summary>
    /// Flow connection trigger
    /// </summary>
    public enum ConnectionTrigger
    {
        Manual,
        Automatic,
        Conditional,
        OnComplete,
        OnError
    }
}
