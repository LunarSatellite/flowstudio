# Aurora FlowStudio - Enums

This folder contains all enumeration types used throughout the Aurora FlowStudio platform, organized by domain.

## 📁 File Structure

### CoreEnums.cs
Core system enumerations:
- `TenantStatus` - Active, Suspended, Trial, Expired, Cancelled
- `TenantPlan` - Trial, Starter, Professional, Enterprise, Custom
- `SettingType` - String, Number, Boolean, Json, Encrypted

### IdentityEnums.cs
Identity and authentication enumerations:
- `UserStatus` - Active, Inactive, Suspended, Locked, PendingVerification, PendingApproval
- `RoleLevel` - SuperAdmin, TenantAdmin, Manager, Supervisor, FlowDesigner, Agent, Analyst, User, ReadOnly
- `PermissionScope` - System, Tenant, Department, Team, Own
- `SessionStatus` - Active, Expired, Revoked, LoggedOut
- `ActivityAction` - Create, Read, Update, Delete, Execute, Publish, Archive, etc.
- `LogSeverity` - Info, Warning, Error, Critical, Security
- `CredentialType` - PlatformAuthenticator, CrossPlatformAuthenticator, Hybrid
- `CredentialStatus` - Active, Revoked, Suspended, Expired

### FlowEnums.cs
Flow and workflow enumerations:
- `FlowType` - ChatBot, VoiceBot, Hybrid, Survey, Form, Appointment, Support, Sales, Marketing
- `FlowCategory` - CustomerService, Sales, Marketing, ECommerce, Healthcare, Finance, etc.
- `FlowStatus` - Draft, Testing, Published, Archived, Deprecated
- `NodeType` - 40+ types including Message, Question, ApiCall, AIResponse, ProductSearch, etc.
- `ConnectionType` - Default, Conditional, Success, Error, Timeout, Fallback
- `ActionType` - Custom, HttpRequest, DatabaseOperation, EmailSend, etc.
- `VariableType` - String, Number, Boolean, Date, DateTime, Array, Object, File
- `VariableScope` - Flow, Session, User, Global
- `MenuType` - Main, SubMenu, Dynamic, Conditional
- `MenuStatus` - Active, Inactive, Draft, Archived
- `MenuDisplayStyle` - List, Grid, Carousel, ButtonGroup, QuickReplies
- `MessageFormat` - PlainText, Markdown, Html, SSML
- `MenuItemType` - Flow, SubMenu, ExternalUrl, CustomAction, Divider, Information
- `ConditionOperator` - And, Or
- `ConditionComparison` - Equals, NotEquals, GreaterThan, Contains, etc.
- `IntegrationType` - API, Database, Webhook, CRM, ERP, Payment, Analytics, Custom
- `IntegrationTrigger` - Manual, OnFlowStart, OnFlowComplete, OnNodeEnter, etc.
- `ResponseSourceType` - Static, API, Database, GraphQL, LLMGenerated, Hybrid, etc.
- `ResponseFormat` - Text, JSON, XML, HTML, Markdown, SSML, Custom
- `ConnectionTrigger` - Manual, Automatic, Conditional, OnComplete, OnError

### ConversationEnums.cs
Conversation and messaging enumerations:
- `ConversationChannel` - WebChat, MobileApp, Voice, SMS, WhatsApp, Telegram, Facebook, etc.
- `ConversationStatus` - Active, Paused, Completed, Abandoned, Transferred, Error
- `ConversationPriority` - Low, Normal, High, Urgent, Critical
- `MessageRole` - User, Bot, Agent, System
- `MessageType` - Text, Image, Video, Audio, File, Location, Contact, Card, etc.
- `MessageStatus` - Pending, Sent, Delivered, Read, Failed, Deleted
- `ReactionType` - Like, Dislike, Love, Helpful, NotHelpful, Custom
- `TagCategory` - Issue, Topic, Status, Priority, Department, Custom
- `NoteVisibility` - Internal, TeamVisible, CustomerVisible
- `FeedbackStatus` - Received, Reviewed, ActionTaken, Dismissed
- `SentimentType` - Positive, Negative, Neutral, Mixed
- `CustomerStatus` - Lead, Active, Inactive, VIP, AtRisk, Churned, Blocked
- `CustomerType` - Individual, Business, Enterprise
- `NoteType` - General, CallLog, MeetingNote, EmailSummary, Complaint, Compliment, Important
- `ActivityType` - PageView, FormSubmission, EmailOpen, Purchase, ChatInitiated, etc.
- `CustomerAuthType` - Anonymous, Email, Phone, OAuth, FIDO2, Biometric, MagicLink, OTP, Custom

### IntegrationEnums.cs
Integration and connector enumerations:
- `ConnectorType` - 50+ types including RestAPI, GraphQL, MySQL, PostgreSQL, MongoDB, Salesforce, Shopify, Stripe, etc.
- `ConnectorCategory` - Database, API, CRM, ERP, Payment, Communication, Storage, Analytics, Marketing, Custom
- `ConnectorStatus` - Active, Inactive, Testing, Error, Deprecated
- `HealthStatus` - Healthy, Degraded, Unhealthy, Unknown
- `AuthenticationType` - None, Basic, ApiKey, Bearer, OAuth2, JWT, Certificate, NTLM, Kerberos, AWSSignature, Custom
- `ApiKeyLocation` - Header, QueryParameter, Cookie
- `RateLimitStrategy` - FixedWindow, SlidingWindow, TokenBucket, LeakyBucket
- `RetryStrategy` - FixedDelay, ExponentialBackoff, LinearBackoff, Custom
- `EndpointType` - Query, Command, Event, Subscription
- `HttpMethod` - GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS
- `QueryType` - Select, Insert, Update, Delete, StoredProcedure, Function
- `ParameterLocation` - Path, Query, Header, Body, Cookie
- `ParameterType` - String, Number, Integer, Boolean, Date, DateTime, Array, Object, File
- `ContentType` - JSON, XML, FormUrlEncoded, MultipartFormData, Text, Binary
- `CacheStrategy` - TimeToLive, LeastRecentlyUsed, LeastFrequentlyUsed, FirstInFirstOut
- `LogLevel` - Trace, Debug, Info, Warning, Error, Critical

### AIEnums.cs
AI/LLM provider enumerations:
- `AIProviderType` - PlatformDefault, OpenAI, Anthropic, GooglePalm, Cohere, Azure, AWS, Custom
- `AIProviderStatus` - Active, Inactive, Testing, Deprecated
- `AICapability` - TextGeneration, ChatCompletion, Embedding, Vision, Audio, Translation, etc.
- `ModelType` - ChatCompletion, TextCompletion, Embedding, ImageGeneration, AudioTranscription, etc.
- `TTSProviderType` - PlatformDefault, ElevenLabs, Google, Amazon, Azure, OpenAI, Custom
- `AudioFormat` - MP3, WAV, OGG, FLAC, AAC
- `STTProviderType` - PlatformDefault, Google, Amazon, Azure, Deepgram, AssemblyAI, Whisper, Custom

### CommerceEnums.cs
E-commerce enumerations:
- `ProductStatus` - Active, Draft, Archived, OutOfStock
- `CartStatus` - Active, Abandoned, Completed, Expired
- `OrderStatus` - Pending, Confirmed, Processing, Shipped, Delivered, Cancelled, Refunded, Failed
- `PaymentStatus` - Pending, Authorized, Paid, PartiallyPaid, Refunded, PartiallyRefunded, Voided, Failed
- `FulfillmentStatus` - Unfulfilled, PartiallyFulfilled, Fulfilled, Shipped, Delivered, PickedUp, Cancelled
- `TransactionType` - Payment, Refund, Authorization, Capture, Void
- `TransactionStatus` - Pending, Success, Failed, Cancelled
- `PaymentMethod` - CreditCard, DebitCard, PayPal, Stripe, BankTransfer, CashOnDelivery, Cryptocurrency, Wallet, Other
- `ReviewStatus` - Pending, Approved, Rejected, Flagged
- `DiscountType` - Percentage, FixedAmount, FreeShipping, BuyXGetY
- `DiscountStatus` - Active, Scheduled, Expired, Disabled

## 🎯 Usage

All enums are in the `Aurora.FlowStudio.Entity.Enums` namespace. Import them in your files:

```csharp
using Aurora.FlowStudio.Entity.Enums;

// Then use directly
public FlowType Type { get; set; } = FlowType.ChatBot;
public NodeType NodeType { get; set; } = NodeType.Message;
public ConnectorType Connector { get; set; } = ConnectorType.RestAPI;
```

## 📊 Statistics

- **Total Enum Files**: 7
- **Total Enums**: 80+
- **Total Enum Values**: 500+

## 🔍 Finding Enums

Use these guidelines to find the right enum:

1. **Core/System level** → CoreEnums.cs
2. **Users/Authentication** → IdentityEnums.cs
3. **Flows/Workflows** → FlowEnums.cs
4. **Conversations/Messages** → ConversationEnums.cs
5. **Connectors/Integrations** → IntegrationEnums.cs
6. **AI/LLM/TTS/STT** → AIEnums.cs
7. **E-commerce/Shopping** → CommerceEnums.cs

## ✨ Benefits of Centralized Enums

1. **Easy to Find** - All enums in one place
2. **No Duplication** - Single source of truth
3. **Easy to Extend** - Add new values in one place
4. **Better IntelliSense** - IDE can find all enums easily
5. **Clean Entity Files** - Entity files focus on data structure
6. **Easier Refactoring** - Change enum names/values in one place

## 🔄 Migration from Inline Enums

All entity files have been updated to use:
```csharp
using Aurora.FlowStudio.Entity.Enums;
```

Enums that were previously defined inline in entity files have been moved here and all references updated.

---

**Aurora FlowStudio** - World-class enumeration organization 🎯
