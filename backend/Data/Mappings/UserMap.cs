using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder
            .HasKey(x => x.Id)
            .HasName("PK_User_Id");

        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("INT")
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.Username)
            .HasColumnName("Username")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(x => x.Email)
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .HasIndex(x => x.Email, "IX_User_Email")
            .IsUnique();

        builder
            .Property(x => x.Password)
            .HasColumnName("Password")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(36)
            .IsRequired();


        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder
            .Property(x => x.UpdateAt)
            .HasColumnName("UpdateAt")
            .HasColumnType("DATETIME")
            .IsRequired(false);
    }
}
