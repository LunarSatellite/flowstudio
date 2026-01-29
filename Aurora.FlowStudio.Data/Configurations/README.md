# Fluent API Configurations

## Complete Entity Configurations for Aurora FlowStudio

All 36 entity configurations organized by domain.

---

## 📂 **Structure**

```
Configurations/
├── Tenant/              (10 configurations)
│   ├── TenantConfiguration.cs
│   ├── TenantSubscriptionConfiguration.cs
│   ├── UsageConfiguration.cs
│   ├── UsageBreakdownConfiguration.cs
│   ├── InvoiceConfiguration.cs
│   ├── PricingRuleConfiguration.cs
│   ├── APIKeyConfiguration.cs
│   ├── CostAlertConfiguration.cs
│   ├── PricingHistoryConfiguration.cs
│   └── UsageQuotaConfiguration.cs
│
├── Conversation/        (5 configurations)
│   ├── ConversationConfiguration.cs
│   ├── MessageConfiguration.cs
│   ├── CustomerConfiguration.cs
│   ├── ChannelConfiguration.cs
│   └── ChannelMessageConfiguration.cs
│
├── Flow/                (6 configurations)
│   ├── FlowConfiguration.cs
│   ├── FlowNodeConfiguration.cs
│   ├── FlowConnectionConfiguration.cs
│   ├── FlowVariableConfiguration.cs
│   ├── FlowExecutionConfiguration.cs
│   └── FlowVersionConfiguration.cs
│
├── AI/                  (3 configurations)
│   ├── AIProviderConfiguration.cs
│   ├── VoiceProviderConfiguration.cs
│   └── WebRTCConfigConfiguration.cs
│
├── Integration/         (3 configurations)
│   ├── ConnectorConfiguration.cs
│   ├── ConnectorEndpointConfiguration.cs
│   └── ConnectorLogConfiguration.cs
│
├── Knowledge/           (3 configurations)
│   ├── MessageTemplateConfiguration.cs
│   ├── KnowledgeBaseConfiguration.cs
│   └── IntentConfiguration.cs
│
└── Identity/            (5 configurations)
    ├── ApplicationUserConfiguration.cs
    ├── ApplicationRoleConfiguration.cs
    ├── ApplicationUserRoleConfiguration.cs
    ├── RefreshTokenConfiguration.cs
    └── AuditLogConfiguration.cs
```

---

## 🚀 **Usage**

### **1. Copy to Your Project**
```bash
# Copy all configurations to your Data project
cp -r Configurations/* YourProject.Data/Configurations/
```

### **2. DbContext Will Auto-Load**
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // This automatically loads ALL configurations from the assembly
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlowStudioDbContext).Assembly);
}
```

---

## ✏️ **Customizing Configurations**

Each configuration file is a template. Customize based on your entity properties:

### **Example: Add Property Configuration**
```csharp
public void Configure(EntityTypeBuilder<Conversation> builder)
{
    builder.ToTable("Conversations");
    builder.HasKey(e => e.Id);

    // Add your property configurations
    builder.Property(c => c.SessionId)
        .IsRequired()
        .HasMaxLength(255);

    builder.Property(c => c.Status)
        .IsRequired()
        .HasConversion<string>();

    builder.Property(c => c.CurrentState)
        .HasColumnType("jsonb");

    // Add your custom indexes
    builder.HasIndex(c => c.SessionId)
        .IsUnique()
        .HasDatabaseName("IX_Conversations_SessionId");

    builder.HasIndex(c => c.Status)
        .HasDatabaseName("IX_Conversations_Status");
}
```

### **Example: Add Relationship**
```csharp
// One-to-Many
builder.HasOne<Customer>()
    .WithMany()
    .HasForeignKey(c => c.CustomerId)
    .OnDelete(DeleteBehavior.Restrict);

// Many-to-Many (with join table)
builder.HasMany<Flow>()
    .WithMany()
    .UsingEntity(j => j.ToTable("ConversationFlows"));
```

### **Example: Add Composite Index**
```csharp
builder.HasIndex(e => new { e.TenantId, e.Status, e.CreatedAt })
    .HasDatabaseName("IX_Conversations_Tenant_Status_Created");
```

---

## ⚠️ **Important Notes**

### **BaseEntity Indexes Already Configured**
The DbContext automatically configures these indexes for ALL entities:
- ✅ `IX_{TableName}_CreatedAt`
- ✅ `IX_{TableName}_CreatedByUserId`
- ✅ `IX_{TableName}_UpdatedAt`
- ✅ `IX_{TableName}_IsDeleted`
- ✅ `IX_{TableName}_IsActive`
- ✅ `IX_{TableName}_Metadata`

### **TenantBaseEntity Indexes Already Configured**
Additional composite indexes for tenant entities:
- ✅ `IX_{TableName}_Tenant_Delete_Created` (TenantId, IsDeleted, CreatedAt)
- ✅ `IX_{TableName}_Tenant_Active` (TenantId, IsActive)

**You don't need to configure these in your entity configurations!**

---

## 📝 **Configuration Priorities**

1. **Table Name** - Define the database table name
2. **Primary Key** - Configure the primary key (already done by BaseEntity)
3. **Properties** - Configure column types, max lengths, required fields
4. **Relationships** - Define foreign keys and navigation properties
5. **Indexes** - Add entity-specific indexes
6. **Constraints** - Add unique constraints, check constraints

---

## 🎯 **Common Patterns**

### **Enum Properties**
```csharp
builder.Property(e => e.Status)
    .IsRequired()
    .HasConversion<string>();  // Store as string in database
```

### **JSON Properties (PostgreSQL)**
```csharp
builder.Property(e => e.Metadata)
    .HasColumnType("jsonb")
    .HasConversion(
        v => JsonSerializer.Serialize(v, null),
        v => JsonSerializer.Deserialize<YourType>(v, null)
    );
```

### **Decimal Properties**
```csharp
builder.Property(e => e.TotalCost)
    .HasColumnType("decimal(18,2)")
    .HasPrecision(18, 2);
```

### **Unique Indexes**
```csharp
builder.HasIndex(e => e.Email)
    .IsUnique()
    .HasDatabaseName("IX_Users_Email");
```

### **Filtered Indexes**
```csharp
builder.HasIndex(e => e.Status)
    .HasDatabaseName("IX_Orders_Active_Status")
    .HasFilter("[IsDeleted] = 0");
```

---

## ✅ **What's Included**

- ✅ 36 configuration files (35 entities + 1 join table)
- ✅ Organized by domain
- ✅ Template structure ready to customize
- ✅ Proper namespaces
- ✅ Table names configured
- ✅ Primary keys configured
- ✅ Comments for guidance

---

## 🔧 **Next Steps**

1. Review each configuration file
2. Add property configurations based on your entity properties
3. Add relationships (foreign keys)
4. Add entity-specific indexes
5. Test with migrations: `dotnet ef migrations add InitialCreate`

---

**All configurations are production-ready templates!** 🎉

Just customize based on your specific entity properties and relationships.
