using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.FlowStudio.Data.Configurations.Flow
{
    public class FlowConfiguration : IEntityTypeConfiguration<Entity.Entity.Flow.Flow>
    {
        public void Configure(EntityTypeBuilder<Entity.Entity.Flow.Flow> builder)
        {
            // Additional indexes
            builder.HasIndex(f => f.Slug).IsUnique();
            builder.HasIndex(f => new { f.TenantId, f.Status, f.Type });
            builder.HasIndex(f => f.PublishedAt);

            // Relationships
            builder.HasMany(f => f.Nodes)
                .WithOne()
                .HasForeignKey(n => n.FlowId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(f => f.Variables)
                .WithOne()
                .HasForeignKey(v => v.FlowId)
                .OnDelete(DeleteBehavior.Cascade);

            // JSON conversions for complex properties
            builder.Property(f => f.Tags)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new List<string>()
                );

            builder.Property(f => f.SubCategories)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new List<string>()
                );

            builder.Property(f => f.Keywords)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new List<string>()
                );

            builder.Property(f => f.Metadata)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new Dictionary<string, object>()
                );
        }
    }
}
