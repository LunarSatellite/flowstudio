namespace Aurora.FlowStudio.Entity.Enums
{
    /// <summary>
    /// Conversation channel
    /// </summary>
    public enum ConversationChannel
    {
        WebChat,
        MobileApp,
        Voice,
        SMS,
        WhatsApp,
        Telegram,
        Facebook,
        Instagram,
        Twitter,
        Email,
        Slack,
        Teams,
        Custom
    }

    /// <summary>
    /// Conversation status
    /// </summary>
    public enum ConversationStatus
    {
        Active,
        Paused,
        Completed,
        Abandoned,
        Transferred,
        Error
    }

    /// <summary>
    /// Conversation priority
    /// </summary>
    public enum ConversationPriority
    {
        Low,
        Normal,
        High,
        Urgent,
        Critical
    }

    /// <summary>
    /// Message role
    /// </summary>
    public enum MessageRole
    {
        User,
        Bot,
        Agent,
        System
    }

    /// <summary>
    /// Message type
    /// </summary>
    public enum MessageType
    {
        Text,
        Image,
        Video,
        Audio,
        File,
        Location,
        Contact,
        Card,
        Carousel,
        QuickReply,
        Button,
        List,
        Form,
        Payment,
        Receipt,
        Typing,
        System
    }

    /// <summary>
    /// Message status
    /// </summary>
    public enum MessageStatus
    {
        Pending,
        Sent,
        Delivered,
        Read,
        Failed,
        Deleted
    }

    /// <summary>
    /// Message reaction type
    /// </summary>
    public enum ReactionType
    {
        Like,
        Dislike,
        Love,
        Helpful,
        NotHelpful,
        Custom
    }

    /// <summary>
    /// Conversation tag category
    /// </summary>
    public enum TagCategory
    {
        Issue,
        Topic,
        Status,
        Priority,
        Department,
        Custom
    }

    /// <summary>
    /// Note visibility
    /// </summary>
    public enum NoteVisibility
    {
        Internal,
        TeamVisible,
        CustomerVisible
    }

    /// <summary>
    /// Feedback status
    /// </summary>
    public enum FeedbackStatus
    {
        Received,
        Reviewed,
        ActionTaken,
        Dismissed
    }

    /// <summary>
    /// Sentiment type
    /// </summary>
    public enum SentimentType
    {
        Positive,
        Negative,
        Neutral,
        Mixed
    }

    /// <summary>
    /// Customer status
    /// </summary>
    public enum CustomerStatus
    {
        Lead,
        Active,
        Inactive,
        VIP,
        AtRisk,
        Churned,
        Blocked
    }

    /// <summary>
    /// Customer type
    /// </summary>
    public enum CustomerType
    {
        Individual,
        Business,
        Enterprise
    }

    /// <summary>
    /// Note type for customer notes
    /// </summary>
    public enum NoteType
    {
        General,
        CallLog,
        MeetingNote,
        EmailSummary,
        Complaint,
        Compliment,
        Important
    }

    /// <summary>
    /// Customer activity type
    /// </summary>
    public enum ActivityType
    {
        PageView,
        FormSubmission,
        EmailOpen,
        EmailClick,
        Purchase,
        Download,
        VideoWatch,
        ChatInitiated,
        ChatCompleted,
        CallReceived,
        CallMade,
        Custom
    }

    /// <summary>
    /// Customer authentication type
    /// </summary>
    public enum CustomerAuthType
    {
        Anonymous,
        Email,
        Phone,
        OAuth,
        FIDO2,
        Biometric,
        MagicLink,
        OTP,
        Custom
    }
}
