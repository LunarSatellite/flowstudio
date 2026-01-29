# 🎯 How 37 Entities Run Your Entire AI Business

## Complete Entity-by-Entity Business Flow Explanation

---

## 📋 **Table of Contents**

1. [Tenant & Billing (10 entities)](#tenant--billing)
2. [Conversation (5 entities)](#conversation)
3. [Flow Automation (6 entities)](#flow-automation)
4. [AI & Voice (3 entities)](#ai--voice)
5. [Integration (3 entities)](#integration)
6. [Knowledge (3 entities)](#knowledge)
7. [Access Control (3 entities)](#access-control)
8. [Complete Business Scenario](#complete-business-scenario)

---

## 🏢 **TENANT & BILLING MODULE (10 Entities)**

This module manages your B2B customers and all billing/cost tracking.

---

### **1. Tenant** - Your B2B Customer (The Company)

**Purpose:** Represents each company that signs up for your platform.

**What It Does:**
- Stores company information (name, domain, contact)
- Tracks subscription status (Active, Trial, Suspended, Cancelled)
- Manages trial periods
- Links to all their data (conversations, flows, etc.)

**Business Flow:**
```
Step 1: Company "ShopEasy" signs up
  ↓
Step 2: Create Tenant record:
  - CompanyName: "ShopEasy"
  - Domain: "shopeasy.com"
  - Status: Trial
  - Plan: Free
  - TrialEndsAt: 30 days from now
  - ContactEmail: "admin@shopeasy.com"
  ↓
Step 3: All their data is isolated by TenantId
```

**Why You Need It:**
- Multi-tenancy: Keeps ShopEasy's data separate from Acme Corp's data
- Subscription management: Know who's paying, who's on trial
- Cancellation: When they cancel, you can clean up all their data

**Real Example:**
```csharp
// ShopEasy signs up
Tenant shopEasy = new Tenant {
    CompanyName = "ShopEasy",
    Domain = "shopeasy.com",
    Status = TenantStatus.Trial,
    Plan = TenantPlan.Free,
    TrialEndsAt = DateTime.UtcNow.AddDays(30)
};

// Everything they create links to this TenantId
// - Their conversations: WHERE TenantId = shopEasy.Id
// - Their flows: WHERE TenantId = shopEasy.Id
// - Their invoices: WHERE TenantId = shopEasy.Id
```

---

### **2. TenantSubscription** - What Plan They're On

**Purpose:** Tracks which subscription plan the tenant has and what's included.

**What It Does:**
- Records monthly fee they pay
- Sets included resources (tokens, voice minutes, API calls)
- Tracks subscription start/end dates
- Manages auto-renewal

**Business Flow:**
```
Step 1: ShopEasy upgrades from Free to Pro
  ↓
Step 2: Create TenantSubscription:
  - TenantId: ShopEasy
  - PlanName: "Pro"
  - MonthlyFee: $299
  - IncludedTokens: 5,000,000
  - IncludedVoiceMinutes: 1,000
  - StartsAt: Today
  - Status: Active
  - AutoRenew: true
  ↓
Step 3: When calculating bill, check included limits first
```

**Why You Need It:**
- Know how much to charge each month
- Check if they've exceeded included resources
- Manage plan upgrades/downgrades
- Track subscription history

**Real Example:**
```csharp
// Check if they've exceeded included resources
var subscription = await GetActiveSubscription(tenantId);
var usageThisMonth = await GetUsageThisMonth(tenantId);

if (usageThisMonth.TotalTokens > subscription.IncludedTokens) {
    // Charge overage
    decimal overageTokens = usageThisMonth.TotalTokens - subscription.IncludedTokens;
    decimal overageCost = overageTokens * 0.05m / 1000; // $0.05 per 1K
}
```

---

### **3. Usage** - Aggregated Usage Tracking

**Purpose:** High-level summary of what services were used (rolled up by day/service).

**What It Does:**
- Aggregates token usage by provider/model
- Aggregates voice minutes by provider
- Summarizes API calls, SMS, Email
- Links usage to conversations

**Business Flow:**
```
End of Day:
  ↓
Aggregate all detailed usage:
  - ShopEasy used 150K tokens today via OpenAI GPT-4
  - Cost you: $4.50
  - Charged them: $7.50
  - Your profit: $3.00
  ↓
Create Usage record for the day
```

**Why You Need It:**
- Fast queries for dashboards (don't scan millions of records)
- Daily/weekly/monthly summaries
- Quick budget checking

**Real Example:**
```csharp
// Get today's usage
var todayUsage = await _db.Usage
    .Where(u => u.TenantId == tenantId && u.UsedAt.Date == DateTime.Today)
    .GroupBy(u => u.Type)
    .Select(g => new {
        Type = g.Key,
        TotalQuantity = g.Sum(u => u.Quantity),
        TotalCost = g.Sum(u => u.CustomerCost)
    })
    .ToListAsync();

// Result:
// Token: 150,000 tokens, $7.50
// Voice: 25 minutes, $5.00
```

---

### **4. UsageBreakdown** - Detailed Per-Request Cost Tracking ⭐

**Purpose:** THE MOST IMPORTANT ENTITY! Tracks every single token/minute with complete transparency.

**What It Does:**
- Records EVERY API call to LLM/Voice providers
- Shows YOUR cost (what you paid OpenAI)
- Shows CLIENT cost (what they pay you)
- Calculates your profit
- Links to conversation & message
- Timestamps everything for audit

**Business Flow:**
```
Customer asks: "What's the price of AirPods?"
  ↓
Your platform calls OpenAI GPT-4:
  - Input: 120 tokens
  - Output: 50 tokens
  ↓
UsageBreakdown records:
  ConversationId: "conv-123"
  MessageId: "msg-456"
  Type: Token
  Provider: "openai"
  Model: "gpt-4"
  
  InputQuantity: 120
  OutputQuantity: 50
  TotalQuantity: 170
  
  InputRatePer1K: $0.03
  OutputRatePer1K: $0.06
  
  YourInputCost: $0.0036    (120 × $0.03/1K)
  YourOutputCost: $0.0030   (50 × $0.06/1K)
  YourTotalCost: $0.0066
  
  MarkupPercent: 66%
  
  ClientInputCost: $0.0060  ($0.0036 × 1.66)
  ClientOutputCost: $0.0050 ($0.0030 × 1.66)
  ClientTotalCost: $0.0110
  
  ProfitAmount: $0.0044     ($0.0110 - $0.0066)
  ProfitPercent: 66%
  
  UsedAt: 2025-01-29 10:15:23
  IsBilled: false
```

**Why You Need It:**
- **Zero disputes**: Client can see EXACTLY what they paid for
- **Drill-down**: "Why is my bill $500?" → Show every single request
- **Profit tracking**: Know your margin on every conversation
- **Audit trail**: Complete history forever

**Real Example - Client Portal:**
```
Dashboard shows:
┌─────────────────────────────────────────────────┐
│ Conversation #conv-123 - Total Cost: $0.0374   │
├─────────────────────────────────────────────────┤
│ 10:15:23 - Intent Detection                     │
│   Input: 120 tokens, Output: 50 tokens         │
│   Your cost: $0.0066, You paid: $0.0110        │
│                                                 │
│ 10:15:45 - Product Search                      │
│   Input: 250 tokens, Output: 180 tokens        │
│   Your cost: $0.0159, You paid: $0.0264        │
│                                                 │
│ [Click for full breakdown CSV export]          │
└─────────────────────────────────────────────────┘
```

---

### **5. CostAlert** - Prevent Surprise Bills

**Purpose:** Proactively warn clients when approaching spending limits.

**What It Does:**
- Monitors spending in real-time
- Sends alerts at thresholds (50%, 80%, 90%, 100%)
- Can auto-pause service when limit reached
- Configurable per tenant

**Business Flow:**
```
ShopEasy sets up alert:
  - Name: "Monthly Budget Alert"
  - ThresholdAmount: $1,000
  - ThresholdPercent: 80% ($800)
  - SendEmail: true
  - NotifyEmails: ["cfo@shopeasy.com", "admin@shopeasy.com"]
  ↓
System checks after every usage:
  CurrentAmount: $820
  ThresholdAmount: $800
  ↓
Alert triggered!
  - Send email: "⚠️ You've reached 82% of your $1,000 budget"
  - Update: LastTriggeredAt, TriggerCount++
  - Show in dashboard: Red warning banner
```

**Why You Need It:**
- Happy customers: No surprise $5,000 bills
- Reduced disputes: They were warned
- Control costs: Auto-pause prevents runaway spending

**Real Example:**
```csharp
// Check after usage
public async Task CheckAlerts(Guid tenantId, decimal newTotalSpend)
{
    var alerts = await _db.CostAlerts
        .Where(a => a.TenantId == tenantId && a.IsEnabled)
        .ToListAsync();
    
    foreach (var alert in alerts) {
        decimal threshold = alert.ThresholdAmount;
        
        if (newTotalSpend >= threshold && alert.CurrentAmount < threshold) {
            // Just crossed threshold!
            await SendAlertEmail(alert);
            alert.LastTriggeredAt = DateTime.UtcNow;
            alert.TriggerCount++;
        }
        
        alert.CurrentAmount = newTotalSpend;
    }
}
```

---

### **6. PricingHistory** - Track Rate Changes

**Purpose:** Complete audit trail when you change prices.

**What It Does:**
- Records old price → new price
- Tracks when change takes effect
- Records why you changed it (OpenAI raised rates)
- Tracks if client was notified

**Business Flow:**
```
March 1: OpenAI raises GPT-4 prices
  Old: $0.03 input, $0.06 output
  New: $0.04 input, $0.07 output
  ↓
You need to pass increase to clients
  ↓
Create PricingHistory:
  ServiceType: "token"
  Provider: "openai"
  Model: "gpt-4"
  
  OldBaseCost: $0.03
  OldMarkup: 66%
  OldClientPrice: $0.05
  
  NewBaseCost: $0.04
  NewMarkup: 66%
  NewClientPrice: $0.0664
  
  ChangePercent: 32.8%
  ChangeReason: "OpenAI rate increase effective March 1"
  EffectiveFrom: 2025-03-01
  ChangedBy: AdminUserId
  
  ClientNotified: true
  NotifiedAt: 2025-02-25 (gave them 5 days notice)
```

**Why You Need It:**
- Legal protection: You have proof you notified them
- Dispute resolution: "You raised prices!" → Show history & notification
- Transparency: Clients can see full pricing history

---

### **7. UsageQuota** - Hard Spending Limits

**Purpose:** Set maximum spending limits and auto-pause if exceeded.

**What It Does:**
- Sets dollar or quantity limits
- Auto-pauses service when exceeded
- Auto-resets monthly/weekly/daily
- Protects both you and client

**Business Flow:**
```
Startup client sets quota:
  - Type: All (token + voice + sms)
  - Period: Monthly
  - MaxDollarAmount: $500
  - WarningThreshold: 80% ($400)
  - OnExceed: Pause
  - AutoReset: true
  ↓
During month:
  CurrentAmount: $475
  PercentUsed: 95%
  IsExceeded: false (still under limit)
  ↓
Next request would cost $30:
  Would total: $505 > $500
  ↓
Action: PAUSE service
  IsExceeded: true
  ExceededAt: Now
  Send email: "Budget limit reached, service paused"
  ↓
Client options:
  1. Increase quota
  2. Wait until next month (auto-reset)
  3. Pay overage to resume
```

**Why You Need It:**
- Protect clients: Can't accidentally spend $10,000
- Protect yourself: Won't have unpaid bills
- Flexibility: Daily/weekly/monthly limits

---

### **8. Invoice** - Monthly Bill

**Purpose:** The actual invoice sent to client at end of month.

**What It Does:**
- Aggregates all costs for billing period
- Breaks down by service type
- Adds subscription fee
- Calculates tax
- Tracks payment status

**Business Flow:**
```
End of January:
  ↓
Generate invoice for ShopEasy:
  InvoiceNumber: "INV-2025-01-001"
  PeriodStart: 2025-01-01
  PeriodEnd: 2025-01-31
  
  SubscriptionFee: $299 (Pro plan)
  TokenCost: $325 (2.5M tokens × $0.13/1K avg)
  VoiceCost: $142.50 (245 minutes)
  SMSCost: $15 (500 messages)
  EmailCost: $5 (200 emails)
  APICost: $0 (included in plan)
  
  TotalAmount: $786.50
  TaxAmount: $78.65 (10%)
  FinalAmount: $865.15
  
  Status: Pending
  ↓
Client pays:
  Status: Paid
  PaidAt: 2025-02-05
  PaymentMethod: "Stripe"
  PaymentTransactionId: "pi_abc123"
```

**Why You Need It:**
- Professional billing
- Payment tracking
- Tax compliance
- Dispute resolution (show itemized breakdown)

---

### **9. PricingRule** - Your Markup Configuration

**Purpose:** Defines how much you mark up each service.

**What It Does:**
- Sets your profit margin per service
- Different markups for different providers
- Time-based (effective from/to dates)
- Per-tenant (VIP clients get better rates)

**Business Flow:**
```
Platform-wide default:
  ServiceType: "token"
  Provider: "openai"
  Model: "gpt-4"
  BaseCostPer1K: $0.03
  MarkupPercent: 66%
  FinalPricePer1K: $0.05
  
Big client gets special rate:
  TenantId: EnterpriseClient
  ServiceType: "token"
  Provider: "openai"
  Model: "gpt-4"
  BaseCostPer1K: $0.03
  MarkupPercent: 40% (lower margin for volume)
  FinalPricePer1K: $0.042
```

**Why You Need It:**
- Flexible pricing: Different margins per service
- VIP pricing: Special rates for big clients
- Easy updates: Change markup without code changes
- A/B testing: Test different pricing tiers

---

### **10. APIKey** - Authentication

**Purpose:** Secure API access for tenant's applications.

**What It Does:**
- Generates secure API keys
- Restricts by IP address
- Tracks usage per key
- Can expire keys

**Business Flow:**
```
ShopEasy creates API key for production:
  Key: "sk_live_abc123xyz"
  Name: "Production Server"
  AllowedIPs: ["52.12.34.56"]
  ExpiresAt: Never
  IsActive: true
  ↓
When they make API call:
  1. Validate key exists
  2. Check not expired
  3. Check IP in allowed list
  4. Increment RequestCount
  5. Update LastUsedAt
```

**Why You Need It:**
- Security: Only authorized apps can access
- Monitoring: Track which keys are used
- Revocation: Disable compromised keys instantly

---

## 💬 **CONVERSATION MODULE (5 Entities)**

This module manages all customer interactions across all channels.

---

### **11. Conversation** - The Chat/Call Session

**Purpose:** Represents one complete interaction with an end-customer.

**What It Does:**
- Groups all messages in one session
- Tracks which channel (WhatsApp, Voice, etc.)
- Links to active flow being executed
- Calculates total cost of conversation
- Stores resolution status

**Business Flow:**
```
Customer John messages on WhatsApp: "Help with order #12345"
  ↓
Create Conversation:
  CustomerId: John's ID
  Channel: WhatsApp
  SessionId: "wa_session_abc123"
  Status: Active
  StartedAt: Now
  CurrentState: {} (will store flow variables)
  MessageCount: 0
  TokensUsed: 0
  TotalCost: 0
  ↓
As conversation progresses:
  MessageCount: 12
  TokensUsed: 3,500
  VoiceMinutesUsed: 0
  TotalCost: $0.175
  ↓
When resolved:
  Status: Completed
  EndedAt: Now
  IsResolved: true
  Rating: 5 (if John rates the service)
  Feedback: "Great help, thanks!"
```

**Why You Need It:**
- Conversation context: All messages grouped together
- Cost tracking: Know cost per conversation
- Analytics: Average resolution time, satisfaction
- Flow state: Remember where customer is in automation

**Real Example:**
```csharp
// Get conversation with all costs
var conversation = await _db.Conversations
    .Where(c => c.Id == conversationId)
    .Select(c => new {
        c.SessionId,
        c.Channel,
        c.MessageCount,
        c.TotalCost,
        Duration = (c.EndedAt ?? DateTime.UtcNow) - c.StartedAt,
        CustomerName = c.Customer.Name,
        Messages = c.Messages.OrderBy(m => m.CreatedAt).ToList()
    })
    .FirstOrDefaultAsync();
```

---

### **12. Message** - Individual Message

**Purpose:** Each message sent or received in a conversation.

**What It Does:**
- Stores message content
- Tracks who sent it (User/Assistant/System)
- Records format (Text/Audio/Image)
- Extracts metadata (intent, entities, sentiment)
- Links to conversation

**Business Flow:**
```
John sends: "I want to return my headphones"
  ↓
Create Message:
  ConversationId: conv-123
  Role: User
  Content: "I want to return my headphones"
  Format: Text
  TokenCount: null (user message, no tokens charged)
  ↓
AI processes and detects:
  DetectedIntent: "return_request"
  ExtractedEntities: { "product": "headphones" }
  SentimentScore: -0.3 (slightly negative)
  ↓
AI responds: "I'll help you with that. What's your order number?"
  ↓
Create Message:
  ConversationId: conv-123
  Role: Assistant
  Content: "I'll help you with that. What's your order number?"
  Format: Text
  TokenCount: 15 (output tokens)
  Metadata: { "generatedBy": "gpt-4", "temperature": 0.7 }
```

**Why You Need It:**
- Complete history: Every message stored
- AI insights: Know what customers want (intents)
- Billing: Link to UsageBreakdown for costs
- Training: Use conversations to improve AI
- Compliance: Keep records for disputes

---

### **13. Customer** - The End-User

**Purpose:** Represents the person chatting (ShopEasy's customer, not yours).

**What It Does:**
- Stores contact info (name, email, phone)
- Links to external system (ShopEasy's customer ID)
- Tracks preferences and profile
- Calculates lifetime value
- Tags for segmentation

**Business Flow:**
```
First time John contacts ShopEasy:
  ↓
Create Customer:
  Name: "John Doe"
  Email: "john@example.com"
  Phone: "+1234567890"
  ExternalId: "cust_abc123" (from ShopEasy's system)
  Language: "en"
  Country: "US"
  Profile: { "preferredShipping": "express" }
  FirstContactAt: Now
  LastContactAt: Now
  TotalConversations: 1
  TotalSpent: $0
  ↓
Over time:
  TotalConversations: 45
  TotalSpent: $2,340 (from ShopEasy's orders)
  LifetimeValue: $2,800 (predicted)
  Tags: ["high-value", "frequent-buyer", "tech-products"]
```

**Why You Need It:**
- Personalization: Remember customer preferences
- Context: Link to ShopEasy's database
- Segmentation: Target high-value customers
- Reporting: Customer satisfaction metrics

---

### **14. Channel** - Communication Platform

**Purpose:** Configuration for each communication channel (WhatsApp, Voice, etc.).

**What It Does:**
- Stores channel-specific settings
- API credentials for third parties
- Webhook configuration
- Tracks channel usage

**Business Flow:**
```
ShopEasy enables WhatsApp:
  ↓
Create Channel:
  Name: "ShopEasy WhatsApp"
  Type: WhatsApp
  IsEnabled: true
  Configuration: {
    "phoneNumberId": "123456789",
    "apiKey": "EAAxxxxx",
    "webhookUrl": "https://api.aurora.com/webhook/wa/tenant123",
    "webhookSecret": "abc123xyz"
  }
  ↓
When WhatsApp message arrives:
  1. Verify webhook signature using webhookSecret
  2. Create Conversation (if new)
  3. Create Message
  4. Process with AI
  5. Send response back to WhatsApp API
  ↓
Update Channel:
  MessageCount: 1,247
  LastMessageAt: Now
```

**Why You Need It:**
- Multi-channel: Single platform, multiple channels
- Configuration: Each channel has different setup
- Monitoring: Track which channels are used most
- Isolation: Each tenant's channels separated

---

### **15. ChannelMessage** - Raw Platform Data

**Purpose:** Stores the original, unprocessed message from the channel.

**What It Does:**
- Keeps raw webhook payload
- Tracks delivery status
- Stores external IDs for syncing
- Debugging failed messages

**Business Flow:**
```
WhatsApp webhook delivers message:
  Raw payload: {
    "messaging": [{
      "sender": {"id": "1234567890"},
      "timestamp": 1706518523,
      "message": {
        "mid": "wamid.abc123",
        "text": "Hello"
      }
    }]
  }
  ↓
Create ChannelMessage:
  MessageId: msg-789 (your internal Message)
  ChannelId: whatsapp-channel
  Direction: Inbound
  ExternalMessageId: "wamid.abc123"
  Status: Delivered
  RawData: { ...entire payload... }
  ↓
Send response via WhatsApp API:
  ↓
Create ChannelMessage:
  MessageId: msg-790
  Direction: Outbound
  ExternalMessageId: "wamid.def456"
  Status: Sent
  ↓
WhatsApp confirms delivery:
  Status: Sent → Delivered
  DeliveredAt: Now
  ↓
Customer reads message:
  Status: Delivered → Read
  ReadAt: Now
```

**Why You Need It:**
- Debugging: When messages fail, check raw data
- Syncing: Match your messages with external IDs
- Delivery tracking: Know if message was read
- Compliance: Keep original data for legal

---

## 🔄 **FLOW AUTOMATION MODULE (6 Entities)**

This module powers the visual flow builder and automation engine.

---

### **16. Flow** - The Automation Workflow

**Purpose:** A complete automation sequence (like "Product Support Flow").

**What It Does:**
- Defines the workflow name and category
- Sets trigger conditions (when to start)
- Tracks performance (success rate, avg duration)
- Manages publish status

**Business Flow:**
```
ShopEasy creates "Product Support" flow:
  ↓
Create Flow:
  Name: "Product Support"
  Description: "Help customers with product questions"
  Category: Support
  Status: Draft (not live yet)
  TriggerType: "intent"
  TriggerValue: "product_question"
  ↓
Flow contains nodes:
  - Welcome message
  - Ask what product
  - Call API to get product info
  - Show product details
  - Ask if helpful
  ↓
Publish flow:
  Status: Draft → Published
  PublishedAt: Now
  PublishedBy: UserId
  ↓
Over time, track:
  ExecutionCount: 1,523
  SuccessCount: 1,478
  FailureCount: 45
  SuccessRate: 97%
  AverageDurationSeconds: 35
```

**Why You Need It:**
- Automation: No-code workflow builder
- Reusability: Same flow used 1000s of times
- Analytics: Know which flows work best
- Versioning: Can update without breaking active flows

---

### **17. FlowNode** - Individual Step in Flow

**Purpose:** One action in the flow (send message, call API, check condition).

**What It Does:**
- Defines node type (Message, API, Condition, AI, etc.)
- Stores configuration as JSON (flexible!)
- Tracks position for visual editor
- Monitors performance

**Business Flow:**
```
Flow has these nodes:

Node 1 - Message:
  NodeId: "node_welcome"
  Type: Message
  Name: "Welcome Message"
  Configuration: {
    "text": "Hi! I can help you find products."
  }
  PositionX: 100
  PositionY: 100

Node 2 - AI:
  NodeId: "node_extract_product"
  Type: AI
  Name: "Extract Product Name"
  Configuration: {
    "prompt": "Extract product from: {{user_message}}",
    "model": "gpt-4",
    "outputVariable": "product_name"
  }
  PositionX: 100
  PositionY: 200

Node 3 - API:
  NodeId: "node_get_product"
  Type: API
  Name: "Fetch Product Info"
  Configuration: {
    "connectorId": "shopify-connector",
    "endpointId": "get-product-endpoint",
    "parameters": { "name": "{{product_name}}" },
    "outputVariable": "product_data"
  }
  PositionX: 100
  PositionY: 300

Node 4 - Condition:
  NodeId: "node_check_stock"
  Type: Condition
  Name: "In Stock?"
  Configuration: {
    "variable": "product_data.stock",
    "operator": ">",
    "value": 0
  }
  PositionX: 100
  PositionY: 400

Node 5 - Message (In Stock):
  NodeId: "node_show_product"
  Type: Message
  Configuration: {
    "text": "{{product_data.name}} is available! Price: ${{product_data.price}}"
  }
  PositionX: 50
  PositionY: 500

Node 6 - Message (Out of Stock):
  NodeId: "node_out_of_stock"
  Type: Message
  Configuration: {
    "text": "Sorry, {{product_data.name}} is out of stock."
  }
  PositionX: 150
  PositionY: 500
```

**Why You Need It:**
- Flexibility: JSON config = any node type without schema changes
- Visual editor: Position fields = drag & drop UI
- Analytics: Track which nodes fail most
- Debugging: See exactly where flow broke

---

### **18. FlowConnection** - Links Between Nodes

**Purpose:** Defines how nodes connect (the lines in visual editor).

**What It Does:**
- Links source node to target node
- Optional condition for branching
- Tracks how often each path is taken

**Business Flow:**
```
Connections for Product Support flow:

Connection 1:
  SourceNodeId: "node_welcome"
  TargetNodeId: "node_extract_product"
  Label: "Continue"
  Condition: null (always go to next)

Connection 2:
  SourceNodeId: "node_extract_product"
  TargetNodeId: "node_get_product"
  Label: "Continue"

Connection 3:
  SourceNodeId: "node_get_product"
  TargetNodeId: "node_check_stock"
  Label: "Continue"

Connection 4:
  SourceNodeId: "node_check_stock"
  TargetNodeId: "node_show_product"
  Label: "Yes"
  Condition: "result == true"

Connection 5:
  SourceNodeId: "node_check_stock"
  TargetNodeId: "node_out_of_stock"
  Label: "No"
  Condition: "result == false"

Track:
  Connection 4 ExecutionCount: 1,405 (most products in stock)
  Connection 5 ExecutionCount: 118 (some out of stock)
```

**Why You Need It:**
- Flow logic: Define paths through nodes
- Branching: Conditional logic (if/else)
- Analytics: Know which paths customers take
- Visual editor: Draw the lines between nodes

---

### **19. FlowVariable** - Data Storage in Flow

**Purpose:** Defines variables that flow can use/store during execution.

**What It Does:**
- Declares variable name and type
- Sets default values
- Defines validation rules
- Stores data between nodes

**Business Flow:**
```
Product Support flow variables:

Variable 1:
  Name: "user_message"
  DataType: "string"
  DefaultValue: null
  Description: "Customer's message"
  IsRequired: true

Variable 2:
  Name: "product_name"
  DataType: "string"
  DefaultValue: null
  Description: "Extracted product name"
  IsRequired: false

Variable 3:
  Name: "product_data"
  DataType: "object"
  DefaultValue: null
  Description: "Product details from API"

During execution:
  Start: user_message = "Looking for AirPods"
  After Node 2: product_name = "AirPods"
  After Node 3: product_data = { name: "AirPods Pro", price: 249, stock: 15 }
  Use in Node 5: "AirPods Pro is available! Price: $249"
```

**Why You Need It:**
- State management: Remember data between nodes
- Type safety: Validate data types
- Documentation: Clear variable definitions
- Debugging: See variable values at each step

---

### **20. FlowExecution** - Runtime Tracking

**Purpose:** Tracks one instance of flow being executed (one customer going through flow).

**What It Does:**
- Records current position in flow
- Stores variable values
- Tracks execution status
- Logs errors
- Measures performance

**Business Flow:**
```
Customer triggers Product Support flow:
  ↓
Create FlowExecution:
  FlowId: product-support-flow
  ConversationId: conv-123
  CurrentNodeId: "node_welcome"
  Variables: { "user_message": "Looking for AirPods" }
  Status: Running
  StartedAt: Now
  ExecutedNodes: []
  ↓
Execute node_welcome:
  Send message: "Hi! I can help you find products."
  Update: CurrentNodeId = "node_extract_product"
  Update: ExecutedNodes = ["node_welcome"]
  ↓
Execute node_extract_product:
  Call AI to extract product
  Update: Variables = { "user_message": "...", "product_name": "AirPods" }
  Update: CurrentNodeId = "node_get_product"
  Update: ExecutedNodes = ["node_welcome", "node_extract_product"]
  ↓
Execute node_get_product:
  Call ShopEasy API
  Update: Variables = { ..., "product_data": {...} }
  Update: CurrentNodeId = "node_check_stock"
  ↓
Execute node_check_stock:
  Check: product_data.stock = 15 > 0 = true
  Update: CurrentNodeId = "node_show_product"
  ↓
Execute node_show_product:
  Send message with product details
  Update: Status = Completed
  Update: CompletedAt = Now
  Update: DurationMs = 3,450
```

**Why You Need It:**
- State persistence: Resume if interrupted
- Debugging: See exactly where it failed
- Analytics: How long do flows take?
- Audit: Complete execution history

---

### **21. FlowVersion** - Version Control

**Purpose:** Save snapshots of flow for rollback and history.

**What It Does:**
- Saves complete flow snapshot (nodes + connections)
- Tracks version number
- Records who made changes
- Allows rollback

**Business Flow:**
```
Initial flow creation:
  ↓
FlowVersion 1:
  FlowId: product-support-flow
  Version: 1
  FlowData: { nodes: [...], connections: [...], variables: [...] }
  ChangeNotes: "Initial version"
  IsActive: true
  ↓
Make changes (add new node):
  ↓
FlowVersion 2:
  Version: 2
  FlowData: { ...updated flow... }
  ChangeNotes: "Added out of stock notification"
  IsActive: true
  ↓
Version 1:
  IsActive: false (superseded)
  ↓
Problem with V2? Rollback:
  Version 1: IsActive = true
  Version 2: IsActive = false
```

**Why You Need It:**
- Safety: Can always rollback bad changes
- History: See how flow evolved
- Compliance: Audit trail of changes
- Testing: Compare versions side-by-side

---

## 🤖 **AI & VOICE MODULE (3 Entities)**

This module manages AI and voice service providers.

---

### **22. AIProvider** - LLM Configuration

**Purpose:** Stores configuration for AI providers (OpenAI, Anthropic, etc.).

**What It Does:**
- Stores API credentials
- Configures model settings
- Tracks usage and costs
- Manages multiple providers per tenant

**Business Flow:**
```
ShopEasy adds OpenAI:
  ↓
Create AIProvider:
  Name: "Primary GPT-4"
  Provider: OpenAI
  Model: "gpt-4"
  ApiKey: "sk-proj-abc123" (encrypted!)
  ApiUrl: "https://api.openai.com/v1"
  Settings: {
    "temperature": 0.7,
    "max_tokens": 1000,
    "top_p": 1
  }
  IsDefault: true
  IsActive: true
  ↓
When flow needs AI:
  1. Get default AI provider for tenant
  2. Use those credentials to call OpenAI
  3. Track: RequestCount++, TokensUsed += 170, TotalCost += $0.0066
  ↓
ShopEasy adds Anthropic as backup:
  Name: "Claude for Complex Queries"
  Provider: Anthropic
  Model: "claude-3-opus"
  IsDefault: false
  ↓
Can choose per flow node which provider to use
```

**Why You Need It:**
- Flexibility: Support multiple AI providers
- Tenant control: They choose which AI to use
- Cost tracking: Know usage per provider
- Failover: If OpenAI is down, use Anthropic

---

### **23. VoiceProvider** - TTS/STT Configuration

**Purpose:** Stores configuration for voice services (text-to-speech, speech-to-text).

**What It Does:**
- Configures TTS providers (ElevenLabs, Google, etc.)
- Configures STT providers (Deepgram, AssemblyAI)
- Stores voice settings (voice ID, language)
- Tracks usage

**Business Flow:**
```
ShopEasy enables voice calls:
  ↓
Add TTS provider (ElevenLabs):
  Name: "Rachel Voice"
  Provider: ElevenLabs
  Type: TTS
  ApiKey: "abc123"
  Settings: {
    "voice_id": "rachel",
    "model_id": "eleven_multilingual_v2",
    "stability": 0.5,
    "similarity_boost": 0.75
  }
  IsDefault: true
  ↓
Add STT provider (Deepgram):
  Name: "Nova-2 STT"
  Provider: Deepgram
  Type: STT
  Settings: {
    "model": "nova-2",
    "language": "en-US",
    "punctuate": true,
    "diarize": false
  }
  ↓
When customer calls:
  1. Use Deepgram to convert speech → text
  2. Process with AI
  3. Use ElevenLabs to convert response → speech
  4. Track: MinutesUsed++, TotalCost += $0.0349
```

**Why You Need It:**
- Voice support: Enable phone calls
- Quality: Use best voices (ElevenLabs > Google)
- Flexibility: Different voices per tenant
- Cost tracking: Voice is expensive, track it!

---

### **24. WebRTCConfig** - Voice Call Setup

**Purpose:** Configuration for WebRTC voice calls (the technology behind calls).

**What It Does:**
- STUN/TURN server settings (for NAT traversal)
- Audio codec configuration
- Connection settings

**Business Flow:**
```
Setup voice infrastructure:
  ↓
Create WebRTCConfig:
  Name: "Primary Voice Config"
  StunServer: "stun:stun.aurora.com:3478"
  TurnServer: "turn:turn.aurora.com:3478"
  TurnUsername: "user123"
  TurnPassword: "pass123"
  AudioSettings: {
    "codec": "opus",
    "bitrate": 48000,
    "sampleRate": 48000
  }
  IsDefault: true
  ↓
When customer clicks "Call Us" button:
  1. Establish WebRTC connection using this config
  2. Stream audio bidirectionally
  3. Process with STT/TTS
```

**Why You Need It:**
- Voice calls: Required for WebRTC
- Quality: Configure audio settings
- Reliability: TURN servers for NAT issues

---

## 🔌 **INTEGRATION MODULE (3 Entities)**

This module connects to customer's external systems.

---

### **25. Connector** - External API Connection

**Purpose:** Represents connection to customer's API or database.

**What It Does:**
- Stores API base URL
- Manages authentication (API key, OAuth, etc.)
- Configures retry & timeout settings
- Tracks health

**Business Flow:**
```
ShopEasy connects their Shopify store:
  ↓
Create Connector:
  Name: "ShopEasy Shopify"
  Type: REST
  BaseUrl: "https://shopeasy.myshopify.com"
  Authentication: {
    "type": "bearer",
    "token": "shpat_abc123xyz"
  }
  Headers: {
    "X-Shopify-Access-Token": "...",
    "Content-Type": "application/json"
  }
  TimeoutSeconds: 30
  RetryCount: 3
  IsActive: true
  ↓
Test connection:
  LastTestedAt: Now
  LastTestSuccess: true
  ↓
Flow can now call Shopify APIs
```

**Why You Need It:**
- Integration: Connect to ANY external system
- Centralized config: One place for credentials
- Health monitoring: Know if connection is working
- Reusability: One connector, many endpoints

---

### **26. ConnectorEndpoint** - Specific API Endpoint

**Purpose:** One specific API call (like GET /products or POST /orders).

**What It Does:**
- Defines HTTP method and path
- Request template (what data to send)
- Response mapping (extract specific fields)
- Caching configuration

**Business Flow:**
```
Add endpoints for Shopify connector:

Endpoint 1 - Get Product:
  ConnectorId: shopify-connector
  Name: "Get Product by Name"
  Method: GET
  Path: "/admin/api/2024-01/products.json?title={{product_name}}"
  ResponseMapping: {
    "productId": "$.products[0].id",
    "name": "$.products[0].title",
    "price": "$.products[0].variants[0].price",
    "stock": "$.products[0].variants[0].inventory_quantity"
  }
  CacheEnabled: true
  CacheDurationSeconds: 300 (5 minutes)

Endpoint 2 - Create Order:
  Name: "Create Order"
  Method: POST
  Path: "/admin/api/2024-01/orders.json"
  RequestTemplate: {
    "order": {
      "line_items": "{{cart_items}}",
      "customer": { "id": "{{customer_id}}" }
    }
  }
  ResponseMapping: {
    "orderId": "$.order.id",
    "orderNumber": "$.order.name"
  }
  ↓
When flow calls endpoint:
  1. Replace {{variables}} with actual values
  2. Call Shopify API
  3. Extract fields using ResponseMapping
  4. Cache result if enabled
  5. Track: ExecutionCount++, SuccessCount++, AverageDurationMs
```

**Why You Need It:**
- Flexibility: Each endpoint configured separately
- Templates: Dynamic parameters
- Mapping: Extract only needed data
- Performance: Caching reduces API calls

---

### **27. ConnectorLog** - API Call Logging

**Purpose:** Debug log of every API call made.

**What It Does:**
- Records request & response
- Stores HTTP status code
- Measures duration
- Links to conversation

**Business Flow:**
```
Flow executes API node:
  ↓
Create ConnectorLog:
  ConnectorId: shopify-connector
  EndpointId: get-product-endpoint
  ConversationId: conv-123
  FlowExecutionId: exec-456
  Method: "GET"
  Url: "https://shopeasy.myshopify.com/admin/api/2024-01/products.json?title=AirPods"
  Request: { headers: {...}, params: {...} }
  Response: { 
    status: 200,
    data: { products: [{ id: 123, title: "AirPods Pro", ... }] }
  }
  StatusCode: 200
  IsSuccess: true
  DurationMs: 145
  ↓
If error:
  StatusCode: 500
  IsSuccess: false
  ErrorMessage: "Internal Server Error"
  ↓
Dashboard shows:
  - API health: 98.7% success rate
  - Average response time: 187ms
  - Failed calls: Click to see details
```

**Why You Need It:**
- Debugging: "Why isn't product showing?" → Check logs
- Monitoring: Know if external APIs are slow
- Compliance: Audit trail of data access
- Optimization: Find slow endpoints

---

## 📚 **KNOWLEDGE MODULE (3 Entities)**

This module provides pre-built content and AI training.

---

### **28. MessageTemplate** - Pre-built Responses

**Purpose:** Reusable message templates with variables.

**What It Does:**
- Stores template text with placeholders
- Multi-language support
- Categorization
- Usage tracking

**Business Flow:**
```
Create templates:

Template 1:
  Name: "Order Confirmation"
  Category: "Orders"
  Content: "Your order #{{order_number}} has been confirmed! Estimated delivery: {{delivery_date}}. Track at: {{tracking_url}}"
  Variables: ["order_number", "delivery_date", "tracking_url"]
  Language: "en"
  ↓
Use in flow:
  1. Select template
  2. Fill variables: order_number="12345", delivery_date="Jan 31"
  3. Send: "Your order #12345 has been confirmed! Estimated delivery: Jan 31..."
  ↓
Track: UsageCount++
```

**Why You Need It:**
- Consistency: Same messages across conversations
- Efficiency: Don't type same thing 1000 times
- Multi-language: Translate once, use everywhere
- Testing: A/B test different templates

---

### **29. KnowledgeBase** - RAG Documents

**Purpose:** Documents for Retrieval Augmented Generation (giving AI context).

**What It Does:**
- Stores help docs, FAQs, policies
- Generates embeddings for semantic search
- Categorization and tagging
- Tracks usage

**Business Flow:**
```
Upload return policy document:
  ↓
Create KnowledgeBase:
  Title: "Return Policy"
  Content: "Items can be returned within 30 days...full text..."
  Category: "Policies"
  Tags: ["returns", "refunds", "policies"]
  FileUrl: "https://cdn.aurora.com/docs/return-policy.pdf"
  FileType: "pdf"
  Status: Published
  ↓
Generate embeddings:
  Embeddings: [0.023, -0.145, 0.089, ...] (1536-dim vector)
  ↓
When customer asks: "Can I return headphones?"
  1. Convert question to embedding
  2. Find similar documents (vector search)
  3. Return policy is most similar
  4. Include in AI prompt: "Use this policy to answer: {{return_policy_content}}"
  5. AI generates accurate answer using policy
  ↓
Track: AccessCount++, LastAccessedAt
```

**Why You Need It:**
- Accurate answers: AI uses real docs, not hallucinations
- Updates: Update docs → AI instantly has new info
- Compliance: Answers follow official policies
- Analytics: Know which docs are used most

---

### **30. Intent** - NLU Classification

**Purpose:** Classify what customer wants (product_question, return_request, etc.).

**What It Does:**
- Stores intent name
- Training examples
- Auto-triggers flows
- Tracks match rate

**Business Flow:**
```
Define intents:

Intent 1:
  Name: "product_question"
  Description: "Customer asking about products"
  Examples: [
    "Tell me about AirPods",
    "What's the price of iPhone?",
    "Do you have Samsung Galaxy?",
    "Show me laptops"
  ]
  FlowId: product-support-flow (auto-trigger)
  IsActive: true
  ↓
Intent 2:
  Name: "return_request"
  Examples: [
    "I want to return my order",
    "How do I get a refund?",
    "Can I return this?"
  ]
  FlowId: returns-flow
  ↓
When message arrives: "I need help with a return"
  1. Call AI: "Classify intent: 'I need help with a return'"
  2. AI responds: "return_request" (confidence: 0.92)
  3. Update intent: MatchCount++, Confidence = 0.92
  4. Trigger returns-flow
```

**Why You Need It:**
- Routing: Auto-route to correct flow
- Analytics: Know what customers want
- Improvement: Low confidence = need better training
- Automation: No manual routing needed

---

## 👥 **ACCESS CONTROL MODULE (3 Entities)**

This module manages users and permissions.

---

### **31. User** - Team Member

**Purpose:** Someone from tenant's team (admin, agent, viewer).

**What It Does:**
- Stores login credentials
- Assigns role
- Tracks login activity
- Multi-factor auth support

**Business Flow:**
```
ShopEasy adds agent:
  ↓
Create User:
  Email: "agent@shopeasy.com"
  FirstName: "Sarah"
  LastName: "Johnson"
  PasswordHash: "..." (bcrypt hash)
  Role: Agent
  IsActive: true
  ↓
Login:
  1. Verify email + password
  2. Generate JWT token
  3. Update: LastLoginAt, LastLoginIP, LoginCount++
  ↓
Access control:
  - Admin: Can do everything
  - Manager: Can view analytics, manage agents
  - Agent: Can view/respond to conversations
  - Viewer: Read-only access
```

**Why You Need It:**
- Team management: Multiple users per tenant
- Security: Each user has own login
- Audit: Track who did what
- Access control: Different permissions per role

---

### **32. Role** - Custom Roles

**Purpose:** Define custom roles beyond default (Admin/Agent/etc.).

**What It Does:**
- Custom role names
- List of permissions
- Can assign to users

**Business Flow:**
```
Create custom role:
  ↓
Role:
  Name: "Support Lead"
  Description: "Can manage agents and view all analytics"
  Permissions: [
    "conversations.view",
    "conversations.assign",
    "users.manage",
    "analytics.view",
    "flows.view"
  ]
  IsSystemRole: false
  UserCount: 3
  ↓
Assign to user:
  User.Role = "Support Lead"
  ↓
Check permission:
  if (user.Role.Permissions.Contains("flows.edit")) {
    // Allow
  }
```

**Why You Need It:**
- Flexibility: Custom roles per tenant
- Granular control: Specific permissions
- Scale: As team grows, need different roles

---

### **33. Permission** - Access Control

**Purpose:** Defines what actions are available (global list).

**What It Does:**
- Lists all possible permissions
- Categorizes by resource
- Used by roles

**Business Flow:**
```
System-defined permissions:

Permission 1:
  Name: "conversations.view"
  Category: "Conversations"
  Description: "View conversations"
  Resource: "Conversation"
  Action: "View"

Permission 2:
  Name: "conversations.edit"
  Category: "Conversations"
  Resource: "Conversation"
  Action: "Edit"

Permission 3:
  Name: "flows.publish"
  Category: "Flows"
  Resource: "Flow"
  Action: "Publish"

...and 50+ more permissions

↓
Roles contain lists of these permissions
↓
Check access:
  var hasPermission = await _permissionService.Check(
    userId,
    "flows.publish"
  );
```

**Why You Need It:**
- Standardization: Consistent permission names
- Documentation: Clear what each allows
- UI: Show/hide features based on permissions

---

## 🎬 **COMPLETE BUSINESS SCENARIO**

Let me walk you through a COMPLETE customer interaction showing how ALL entities work together:

---

### **Scenario: John wants to buy headphones via WhatsApp**

---

#### **Step 1: Setup (One-time)**

```
ShopEasy (your client) has:
  ✅ Tenant record (ID: tenant-shopeasy)
  ✅ TenantSubscription (Pro plan, $299/month)
  ✅ PricingRule (66% markup on tokens)
  ✅ Channel (WhatsApp configured)
  ✅ AIProvider (OpenAI GPT-4)
  ✅ Connector (Shopify API)
  ✅ ConnectorEndpoint (Get Product, Create Order)
  ✅ Flow (Product Purchase Flow)
  ✅ Intent (product_purchase)
```

---

#### **Step 2: John Sends Message**

```
10:00:00 - John sends WhatsApp: "I want to buy AirPods Pro"
  ↓
WhatsApp webhook → Your platform
  ↓
1. Create/Update Customer:
   - Name: "John Doe"
   - Phone: "+1234567890"
   - TotalConversations: 12 (has bought before)
   - TenantId: tenant-shopeasy
   ↓
2. Create Conversation:
   - CustomerId: John's ID
   - Channel: WhatsApp
   - SessionId: "wa_abc123"
   - Status: Active
   - TenantId: tenant-shopeasy
   ↓
3. Create Message (User):
   - ConversationId: conv-123
   - Role: User
   - Content: "I want to buy AirPods Pro"
   - Format: Text
   ↓
4. Create ChannelMessage:
   - MessageId: msg-001
   - Direction: Inbound
   - ExternalMessageId: "wamid.123"
   - RawData: {entire WhatsApp payload}
```

---

#### **Step 3: AI Detects Intent**

```
10:00:01 - Call OpenAI to detect intent
  ↓
AI Request:
  "Classify intent: 'I want to buy AirPods Pro'"
  Input tokens: 15
  Output tokens: 5
  ↓
AI Response:
  Intent: "product_purchase"
  Confidence: 0.95
  ↓
5. Create UsageBreakdown:
   - ConversationId: conv-123
   - MessageId: msg-001
   - Type: Token
   - Provider: "openai"
   - Model: "gpt-4"
   - InputQuantity: 15
   - OutputQuantity: 5
   - TotalQuantity: 20
   - YourTotalCost: $0.00075 (15×$0.03/1K + 5×$0.06/1K)
   - MarkupPercent: 66%
   - ClientTotalCost: $0.00125
   - ProfitAmount: $0.0005
   - UsedAt: 10:00:01
   - TenantId: tenant-shopeasy
   ↓
6. Update Conversation:
   - TokensUsed: 20
   - TotalCost: $0.00125
   ↓
7. Update Usage (aggregated):
   - Type: Token, Quantity: 20, Cost: $0.00125
   ↓
8. Check CostAlert:
   - Current spend this month: $245.50
   - Alert threshold: $250 (not reached yet)
```

---

#### **Step 4: Trigger Flow**

```
10:00:02 - Intent "product_purchase" triggers "Product Purchase Flow"
  ↓
9. Create FlowExecution:
   - FlowId: product-purchase-flow
   - ConversationId: conv-123
   - CurrentNodeId: "node_1_welcome"
   - Variables: { "user_message": "I want to buy AirPods Pro" }
   - Status: Running
   - StartedAt: 10:00:02
```

---

#### **Step 5: Execute Node 1 (Welcome)**

```
10:00:02 - Execute node_1_welcome (type: Message)
  ↓
Configuration: { "text": "Great! Let me help you with that." }
  ↓
10. Create Message (Assistant):
    - ConversationId: conv-123
    - Role: Assistant
    - Content: "Great! Let me help you with that."
    ↓
11. Create ChannelMessage (send to WhatsApp):
    - Direction: Outbound
    - Status: Sent
    ↓
12. Update FlowExecution:
    - CurrentNodeId: "node_2_extract_product"
    - ExecutedNodes: ["node_1_welcome"]
    ↓
13. Update Conversation:
    - MessageCount: 2
```

---

#### **Step 6: Execute Node 2 (Extract Product)**

```
10:00:03 - Execute node_2_extract_product (type: AI)
  ↓
Configuration: {
  "prompt": "Extract product name from: {{user_message}}",
  "outputVariable": "product_name"
}
  ↓
Call OpenAI:
  Input: "Extract product name from: I want to buy AirPods Pro"
  Input tokens: 25
  Output tokens: 10
  Result: "AirPods Pro"
  ↓
14. Create UsageBreakdown:
    - InputQuantity: 25, OutputQuantity: 10
    - YourTotalCost: $0.00135
    - ClientTotalCost: $0.00224
    - ProfitAmount: $0.00089
    ↓
15. Update FlowExecution:
    - Variables: { 
        "user_message": "I want to buy AirPods Pro",
        "product_name": "AirPods Pro"
      }
    - CurrentNodeId: "node_3_fetch_product"
    ↓
16. Update Conversation:
    - TokensUsed: 55 (20 + 35)
    - TotalCost: $0.00349 ($0.00125 + $0.00224)
```

---

#### **Step 7: Execute Node 3 (Fetch Product)**

```
10:00:04 - Execute node_3_fetch_product (type: API)
  ↓
Configuration: {
  "connectorId": "shopify-connector",
  "endpointId": "get-product",
  "parameters": { "title": "{{product_name}}" },
  "outputVariable": "product_data"
}
  ↓
Call Shopify API:
  GET https://shopeasy.myshopify.com/admin/api/2024-01/products.json?title=AirPods%20Pro
  ↓
17. Create ConnectorLog:
    - ConnectorId: shopify-connector
    - ConversationId: conv-123
    - FlowExecutionId: exec-456
    - Method: "GET"
    - Url: "https://shopeasy.myshopify.com/..."
    - Request: { headers, params }
    - Response: {
        products: [{
          id: 7890,
          title: "AirPods Pro",
          variants: [{
            price: "249.00",
            inventory_quantity: 15
          }]
        }]
      }
    - StatusCode: 200
    - IsSuccess: true
    - DurationMs: 187
    ↓
18. Update FlowExecution:
    - Variables: {
        "user_message": "...",
        "product_name": "AirPods Pro",
        "product_data": {
          "id": 7890,
          "name": "AirPods Pro",
          "price": 249.00,
          "stock": 15
        }
      }
    - CurrentNodeId: "node_4_check_stock"
```

---

#### **Step 8: Execute Node 4 (Check Stock)**

```
10:00:05 - Execute node_4_check_stock (type: Condition)
  ↓
Configuration: {
  "variable": "product_data.stock",
  "operator": ">",
  "value": 0
}
  ↓
Evaluate: 15 > 0 = TRUE
  ↓
19. Update FlowExecution:
    - CurrentNodeId: "node_5_show_product"
```

---

#### **Step 9: Execute Node 5 (Show Product)**

```
10:00:05 - Execute node_5_show_product (type: Message)
  ↓
Configuration: {
  "text": "{{product_data.name}} is available for ${{product_data.price}}!\n\nWould you like to proceed with purchase?"
}
  ↓
Replace variables:
  "AirPods Pro is available for $249.00!\n\nWould you like to proceed with purchase?"
  ↓
20. Create Message (Assistant):
    - Content: "AirPods Pro is available for $249.00!..."
    ↓
21. Create ChannelMessage (send to WhatsApp):
    - Status: Sent
    ↓
22. Update FlowExecution:
    - Status: Waiting (waiting for user response)
    - CurrentNodeId: "node_6_wait_confirm"
    ↓
23. Update Conversation:
    - MessageCount: 3
```

---

#### **Step 10: John Confirms Purchase**

```
10:00:30 - John sends: "Yes, please!"
  ↓
24. Create Message (User):
    - Content: "Yes, please!"
    ↓
25. Update FlowExecution:
    - Status: Running
    - CurrentNodeId: "node_7_create_order"
```

---

#### **Step 11: Execute Node 7 (Create Order)**

```
10:00:31 - Execute node_7_create_order (type: API)
  ↓
Configuration: {
  "connectorId": "shopify-connector",
  "endpointId": "create-order",
  "parameters": {
    "customer_id": "{{customer.external_id}}",
    "line_items": [{ "variant_id": "{{product_data.id}}", "quantity": 1 }]
  }
}
  ↓
Call Shopify API:
  POST https://shopeasy.myshopify.com/admin/api/2024-01/orders.json
  ↓
26. Create ConnectorLog:
    - Method: "POST"
    - Response: { order: { id: 12345, name: "#1001" } }
    - StatusCode: 201
    - IsSuccess: true
    ↓
27. Update FlowExecution:
    - Variables: { ..., "order_id": 12345, "order_number": "#1001" }
    - CurrentNodeId: "node_8_confirm"
```

---

#### **Step 12: Execute Node 8 (Order Confirmation)**

```
10:00:32 - Execute node_8_confirm (type: Message)
  ↓
Use MessageTemplate:
  Template: "Order Confirmation"
  Variables: { order_number: "#1001", delivery_date: "Jan 31" }
  ↓
28. Create Message (Assistant):
    - Content: "Your order #1001 has been confirmed! Estimated delivery: Jan 31..."
    ↓
29. Update MessageTemplate:
    - UsageCount++
    ↓
30. Update FlowExecution:
    - Status: Completed
    - CompletedAt: 10:00:32
    - DurationMs: 30,000 (30 seconds)
    ↓
31. Update Flow:
    - ExecutionCount++
    - SuccessCount++
    - AverageDurationSeconds: Update average
    ↓
32. Update Conversation:
    - Status: Completed
    - EndedAt: 10:00:32
    - IsResolved: true
    - MessageCount: 5
    - Final TokensUsed: 55
    - Final TotalCost: $0.00349
```

---

#### **Step 13: End of Day - Aggregate Usage**

```
23:59:59 - Daily aggregation job runs
  ↓
33. Update Usage (daily summary):
    - TenantId: tenant-shopeasy
    - Type: Token
    - Provider: "openai"
    - Model: "gpt-4"
    - Quantity: 150,000 (all conversations today)
    - BaseCost: $4.50
    - CustomerCost: $7.50
    - UsedAt: 2025-01-29 (today)
```

---

#### **Step 14: Check Alerts**

```
After this conversation:
  ↓
34. Update CostAlert:
    - CurrentAmount: $245.85 (was $245.50, added $0.35)
    - Still below $250 threshold
    - No alert triggered
```

---

#### **Step 15: End of Month - Generate Invoice**

```
January 31, 23:59:59
  ↓
35. Create Invoice:
    - TenantId: tenant-shopeasy
    - InvoiceNumber: "INV-2025-01-001"
    - PeriodStart: 2025-01-01
    - PeriodEnd: 2025-01-31
    ↓
Query all UsageBreakdown for month:
  - Total tokens: 2,500,000
  - Total voice minutes: 245
  - Total SMS: 500
  ↓
Calculate costs:
    - SubscriptionFee: $299
    - TokenCost: $125 (aggregated from UsageBreakdown)
    - VoiceCost: $142.50
    - SMSCost: $15
    - EmailCost: $5
    - TotalAmount: $586.50
    - TaxAmount: $58.65
    - FinalAmount: $645.15
    - Status: Pending
    ↓
Send invoice email to ShopEasy
  ↓
ShopEasy pays:
36. Update Invoice:
    - Status: Paid
    - PaidAt: 2025-02-05
    - PaymentTransactionId: "ch_abc123"
```

---

## 🎯 **SUMMARY: How Entities Work Together**

### **Every customer interaction uses:**

1. **Tenant** - Which company?
2. **TenantSubscription** - What's their plan?
3. **PricingRule** - What do we charge?
4. **Customer** - Who's chatting?
5. **Conversation** - This session
6. **Message** - Each message
7. **Channel** - Which platform?
8. **ChannelMessage** - Raw data
9. **Intent** - What do they want?
10. **Flow** - Which automation?
11. **FlowExecution** - Track progress
12. **FlowNode** - Each step
13. **FlowConnection** - How nodes link
14. **FlowVariable** - Store data
15. **AIProvider** - Which LLM?
16. **UsageBreakdown** - EVERY token tracked ⭐
17. **Usage** - Daily summary
18. **CostAlert** - Budget warning?
19. **Connector** - External API
20. **ConnectorEndpoint** - Which endpoint?
21. **ConnectorLog** - API call log
22. **MessageTemplate** - Pre-built messages
23. **User** - Who's viewing this?
24. **Invoice** - Monthly bill

### **At month end:**
- **PricingHistory** - Track rate changes
- **UsageQuota** - Check limits
- **FlowVersion** - Track changes

### **For voice calls, add:**
- **VoiceProvider** - TTS/STT
- **WebRTCConfig** - Call setup

### **For AI training, add:**
- **KnowledgeBase** - RAG docs

### **For team management:**
- **Role** - Custom roles
- **Permission** - Access control

---

## ✅ **Conclusion**

**Every single entity has a purpose!**

- 10 entities manage **billing** (zero disputes!)
- 5 entities manage **conversations** (all channels!)
- 6 entities power **automation** (visual flows!)
- 3 entities manage **AI/Voice** (any provider!)
- 3 entities handle **integrations** (any API!)
- 3 entities provide **knowledge** (RAG & templates!)
- 3 entities control **access** (team management!)

**Together, they run your ENTIRE AI customer service business!** 🚀

