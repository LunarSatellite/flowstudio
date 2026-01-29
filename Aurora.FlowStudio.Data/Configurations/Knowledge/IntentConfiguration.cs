using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aurora.FlowStudio.Entity.Knowledge;

namespace Aurora.FlowStudio.Data.Configurations.Knowledge
{
    public class IntentConfiguration : IEntityTypeConfiguration<Intent>
    {
        public void Configure(EntityTypeBuilder<Intent> builder)
        {
            // Table name
            builder.ToTable("Intents");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Indexes on essential BaseEntity columns are already configured globally
            // Add entity-specific indexes here
            
            // Example: Uncomment and customize based on your entity properties
            // builder.HasIndex(e => e.SomeProperty)
            //     .HasDatabaseName("IX_Intents_SomeProperty");
            
            // Configure relationships
            // Example:
            // builder.HasOne<RelatedEntity>()
            //     .WithMany()
            //     .HasForeignKey(e => e.RelatedEntityId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
