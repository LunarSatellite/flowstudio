using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aurora.FlowStudio.Entity.Tenant;

namespace Aurora.FlowStudio.Data.Configurations.Tenant
{
    public class TenantConfiguration : IEntityTypeConfiguration<Entity.Tenant.Tenant>
    {
        public void Configure(EntityTypeBuilder<Entity.Tenant.Tenant> builder)
        {
            // Table name
            builder.ToTable("Tenants");

            // Primary Key
            builder.HasKey(t => t.Id);

            // ========================================
            // Property Configurations
            // ========================================

            builder.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Domain)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Status)
                .IsRequired()
                .HasConversion<string>();  // Store enum as string

            builder.Property(t => t.Plan)
                .IsRequired()
                .HasConversion<string>();  // Store enum as string

            builder.Property(t => t.ContactEmail)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(t => t.ContactPhone)
                .HasMaxLength(20);

            builder.Property(t => t.Address)
                .HasMaxLength(500);

            builder.Property(t => t.Country)
                .HasMaxLength(100);

            builder.Property(t => t.Timezone)
                .HasMaxLength(50);

            // ========================================
            // Indexes (beyond BaseEntity indexes)
            // ========================================

            // Unique index on Domain (each tenant has unique domain)
            builder.HasIndex(t => t.Domain)
                .IsUnique()
                .HasDatabaseName("IX_Tenants_Domain");

            // Index on CompanyName for search
            builder.HasIndex(t => t.CompanyName)
                .HasDatabaseName("IX_Tenants_CompanyName");

            // Index on Status for filtering active/inactive tenants
            builder.HasIndex(t => t.Status)
                .HasDatabaseName("IX_Tenants_Status");

            // Index on ContactEmail for lookups
            builder.HasIndex(t => t.ContactEmail)
                .HasDatabaseName("IX_Tenants_ContactEmail");

            // Composite index for trial expiration queries
            builder.HasIndex(t => new { t.Status, t.TrialEndsAt })
                .HasDatabaseName("IX_Tenants_Status_TrialEnds");
        }
    }
}