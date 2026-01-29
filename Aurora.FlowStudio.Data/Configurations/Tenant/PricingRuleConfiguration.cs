using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aurora.FlowStudio.Entity.Tenant;

namespace Aurora.FlowStudio.Data.Configurations.Tenant
{
    public class PricingRuleConfiguration : IEntityTypeConfiguration<PricingRule>
    {
        public void Configure(EntityTypeBuilder<PricingRule> builder)
        {
            // Table name
            builder.ToTable("PricingRules");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Indexes on essential BaseEntity columns are already configured globally
            // Add entity-specific indexes here
            
            // Example: Uncomment and customize based on your entity properties
            // builder.HasIndex(e => e.SomeProperty)
            //     .HasDatabaseName("IX_PricingRules_SomeProperty");
            
            // Configure relationships
            // Example:
            // builder.HasOne<RelatedEntity>()
            //     .WithMany()
            //     .HasForeignKey(e => e.RelatedEntityId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
