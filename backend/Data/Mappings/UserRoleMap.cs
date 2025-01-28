using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Data.Mappings;
public class UserRoleMap : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRole");

        builder
            .HasKey(x => new { x.UserId, x.RoleId })
            .HasName("PK_UserRole_Id");

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_UserRole_UserId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasIndex(x => x.UserId, "IX_UserRole_UserId");

        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.RoleId)
            .HasConstraintName("FK_UserRole_RoleId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasIndex(x => x.RoleId, "IX_UserRole_RoleId");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME")
            .IsRequired();
    }
}