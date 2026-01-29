using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aurora.FlowStudio.Entity.Integration;

namespace Aurora.FlowStudio.Data.Configurations.Integration
{
    public class ConnectorEndpointConfiguration : IEntityTypeConfiguration<ConnectorEndpoint>
    {
        public void Configure(EntityTypeBuilder<ConnectorEndpoint> builder)
        {
            // Table name
            builder.ToTable("ConnectorEndpoints");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Indexes on essential BaseEntity columns are already configured globally
            // Add entity-specific indexes here
            
            // Example: Uncomment and customize based on your entity properties
            // builder.HasIndex(e => e.SomeProperty)
            //     .HasDatabaseName("IX_ConnectorEndpoints_SomeProperty");
            
            // Configure relationships
            // Example:
            // builder.HasOne<RelatedEntity>()
            //     .WithMany()
            //     .HasForeignKey(e => e.RelatedEntityId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
