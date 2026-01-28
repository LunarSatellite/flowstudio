using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Aurora.FlowStudio.Entity.Entity.Core;
using Aurora.FlowStudio.Entity.Entity.Identity;
using Aurora.FlowStudio.Entity.Entity.Flow;
using Aurora.FlowStudio.Entity.Entity.Conversation;
using Aurora.FlowStudio.Entity.Entity.Integration;
using Aurora.FlowStudio.Entity.Entity.AI;
using Aurora.FlowStudio.Entity.Entity.Commerce;
using Aurora.FlowStudio.Entity.Entity.DataManagement;
using Aurora.FlowStudio.Entity.Entity.NLU;
using Aurora.FlowStudio.Entity.Entity.AITraining;
using Aurora.FlowStudio.Entity.Entity.Messaging;

namespace Aurora.FlowStudio.Data.Context
{
    /// <summary>
    /// Main DbContext for Aurora FlowStudio with ASP.NET Core Identity and multi-tenancy support
    /// </summary>
    public class FlowStudioDbContext : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        Guid,
        ApplicationUserClaim,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationRoleClaim,
        ApplicationUserToken>
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

        #region Core DbSets
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<TenantSetting> TenantSettings { get; set; }
        #endregion

        #region Identity DbSets
        // ApplicationUser, ApplicationRole, etc. are already defined by IdentityDbContext
        public DbSet<FIDO2Credential> FIDO2Credentials { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }
        #endregion

        #region Flow DbSets
        public DbSet<Entity.Entity.Flow.Flow> Flows { get; set; }
        public DbSet<FlowNode> FlowNodes { get; set; }
        public DbSet<FlowConnection> FlowConnections { get; set; }
        public DbSet<FlowVariable> FlowVariables { get; set; }
        public DbSet<FlowVersion> FlowVersions { get; set; }
        public DbSet<FlowAnalytics> FlowAnalytics { get; set; }
        public DbSet<FlowIntegration> FlowIntegrations { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<NodeAction> NodeActions { get; set; }
        public DbSet<NodeResponseSource> NodeResponseSources { get; set; }
        #endregion

        #region Conversation DbSets
        public DbSet<Entity.Entity.Conversation.Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAuthentication> CustomerAuthentications { get; set; }
        public DbSet<CustomerActivity> CustomerActivities { get; set; }
        public DbSet<CustomerNote> CustomerNotes { get; set; }
        public DbSet<CustomerTag> CustomerTags { get; set; }
        public DbSet<ConversationTag> ConversationTags { get; set; }
        public DbSet<ConversationNote> ConversationNotes { get; set; }
        public DbSet<ConversationFeedback> ConversationFeedbacks { get; set; }
        public DbSet<MessageAttachment> MessageAttachments { get; set; }
        public DbSet<MessageReaction> MessageReactions { get; set; }
        public DbSet<SentimentAnalysis> SentimentAnalyses { get; set; }
        #endregion

        #region Integration DbSets
        public DbSet<Connector> Connectors { get; set; }
        public DbSet<ConnectorEndpoint> ConnectorEndpoints { get; set; }
        public DbSet<ConnectorLog> ConnectorLogs { get; set; }
        public DbSet<ConnectorMetrics> ConnectorMetrics { get; set; }
        #endregion

        #region AI DbSets
        public DbSet<AIProvider> AIProviders { get; set; }
        public DbSet<AIModel> AIModels { get; set; }
        public DbSet<AIProviderLog> AIProviderLogs { get; set; }
        public DbSet<STTProvider> STTProviders { get; set; }
        public DbSet<TTSProvider> TTSProviders { get; set; }
        #endregion

        #region Commerce DbSets
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderTransaction> OrderTransactions { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        #endregion

        #region DataManagement DbSets
        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<DataQuery> DataQueries { get; set; }
        public DbSet<QueryExecution> QueryExecutions { get; set; }
        public DbSet<DataSourceConnection> DataSourceConnections { get; set; }
        public DbSet<DataSourceLog> DataSourceLogs { get; set; }
        #endregion

        #region NLU DbSets
        public DbSet<Intent> Intents { get; set; }
        public DbSet<Entity.Entity.NLU.Entity> Entities { get; set; }
        public DbSet<NLUModel> NLUModels { get; set; }
        public DbSet<TrainingPhrase> TrainingPhrases { get; set; }
        public DbSet<IntentResponse> IntentResponses { get; set; }
        public DbSet<ModelEvaluation> ModelEvaluations { get; set; }
        public DbSet<IntentDetectionLog> IntentDetectionLogs { get; set; }
        #endregion

        #region AITraining DbSets
        public DbSet<TrainingDataset> TrainingDatasets { get; set; }
        public DbSet<DatasetSample> DatasetSamples { get; set; }
        public DbSet<TrainingJob> TrainingJobs { get; set; }
        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<ModelRegistry> ModelRegistries { get; set; }
        public DbSet<ModelDeployment> ModelDeployments { get; set; }
        #endregion

        #region Messaging DbSets
        public DbSet<MessageTemplate> MessageTemplates { get; set; }
        public DbSet<MessageTemplateVersion> MessageTemplateVersions { get; set; }
        public DbSet<TemplateLibrary> TemplateLibraries { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Identity table names and schema
            ConfigureIdentityTables(modelBuilder);

            // Apply all configurations from assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlowStudioDbContext).Assembly);

            // Global query filters for soft delete
            ApplySoftDeleteFilter(modelBuilder);

            // Global query filters for multi-tenancy
            if (_currentTenantId.HasValue)
            {
                ApplyTenantFilter(modelBuilder, _currentTenantId.Value);
            }
        }

        private void ConfigureIdentityTables(ModelBuilder modelBuilder)
        {
            // Configure Identity tables with proper naming and schema
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("AspNetUsers", "identity");
                b.HasIndex(u => u.TenantId);
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("AspNetRoles", "identity");
                b.HasIndex(r => r.TenantId);
            });

            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                b.ToTable("AspNetUserRoles", "identity");
                b.HasKey(ur => new { ur.UserId, ur.RoleId });
            });

            modelBuilder.Entity<ApplicationUserClaim>(b =>
            {
                b.ToTable("AspNetUserClaims", "identity");
            });

            modelBuilder.Entity<ApplicationUserLogin>(b =>
            {
                b.ToTable("AspNetUserLogins", "identity");
            });

            modelBuilder.Entity<ApplicationUserToken>(b =>
            {
                b.ToTable("AspNetUserTokens", "identity");
            });

            modelBuilder.Entity<ApplicationRoleClaim>(b =>
            {
                b.ToTable("AspNetRoleClaims", "identity");
            });
        }

        private void ApplySoftDeleteFilter(ModelBuilder modelBuilder)
        {
            // Apply soft delete filter to all entities inheriting from BaseEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Entity.Entity.Base.BaseEntity).IsAssignableFrom(entityType.ClrType))
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
            where TEntity : Entity.Entity.Base.BaseEntity
        {
            System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }

        private void ApplyTenantFilter(ModelBuilder modelBuilder, Guid tenantId)
        {
            // Apply tenant filter to all entities inheriting from TenantBaseEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Entity.Entity.Base.TenantBaseEntity).IsAssignableFrom(entityType.ClrType))
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
            where TEntity : Entity.Entity.Base.TenantBaseEntity
        {
            System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = x => x.TenantId == tenantId;
            return filter;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Auto-set audit fields before saving
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Entity.Entity.Base.BaseEntity && 
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (Entity.Entity.Base.BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.UpdateCount++;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
