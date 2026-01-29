using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aurora.FlowStudio.Entity.Conversation;

namespace Aurora.FlowStudio.Data.Configurations.Conversation
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Entity.Conversation.Conversation>
    {
        public void Configure(EntityTypeBuilder<Entity.Conversation.Conversation> builder)
        {
            // Table name
            builder.ToTable("Conversations");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Indexes on essential BaseEntity columns are already configured globally
            // Add entity-specific indexes here
            
            // Example: Uncomment and customize based on your entity properties
            // builder.HasIndex(e => e.SomeProperty)
            //     .HasDatabaseName("IX_Conversations_SomeProperty");
            
            // Configure relationships
            // Example:
            // builder.HasOne<RelatedEntity>()
            //     .WithMany()
            //     .HasForeignKey(e => e.RelatedEntityId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
