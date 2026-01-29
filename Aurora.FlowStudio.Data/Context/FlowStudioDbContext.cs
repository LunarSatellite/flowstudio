using Microsoft.EntityFrameworkCore;
using Aurora.FlowStudio.Entity.Base;
using Aurora.FlowStudio.Entity.Tenant;
using Aurora.FlowStudio.Entity.Conversation;
using Aurora.FlowStudio.Entity.Flow;
using Aurora.FlowStudio.Entity.AI;
using Aurora.FlowStudio.Entity.Integration;
using Aurora.FlowStudio.Entity.Knowledge;
using Aurora.FlowStudio.Entity.Identity;
using System.Text.Json;

namespace Aurora.FlowStudio.Data.Context
{
    /// <summary>
    /// Main DbContext for Aurora FlowStudio
    /// ✨ Optimized with JSON Metadata (12 columns + 1 JSON column)
    /// </summary>
    public class FlowStudioDbContext : DbContext
    {
        private readonly Guid? _currentTenantId;

        public FlowStudioDbContext(DbContextOptions<FlowStudioDbContext> options) : base(options)
        {
        }

        public FlowStudioDbContext(DbContextOptions<FlowStudioDbContext> options, Guid? currentTenantId)
            : base(options)
        {
            _currentTenantId = currentTenantId;
        }

        #region Tenant DbSets (10 entities)
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantSubscription> TenantSubscriptions { get; set; }
        public DbSet<Usage> Usages { get; set; }
        public DbSet<UsageBreakdown> UsageBreakdowns { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<PricingRule> PricingRules { get; set; }
        public DbSet<APIKey> APIKeys { get; set; }
        public DbSet<CostAlert> CostAlerts { get; set; }
        public DbSet<PricingHistory> PricingHistories { get; set; }
        public DbSet<UsageQuota> UsageQuotas { get; set; }
        #endregion

        #region Conversation DbSets (5 entities)
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<ChannelMessage> ChannelMessages { get; set; }
        #endregion

        #region Flow DbSets (6 entities)
        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowNode> FlowNodes { get; set; }
        public DbSet<FlowConnection> FlowConnections { get; set; }
        public DbSet<FlowVariable> FlowVariables { get; set; }
        public DbSet<FlowExecution> FlowExecutions { get; set; }
        public DbSet<FlowVersion> FlowVersions { get; set; }
        #endregion

        #region AI DbSets (3 entities)
        public DbSet<AIProvider> AIProviders { get; set; }
        public DbSet<VoiceProvider> VoiceProviders { get; set; }
        public DbSet<WebRTCConfig> WebRTCConfigs { get; set; }
        #endregion

        #region Integration DbSets (3 entities)
        public DbSet<Connector> Connectors { get; set; }
        public DbSet<ConnectorEndpoint> ConnectorEndpoints { get; set; }
        public DbSet<ConnectorLog> ConnectorLogs { get; set; }
        #endregion

        #region Knowledge DbSets (3 entities)
        public DbSet<MessageTemplate> MessageTemplates { get; set; }
        public DbSet<KnowledgeBase> KnowledgeBases { get; set; }
        public DbSet<Intent> Intents { get; set; }
        #endregion

        #region Identity DbSets (6 files covering 9 entities)
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        // ApplicationUserClaim, ApplicationUserLogin, ApplicationUserToken, ApplicationRoleClaim
        // are handled by ApplicationIdentityEntities.cs
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✨ STEP 1: Configure JSON metadata for all BaseEntity types
            ConfigureJsonMetadata(modelBuilder);

            // ✨ STEP 2: Configure indexes on essential columns
            ConfigureEssentialIndexes(modelBuilder);

            // STEP 3: Apply global query filters
            ApplySoftDeleteFilter(modelBuilder);

            if (_currentTenantId.HasValue)
            {
                ApplyTenantFilter(modelBuilder, _currentTenantId.Value);
            }

            // STEP 4: Apply your entity configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlowStudioDbContext).Assembly);
        }

        #region JSON Metadata Configuration

        private void ConfigureJsonMetadata(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<AuditMetadata>("Metadata")
                        .HasColumnType("jsonb")
                        .HasConversion(
                            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                            v => JsonSerializer.Deserialize<AuditMetadata>(v, (JsonSerializerOptions?)null) ?? new AuditMetadata()
                        )
                        .IsRequired();

                    modelBuilder.Entity(entityType.ClrType)
                        .HasIndex("Metadata")
                        .HasDatabaseName($"IX_{entityType.GetTableName()}_Metadata");
                }
            }
        }

        #endregion

        #region Essential Column Indexes

        private void ConfigureEssentialIndexes(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var tableName = entityType.GetTableName() ?? entityType.ClrType.Name;

                    modelBuilder.Entity(entityType.ClrType)
                        .HasIndex("CreatedAt")
                        .HasDatabaseName($"IX_{tableName}_CreatedAt");

                    modelBuilder.Entity(entityType.ClrType)
                        .HasIndex("CreatedByUserId")
                        .HasDatabaseName($"IX_{tableName}_CreatedByUserId");

                    modelBuilder.Entity(entityType.ClrType)
                        .HasIndex("IsDeleted")
                        .HasDatabaseName($"IX_{tableName}_IsDeleted");

                    modelBuilder.Entity(entityType.ClrType)
                        .HasIndex("IsActive")
                        .HasDatabaseName($"IX_{tableName}_IsActive");

                    modelBuilder.Entity(entityType.ClrType)
                        .HasIndex("UpdatedAt")
                        .HasDatabaseName($"IX_{tableName}_UpdatedAt");
                }

                if (typeof(TenantBaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var tableName = entityType.GetTableName() ?? entityType.ClrType.Name;

                    modelBuilder.Entity(entityType.ClrType)
                        .HasIndex("TenantId", "IsDeleted", "CreatedAt")
                        .HasDatabaseName($"IX_{tableName}_Tenant_Delete_Created");

                    modelBuilder.Entity(entityType.ClrType)
                        .HasIndex("TenantId", "IsActive")
                        .HasDatabaseName($"IX_{tableName}_Tenant_Active");
                }
            }
        }

        #endregion

        #region Global Query Filters

        private void ApplySoftDeleteFilter(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(FlowStudioDbContext)
                        .GetMethod(nameof(GetSoftDeleteFilter),
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?
                        .MakeGenericMethod(entityType.ClrType);

                    var filter = method?.Invoke(null, Array.Empty<object>());
                    if (filter != null)
                    {
                        entityType.SetQueryFilter((System.Linq.Expressions.LambdaExpression)filter);
                    }
                }
            }
        }

        private static System.Linq.Expressions.LambdaExpression GetSoftDeleteFilter<TEntity>()
            where TEntity : BaseEntity
        {
            System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }

        private void ApplyTenantFilter(ModelBuilder modelBuilder, Guid tenantId)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(TenantBaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(FlowStudioDbContext)
                        .GetMethod(nameof(GetTenantFilter),
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?
                        .MakeGenericMethod(entityType.ClrType);

                    var filter = method?.Invoke(null, new object[] { tenantId });
                    if (filter != null)
                    {
                        entityType.SetQueryFilter((System.Linq.Expressions.LambdaExpression)filter);
                    }
                }
            }
        }

        private static System.Linq.Expressions.LambdaExpression GetTenantFilter<TEntity>(Guid tenantId)
            where TEntity : TenantBaseEntity
        {
            System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = x => x.TenantId == tenantId;
            return filter;
        }

        #endregion

        #region SaveChanges Override

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;

                if (entity.Metadata == null)
                {
                    entity.Metadata = new AuditMetadata();
                }

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.IsDeleted = false;
                    entity.IsActive = true;
                    entity.Version = 1;
                    entity.ConcurrencyStamp = Guid.NewGuid();

                    entity.Metadata.UpdateCount = 0;
                    entity.Metadata.ViewCount = 0;
                    entity.Metadata.AccessCount = 0;
                    entity.Metadata.CommentCount = 0;
                    entity.Metadata.AttachmentCount = 0;
                    entity.Metadata.IsLatestVersion = true;
                    entity.Metadata.IsArchived = false;
                    entity.Metadata.IsVerified = false;
                    entity.Metadata.RequiresAudit = true;
                    entity.Metadata.DisplayOrder = 0;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.ConcurrencyStamp = Guid.NewGuid();

                    entity.Metadata.UpdateCount++;
                    entity.Metadata.LastAccessedAt = DateTime.UtcNow;
                    entity.Metadata.LastValidatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
