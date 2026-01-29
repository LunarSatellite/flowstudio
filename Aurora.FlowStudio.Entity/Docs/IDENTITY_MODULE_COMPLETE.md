# 🔐 Identity Module - ASP.NET Core Identity Integration

## Overview

Complete Identity implementation with **9 entities** for authentication, authorization, and audit logging.

---

## 📊 Identity Entities (9)

| Entity | Purpose | Extends |
|--------|---------|---------|
| **ApplicationUser** | User accounts | IdentityUser<Guid> |
| **ApplicationRole** | Roles with permissions | IdentityRole<Guid> |
| **ApplicationUserRole** | User-Role mapping | IdentityUserRole<Guid> |
| **ApplicationUserClaim** | User claims | IdentityUserClaim<Guid> |
| **ApplicationUserLogin** | External logins (Google, etc.) | IdentityUserLogin<Guid> |
| **ApplicationUserToken** | Password reset tokens, etc. | IdentityUserToken<Guid> |
| **ApplicationRoleClaim** | Role claims | IdentityRoleClaim<Guid> |
| **RefreshToken** | JWT refresh tokens | BaseEntity |
| **AuditLog** | User activity tracking | TenantBaseEntity |

**Total Identity Entities: 9**
**Total Project Entities: 37 + 9 = 46**

---

## 🎯 Entity Details

### **1. ApplicationUser** - The User Account

**Purpose**: Core user entity with tenant isolation and profile

**Key Fields**:
```csharp
// Identity (inherited from IdentityUser)
Id, UserName, Email, EmailConfirmed, PasswordHash, PhoneNumber, TwoFactorEnabled

// Tenant & Profile
TenantId                 // Multi-tenancy
FirstName, LastName      // Name
Avatar                   // Profile picture
Timezone, Language       // Preferences

// Status
IsActive, IsDeleted      // Soft delete
LastLoginAt, LastLoginIp // Tracking
LoginCount               // Analytics
FailedLoginAttempts      // Security

// Notifications
EmailNotifications
SmsNotifications
PushNotifications
```

**Business Flow**:
```csharp
// User signs up
var user = new ApplicationUser {
    TenantId = shopEasyTenantId,
    UserName = "john@shopeasy.com",
    Email = "john@shopeasy.com",
    FirstName = "John",
    LastName = "Doe",
    EmailNotifications = true
};

await _userManager.CreateAsync(user, "SecurePassword123!");
await _userManager.AddToRoleAsync(user, "Admin");

// User logs in
var result = await _signInManager.PasswordSignInAsync(
    user.UserName, 
    password, 
    isPersistent: false, 
    lockoutOnFailure: true);

if (result.Succeeded) {
    user.LastLoginAt = DateTime.UtcNow;
    user.LastLoginIp = httpContext.Connection.RemoteIpAddress;
    user.LoginCount++;
    await _userManager.UpdateAsync(user);
}
```

---

### **2. ApplicationRole** - Roles with Permissions

**Purpose**: Tenant-specific roles with custom permissions

**Key Fields**:
```csharp
// Identity (inherited)
Id, Name, NormalizedName

// Tenant
TenantId                 // Null = system role (Admin, SuperAdmin)

// Metadata
Description              // What this role is for
IsSystemRole             // Cannot be modified/deleted
IsActive                 // Can be disabled
Permissions              // List<string> of permission names
UserCount                // How many users have this role
```

**System Roles**:
```csharp
// Platform-wide (TenantId = null)
- SystemAdmin            // Full platform access
- PlatformSupport        // Support access across tenants

// Tenant-specific (TenantId = tenant's ID)
- Admin                  // Full tenant access
- Manager                // Team management
- Agent                  // Customer service
- Viewer                 // Read-only
```

**Custom Permissions**:
```csharp
var role = new ApplicationRole {
    TenantId = shopEasyTenantId,
    Name = "Support Lead",
    Description = "Manages support team and views analytics",
    Permissions = new List<string> {
        "conversations.view",
        "conversations.assign",
        "users.view",
        "users.manage",
        "analytics.view",
        "flows.view"
    },
    IsSystemRole = false
};
```

---

### **3. ApplicationUserRole** - User-Role Mapping

**Purpose**: Links users to roles with expiration and audit

**Key Fields**:
```csharp
UserId, RoleId           // The mapping
AssignedAt               // When assigned
AssignedBy               // Who assigned
IsActive                 // Can be disabled
ExpiresAt                // Optional expiration (contractors, temporary access)
```

**Temporary Access Example**:
```csharp
// Grant contractor temporary admin access
var userRole = new ApplicationUserRole {
    UserId = contractorId,
    RoleId = adminRoleId,
    AssignedBy = currentUserId,
    ExpiresAt = DateTime.UtcNow.AddDays(30),  // 30-day access
    IsActive = true
};

// Check if still valid
if (userRole.ExpiresAt.HasValue && DateTime.UtcNow > userRole.ExpiresAt.Value) {
    // Access expired, revoke
    userRole.IsActive = false;
}
```

---

### **4. RefreshToken** - JWT Authentication

**Purpose**: Secure refresh token storage for JWT authentication

**Key Fields**:
```csharp
UserId, TenantId         // Owner
Token                    // Hashed token
JwtId                    // Links to access token
IssuedAt, ExpiresAt      // Validity period
IsRevoked, IsUsed        // Security flags
CreatedByIp              // Security audit
DeviceId                 // Device tracking
ReplacedByTokenId        // Token rotation
```

**JWT Flow**:
```csharp
// 1. User logs in
var accessToken = GenerateAccessToken(user);  // 15 minutes
var refreshToken = GenerateRefreshToken(user); // 7 days

// Save refresh token
await _db.RefreshTokens.AddAsync(new RefreshToken {
    UserId = user.Id,
    TenantId = user.TenantId,
    Token = HashToken(refreshToken),
    JwtId = accessToken.JwtId,
    CreatedByIp = httpContext.Connection.RemoteIpAddress,
    DeviceId = GetDeviceId(httpContext)
});

// 2. Access token expires (after 15 minutes)
// Client sends refresh token

// 3. Validate refresh token
var storedToken = await _db.RefreshTokens
    .FirstOrDefaultAsync(t => t.Token == hashedToken && t.IsValid);

if (storedToken == null) {
    return Unauthorized("Invalid refresh token");
}

// 4. Mark old token as used
storedToken.IsUsed = true;
storedToken.UsedAt = DateTime.UtcNow;

// 5. Generate new tokens (token rotation)
var newAccessToken = GenerateAccessToken(user);
var newRefreshToken = GenerateRefreshToken(user);

await _db.RefreshTokens.AddAsync(new RefreshToken {
    UserId = user.Id,
    Token = HashToken(newRefreshToken),
    JwtId = newAccessToken.JwtId,
    ReplacedByTokenId = storedToken.Id  // Link to old token
});
```

**Security Features**:
- ✅ Token rotation (old token invalidated)
- ✅ One-time use (IsUsed flag)
- ✅ IP tracking (detect stolen tokens)
- ✅ Device tracking
- ✅ Manual revocation (logout all devices)

---

### **5. AuditLog** - User Activity Tracking

**Purpose**: Complete audit trail for compliance and security

**Key Fields**:
```csharp
UserId, UserEmail        // Who
Action, EntityType       // What
EntityId                 // Which entity
OldValues, NewValues     // Changes (JSON)
IpAddress, UserAgent     // Where/How
RequestPath, HttpMethod  // API details
IsSuccess, ErrorMessage  // Result
CorrelationId            // Distributed tracing
```

**Logged Actions**:
```csharp
// Authentication
- Login, Logout, LoginFailed
- PasswordChanged, PasswordReset
- TwoFactorEnabled, TwoFactorDisabled

// CRUD Operations
- Created, Updated, Deleted
- Viewed, Exported

// Security
- PermissionGranted, PermissionRevoked
- RoleAssigned, RoleRemoved
- AccountLocked, AccountUnlocked

// Business
- FlowPublished, FlowExecuted
- ConversationStarted, MessageSent
- InvoiceGenerated, PaymentReceived
```

**Example Audit Logs**:
```csharp
// User logs in
new AuditLog {
    TenantId = user.TenantId,
    UserId = user.Id,
    UserEmail = user.Email,
    Action = "Login",
    EntityType = "User",
    EntityId = user.Id,
    Description = "User logged in successfully",
    IpAddress = "192.168.1.100",
    UserAgent = "Mozilla/5.0...",
    IsSuccess = true
};

// User updates flow
new AuditLog {
    UserId = user.Id,
    Action = "Updated",
    EntityType = "Flow",
    EntityId = flowId,
    Description = "Updated Product Support flow",
    OldValues = { 
        "Name" = "Product Support",
        "Status" = "Draft"
    },
    NewValues = { 
        "Name" = "Product Support v2",
        "Status" = "Published"
    },
    IsSuccess = true
};

// Failed delete attempt
new AuditLog {
    UserId = user.Id,
    Action = "Delete",
    EntityType = "Conversation",
    EntityId = conversationId,
    Description = "Attempted to delete conversation",
    IsSuccess = false,
    ErrorMessage = "Access denied: User does not have permission"
};
```

**Compliance Queries**:
```csharp
// GDPR: Show all data access for a user
var accessLog = await _db.AuditLogs
    .Where(a => a.EntityType == "Customer" && 
                a.EntityId == customerId &&
                a.Action == "Viewed")
    .OrderByDescending(a => a.CreatedAt)
    .ToListAsync();

// Security: Failed login attempts
var failedLogins = await _db.AuditLogs
    .Where(a => a.Action == "LoginFailed" &&
                a.IpAddress == suspiciousIp &&
                a.CreatedAt > DateTime.UtcNow.AddHours(-1))
    .CountAsync();

if (failedLogins > 5) {
    // Block IP, trigger alert
}

// Analytics: Most active users
var topUsers = await _db.AuditLogs
    .Where(a => a.CreatedAt > DateTime.UtcNow.AddDays(-30))
    .GroupBy(a => a.UserId)
    .Select(g => new {
        UserId = g.Key,
        ActionCount = g.Count()
    })
    .OrderByDescending(x => x.ActionCount)
    .Take(10)
    .ToListAsync();
```

---

## 🔄 DbContext Configuration

### **ApplicationDbContext with Identity**

```csharp
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Aurora.FlowStudio.Entity.Identity;

public class ApplicationDbContext : IdentityDbContext<
    ApplicationUser,           // User
    ApplicationRole,           // Role
    Guid,                      // Key type (Guid instead of string)
    ApplicationUserClaim,      // User claims
    ApplicationUserRole,       // User roles
    ApplicationUserLogin,      // External logins
    ApplicationRoleClaim,      // Role claims
    ApplicationUserToken>      // User tokens
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Additional entities
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    // All business entities (37)
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    // ... etc

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Rename Identity tables (optional, cleaner)
        modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
        modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles");
        modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims");
        modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogins");
        modelBuilder.Entity<ApplicationUserToken>().ToTable("UserTokens");
        modelBuilder.Entity<ApplicationRoleClaim>().ToTable("RoleClaims");

        // Indexes
        modelBuilder.Entity<ApplicationUser>()
            .HasIndex(u => u.TenantId);

        modelBuilder.Entity<ApplicationUser>()
            .HasIndex(u => new { u.TenantId, u.Email });

        modelBuilder.Entity<RefreshToken>()
            .HasIndex(t => t.Token);

        modelBuilder.Entity<RefreshToken>()
            .HasIndex(t => new { t.UserId, t.IsRevoked, t.IsUsed });

        modelBuilder.Entity<AuditLog>()
            .HasIndex(a => new { a.TenantId, a.CreatedAt });

        modelBuilder.Entity<AuditLog>()
            .HasIndex(a => new { a.UserId, a.CreatedAt });
    }
}
```

---

## ⚙️ Startup Configuration

```csharp
// Program.cs or Startup.cs
services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// JWT Authentication
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = "aurora-flowstudio",
        ValidateAudience = true,
        ValidAudience = "aurora-flowstudio-clients",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
```

---

## 🎯 Complete Entity Summary

### **Business Entities (37)**
- Tenant & Billing: 10
- Conversation: 5
- Flow: 6
- AI & Voice: 3
- Integration: 3
- Knowledge: 3
- Access Control: 3 (Deprecated - use Identity)
- Base: 2

### **Identity Entities (9)**
- ApplicationUser
- ApplicationRole
- ApplicationUserRole
- ApplicationUserClaim
- ApplicationUserLogin
- ApplicationUserToken
- ApplicationRoleClaim
- RefreshToken
- AuditLog

### **Total: 46 Entities**

---

## ✅ Benefits

| Feature | Benefit |
|---------|---------|
| **Multi-tenancy** | Users belong to specific tenants |
| **JWT Tokens** | Stateless, scalable authentication |
| **Refresh Tokens** | Secure long-lived sessions |
| **Audit Logging** | Complete compliance trail |
| **Soft Delete** | Users can be restored |
| **External Logins** | Google, Microsoft, etc. |
| **Permission System** | Granular access control |
| **Token Rotation** | Enhanced security |

---

**Complete Identity module with enterprise-grade security!** 🔐
