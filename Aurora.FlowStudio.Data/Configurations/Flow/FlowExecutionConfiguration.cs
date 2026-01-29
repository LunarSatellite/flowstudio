using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aurora.FlowStudio.Entity.Flow;

namespace Aurora.FlowStudio.Data.Configurations.Flow
{
    public class FlowExecutionConfiguration : IEntityTypeConfiguration<FlowExecution>
    {
        public void Configure(EntityTypeBuilder<FlowExecution> builder)
        {
            // Table name
            builder.ToTable("FlowExecutions");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Indexes on essential BaseEntity columns are already configured globally
            // Add entity-specific indexes here
            
            // Example: Uncomment and customize based on your entity properties
            // builder.HasIndex(e => e.SomeProperty)
            //     .HasDatabaseName("IX_FlowExecutions_SomeProperty");
            
            // Configure relationships
            // Example:
            // builder.HasOne<RelatedEntity>()
            //     .WithMany()
            //     .HasForeignKey(e => e.RelatedEntityId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
