# Aurora FlowStudio - Data Transfer Objects (DTOs)

This folder contains all Data Transfer Objects used for API communication between the client and server.

## 📁 Folder Structure

```
DTO/
├── Base/
│   └── BaseDTO.cs                 - Base DTOs and API responses
├── Core/
│   └── TenantDTO.cs              - Tenant and settings DTOs
├── Identity/
│   └── UserDTO.cs                - User, auth, FIDO2, role DTOs
├── Flow/
│   └── FlowDTO.cs                - Flow, node, menu DTOs
├── Conversation/
│   └── ConversationDTO.cs        - Conversation, message, customer DTOs
├── Integration/
│   └── ConnectorDTO.cs           - Connector and endpoint DTOs
├── AI/
│   └── AIDTO.cs                  - AI, TTS, STT provider DTOs
└── Commerce/
    └── CommerceDTO.cs            - Product, cart, order DTOs
```

## 🎯 DTO Categories

### Base DTOs (`DTO/Base/`)
**Purpose**: Common response wrappers and base classes

- `BaseDTO` - Base class with Id, CreatedAt, UpdatedAt
- `ApiResponse<T>` - Standard API response wrapper
- `PagedResponse<T>` - Paginated results
- `FlowExecutionResponse` - Response sent to client apps during flow execution
- `ResponseSourceInfo` - Information about data source
- `NextAction` - Available actions for user
- `ConversationState` - Current conversation state

**Key Feature**: `FlowExecutionResponse` includes:
- Content to display
- Source information (API, Database, LLM, etc.)
- Whether LLM processed the data
- Client metadata
- Next available actions

### Core DTOs (`DTO/Core/`)
**Entities**: Tenant, Settings

- `TenantDTO` - Tenant information
- `CreateTenantRequest` - Create new tenant
- `UpdateTenantRequest` - Update tenant
- `TenantSettingDTO` - Tenant settings
- `TenantStatsDTO` - Tenant statistics

### Identity DTOs (`DTO/Identity/`)
**Entities**: User, FIDO2, Roles, Permissions

- `UserDTO` - User profile
- `RegisterUserRequest` - Register new user
- `UpdateUserProfileRequest` - Update profile
- `FIDO2RegisterRequest` - Register FIDO2 credential
- `FIDO2AuthRequest` - Authenticate with FIDO2
- `FIDO2CredentialDTO` - FIDO2 credential info
- `AuthResponseDTO` - Authentication response with tokens
- `RoleDTO` - Role information
- `PermissionDTO` - Permission information
- `AssignRoleRequest` - Assign role to user
- `UserActivityDTO` - User activity log

**Authentication Flow**:
1. Register user → `RegisterUserRequest`
2. Setup FIDO2 → `FIDO2RegisterRequest`
3. Login with FIDO2 → `FIDO2AuthRequest`
4. Get tokens → `AuthResponseDTO`

### Flow DTOs (`DTO/Flow/`)
**Entities**: Flow, Node, Menu, MenuItem

- `FlowDTO` - Flow information
- `CreateFlowRequest` - Create new flow
- `UpdateFlowRequest` - Update flow
- `FlowNodeDTO` - Node information
- `SaveFlowNodeRequest` - Create/update node
- `NodeResponseSourceDTO` - Node data source configuration
- `FlowConnectionDTO` - Connection between nodes
- `MenuDTO` - Menu (welcome screen)
- `CreateMenuRequest` - Create menu
- `MenuItemDTO` - Menu item (flow option)
- `CreateMenuItemRequest` - Create menu item
- `FlowAnalyticsDTO` - Flow analytics
- `PublishFlowRequest` - Publish flow
- `FlowVariableDTO` - Flow variable

**Key Feature**: `NodeResponseSourceDTO` shows:
- Where node gets data (API, Database, Static, LLM)
- Whether to process with LLM
- Whether to include source info in response
- Client metadata to send

### Conversation DTOs (`DTO/Conversation/`)
**Entities**: Conversation, Message, Customer

- `ConversationDTO` - Conversation information
- `StartConversationRequest` - Start new conversation
- `MessageDTO` - Message details
- `SendMessageRequest` - Send message
- `MessageAttachmentDTO` - File attachments
- `SentimentDTO` - Sentiment analysis
- `CustomerDTO` - Customer profile
- `CreateCustomerRequest` - Create customer
- `UpdateCustomerRequest` - Update customer
- `CustomerAuthRequest` - Customer authentication
- `SubmitFeedbackRequest` - Submit feedback
- `ConversationNoteDTO` - Agent notes
- `AddConversationNoteRequest` - Add note

**Customer Auth Types**: Anonymous, Email, Phone, OAuth, FIDO2, MagicLink, OTP, Custom

### Integration DTOs (`DTO/Integration/`)
**Entities**: Connector, Endpoint

- `ConnectorDTO` - Connector information
- `CreateConnectorRequest` - Create connector
- `UpdateConnectorRequest` - Update connector
- `TestConnectorRequest` - Test connection
- `ConnectorTestResultDTO` - Test result
- `ConnectorEndpointDTO` - Endpoint details
- `CreateEndpointRequest` - Create endpoint
- `ExecuteEndpointRequest` - Execute endpoint
- `ExecuteEndpointResponseDTO` - Execution result
- `ConnectorMetricsDTO` - Usage metrics
- `ConnectorLogDTO` - Execution logs

**Connector Types**: 50+ (REST API, GraphQL, MySQL, PostgreSQL, MongoDB, Salesforce, Shopify, Stripe, etc.)

### AI DTOs (`DTO/AI/`)
**Entities**: AI Provider, TTS, STT

- `AIProviderDTO` - AI provider info
- `CreateAIProviderRequest` - Create provider
- `AIModelDTO` - Model details
- `AICompletionRequest` - Request completion
- `ChatMessage` - Chat message format
- `AICompletionResponseDTO` - Completion response
- `TTSProviderDTO` - TTS provider
- `VoiceConfigDTO` - Voice configuration
- `TTSRequest` - Text-to-speech request
- `TTSResponseDTO` - Audio response
- `STTProviderDTO` - STT provider
- `STTRequest` - Speech-to-text request
- `STTResponseDTO` - Transcription result
- `TranscriptSegment` - Transcript segment
- `AIProviderMetricsDTO` - Usage metrics

**Platform Model**: Tenants configure preferences, platform executes through predefined providers

### Commerce DTOs (`DTO/Commerce/`)
**Entities**: Product, Cart, Order, Payment

- `ProductDTO` - Product details
- `CreateProductRequest` - Create product
- `UpdateProductRequest` - Update product
- `ProductVariantDTO` - Product variant
- `ProductCategoryDTO` - Category
- `CartDTO` - Shopping cart
- `AddToCartRequest` - Add item to cart
- `UpdateCartItemRequest` - Update quantity
- `CartItemDTO` - Cart item
- `OrderDTO` - Order details
- `CreateOrderRequest` - Create order
- `OrderItemDTO` - Order item
- `AddressDTO` - Shipping/billing address
- `ProcessPaymentRequest` - Process payment
- `PaymentResponseDTO` - Payment result
- `ProductReviewDTO` - Product review
- `SubmitReviewRequest` - Submit review
- `DiscountDTO` - Discount/coupon
- `ApplyDiscountRequest` - Apply discount
- `ProductSearchRequest` - Search products

## 🔄 Request/Response Pattern

### Standard Pattern
```
Request → DTO Request Class → API Controller → Service → Repository → Database
Database → Entity → Service → DTO Response Class → API Controller → Response
```

### Example: Create Flow
```csharp
// Request
POST /api/flows
Body: CreateFlowRequest
{
    "name": "customer-support",
    "displayName": "Customer Support",
    "type": "ChatBot",
    "category": "CustomerService"
}

// Response
ApiResponse<FlowDTO>
{
    "success": true,
    "message": "Flow created successfully",
    "data": {
        "id": "guid",
        "name": "customer-support",
        "displayName": "Customer Support",
        "status": "Draft",
        ...
    }
}
```

### Example: Flow Execution Response (Sent to Client App)
```csharp
FlowExecutionResponse
{
    "conversationId": "guid",
    "messageId": "guid",
    "content": "Your order #12345 will arrive tomorrow",
    "contentType": "text",
    "sourceInfo": {
        "sourceType": "API",
        "processedByLLM": true,
        "connectorName": "Shopify",
        "latencyMs": 245,
        "fromCache": false
    },
    "nextActions": [
        {
            "type": "button",
            "label": "Track Order",
            "value": "track"
        },
        {
            "type": "button",
            "label": "Main Menu",
            "value": "menu"
        }
    ],
    "clientMetadata": {
        "orderId": "12345",
        "trackingNumber": "1Z999AA10123456784"
    },
    "state": {
        "status": "active",
        "currentNodeId": "guid",
        "isComplete": false
    }
}
```

## ✨ DTO Conventions

### Naming
- Response DTOs: `EntityDTO` (e.g., `FlowDTO`, `UserDTO`)
- Create requests: `CreateEntityRequest` (e.g., `CreateFlowRequest`)
- Update requests: `UpdateEntityRequest` (e.g., `UpdateFlowRequest`)
- Action requests: `ActionEntityRequest` (e.g., `PublishFlowRequest`)

### Properties
- Use proper enums from `Aurora.FlowStudio.Entity.Enums`
- Use nullable types (`?`) for optional fields
- Include navigation property names (e.g., `FlowName` when returning `FlowId`)
- Initialize collections to empty lists

### API Response
Always wrap responses in `ApiResponse<T>`:
```csharp
return new ApiResponse<FlowDTO>
{
    Success = true,
    Message = "Flow retrieved successfully",
    Data = flowDto,
    Timestamp = DateTime.UtcNow
};
```

### Pagination
Use `PagedResponse<T>` for lists:
```csharp
return new PagedResponse<FlowDTO>
{
    Items = flows,
    TotalCount = 150,
    Page = 1,
    PageSize = 20
};
```

## 🎯 Usage Examples

### Create and Authenticate User (FIDO2)
```csharp
// 1. Register user
var registerRequest = new RegisterUserRequest
{
    Email = "user@company.com",
    FirstName = "John",
    LastName = "Doe",
    TenantId = tenantId
};

// 2. Setup FIDO2
var fido2Request = new FIDO2RegisterRequest
{
    AttestationResponse = "...",
    DeviceName = "iPhone 15",
    SetAsPrimary = true
};

// 3. Login with FIDO2
var authRequest = new FIDO2AuthRequest
{
    AssertionResponse = "...",
    CredentialId = "..."
};

// 4. Get tokens
AuthResponseDTO response = await Authenticate(authRequest);
// response.AccessToken, response.RefreshToken
```

### Create Flow with Menu
```csharp
// 1. Create flow
var flowRequest = new CreateFlowRequest
{
    Name = "product-inquiry",
    DisplayName = "Product Inquiry",
    Type = FlowType.ChatBot
};

// 2. Create menu
var menuRequest = new CreateMenuRequest
{
    Name = "main-menu",
    DisplayName = "Main Menu",
    WelcomeMessage = "How can I help you?",
    DisplayStyle = MenuDisplayStyle.List
};

// 3. Add menu items
var menuItem = new CreateMenuItemRequest
{
    Title = "Browse Products",
    Type = MenuItemType.Flow,
    TargetFlowId = flowId
};
```

### Execute Flow
```csharp
// 1. Start conversation
var startRequest = new StartConversationRequest
{
    FlowId = flowId,
    Channel = ConversationChannel.WebChat
};

// 2. Send message
var messageRequest = new SendMessageRequest
{
    ConversationId = conversationId,
    Content = "Show me laptops",
    Type = MessageType.Text
};

// 3. Get response (FlowExecutionResponse)
// Contains: content, sourceInfo, nextActions, clientMetadata
```

### E-Commerce Flow
```csharp
// 1. Search products
var searchRequest = new ProductSearchRequest
{
    Query = "laptop",
    MinPrice = 500,
    MaxPrice = 2000,
    InStock = true
};

// 2. Add to cart
var addToCartRequest = new AddToCartRequest
{
    ProductId = productId,
    Quantity = 1
};

// 3. Create order
var orderRequest = new CreateOrderRequest
{
    CartId = cartId,
    CustomerEmail = "customer@email.com",
    ShippingAddress = address,
    PaymentMethod = PaymentMethod.CreditCard
};

// 4. Process payment
var paymentRequest = new ProcessPaymentRequest
{
    OrderId = orderId,
    Amount = 1299.99m,
    PaymentMethod = PaymentMethod.Stripe
};
```

## 📊 Statistics

- **Total DTO Files**: 8
- **Total DTOs**: 100+
- **Request DTOs**: 40+
- **Response DTOs**: 40+
- **Helper DTOs**: 20+

## 🚀 Benefits

1. **Type Safety** - Strongly typed request/response
2. **Clear Contracts** - API contracts are explicit
3. **Validation** - Easy to add validation attributes
4. **Documentation** - Self-documenting API
5. **Versioning** - Easy to version DTOs
6. **Security** - Don't expose entities directly
7. **Flexibility** - DTOs can differ from entities

---

**Aurora FlowStudio** - Production-ready DTOs for world-class APIs! 🎯
