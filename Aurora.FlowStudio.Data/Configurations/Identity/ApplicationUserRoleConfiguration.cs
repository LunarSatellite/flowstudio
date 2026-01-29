using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aurora.FlowStudio.Entity.Identity;

namespace Aurora.FlowStudio.Data.Configurations.Identity
{
    public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            // Table name
            builder.ToTable("UserRoles", "identity");

            // ========================================
            // Composite Primary Key (UserId + RoleId)
            // ========================================
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            // ========================================
            // Property Configurations
            // ========================================

            builder.Property(ur => ur.AssignedAt)
                .IsRequired();

            builder.Property(ur => ur.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // ========================================
            // Relationships
            // ========================================

            // Relationship to ApplicationUser
            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to ApplicationRole
            builder.HasOne<ApplicationRole>()
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship to User who assigned the role
            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(ur => ur.AssignedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // ========================================
            // Indexes
            // ========================================

            // Index on UserId for quick lookup of user's roles
            builder.HasIndex(ur => ur.UserId)
                .HasDatabaseName("IX_UserRoles_UserId");

            // Index on RoleId for quick lookup of role's users
            builder.HasIndex(ur => ur.RoleId)
                .HasDatabaseName("IX_UserRoles_RoleId");

            // Index on IsActive for filtering active role assignments
            builder.HasIndex(ur => ur.IsActive)
                .HasDatabaseName("IX_UserRoles_IsActive");

            // Index on AssignedAt for temporal queries
            builder.HasIndex(ur => ur.AssignedAt)
                .HasDatabaseName("IX_UserRoles_AssignedAt");

            // Composite index for active roles per user
            builder.HasIndex(ur => new { ur.UserId, ur.IsActive })
                .HasDatabaseName("IX_UserRoles_User_Active");

            // Index on ExpiresAt for finding expiring roles
            builder.HasIndex(ur => ur.ExpiresAt)
                .HasDatabaseName("IX_UserRoles_ExpiresAt")
                .HasFilter("[ExpiresAt] IS NOT NULL");
        }
    }
}