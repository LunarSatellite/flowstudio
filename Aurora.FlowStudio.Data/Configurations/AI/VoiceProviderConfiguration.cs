using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aurora.FlowStudio.Entity.AI;

namespace Aurora.FlowStudio.Data.Configurations.AI
{
    public class VoiceProviderConfiguration : IEntityTypeConfiguration<VoiceProvider>
    {
        public void Configure(EntityTypeBuilder<VoiceProvider> builder)
        {
            // Table name
            builder.ToTable("VoiceProviders");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Indexes on essential BaseEntity columns are already configured globally
            // Add entity-specific indexes here
            
            // Example: Uncomment and customize based on your entity properties
            // builder.HasIndex(e => e.SomeProperty)
            //     .HasDatabaseName("IX_VoiceProviders_SomeProperty");
            
            // Configure relationships
            // Example:
            // builder.HasOne<RelatedEntity>()
            //     .WithMany()
            //     .HasForeignKey(e => e.RelatedEntityId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
