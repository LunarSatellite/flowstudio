namespace Aurora.FlowStudio.Entity.Enums
{
    /// <summary>
    /// AI Provider type
    /// </summary>
    public enum AIProviderType
    {
        PlatformDefault,  // Our predefined LLM - always used for execution
        OpenAI,           // Tenant config only
        Anthropic,
        GooglePalm,
        Cohere,
        Azure,
        AWS,
        Custom
    }

    /// <summary>
    /// AI Provider status
    /// </summary>
    public enum AIProviderStatus
    {
        Active,
        Inactive,
        Testing,
        Deprecated
    }

    /// <summary>
    /// AI capabilities
    /// </summary>
    public enum AICapability
    {
        TextGeneration,
        ChatCompletion,
        Embedding,
        FineTuning,
        FunctionCalling,
        Vision,
        Audio,
        Moderation,
        Classification,
        Summarization,
        Translation,
        SentimentAnalysis,
        EntityExtraction,
        IntentRecognition
    }

    /// <summary>
    /// AI Model type
    /// </summary>
    public enum ModelType
    {
        ChatCompletion,
        TextCompletion,
        Embedding,
        ImageGeneration,
        AudioTranscription,
        AudioGeneration,
        Moderation,
        FineTuned
    }

    /// <summary>
    /// TTS Provider type
    /// </summary>
    public enum TTSProviderType
    {
        PlatformDefault,
        ElevenLabs,
        Google,
        Amazon,
        Azure,
        OpenAI,
        Custom
    }

    /// <summary>
    /// Audio format
    /// </summary>
    public enum AudioFormat
    {
        MP3,
        WAV,
        OGG,
        FLAC,
        AAC
    }

    /// <summary>
    /// STT Provider type
    /// </summary>
    public enum STTProviderType
    {
        PlatformDefault,
        Google,
        Amazon,
        Azure,
        Deepgram,
        AssemblyAI,
        Whisper,
        Custom
    }
}
