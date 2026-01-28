using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aurora.FlowStudio.Entity.Entity.Core;

namespace Aurora.FlowStudio.Data.Configurations.Core
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            // Indexes beyond attributes
            builder.HasIndex(t => t.Slug).IsUnique();
            builder.HasIndex(t => new { t.Status, t.Plan });
            builder.HasIndex(t => t.BillingEmail);

            // Relationships
            builder.HasMany(t => t.Users)
                .WithOne()
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Settings)
                .WithOne()
                .HasForeignKey(s => s.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Value conversions for Dictionary properties
            builder.Property(t => t.BrandingTheme)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new Dictionary<string, string>()
                );

            builder.Property(t => t.Metadata)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new Dictionary<string, object>()
                );

            builder.Property(t => t.Configuration)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new Dictionary<string, object>()
                );

            // List conversions
            builder.Property(t => t.AlternateDomains)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new List<string>()
                );

            builder.Property(t => t.EnabledFeatures)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new List<string>()
                );
        }
    }
}
