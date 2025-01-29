using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Data.Mappings;
public class AuthTokenMap : IEntityTypeConfiguration<AuthToken>
{
    public void Configure(EntityTypeBuilder<AuthToken> builder)
    {
        builder.ToTable("AuthToken");

        builder
            .HasKey(x => x.Id)
            .HasName("PK_AuthToken_Id");

        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("INT")
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.AccessToken)
            .HasColumnName("AccessToken")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(800)
            .IsRequired();

        builder
            .HasIndex(x => x.AccessToken, "IX_AuthToken_Token");

        builder
            .Property(x => x.AccessTokenExpiry)
            .HasColumnName("AccessTokenExpiry")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder
            .Property(x => x.RefreshToken)
            .HasColumnName("RefreshToken")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(800)
            .IsRequired();

        builder
            .Property(x => x.RefreshTokenExpiry)
            .HasColumnName("RefreshTokenExpiry")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME2")
            .IsRequired();

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("DATETIME2")
            .IsRequired(false);

        builder
            .HasOne(x => x.User)
            .WithOne(x => x.AuthToken)
            .HasForeignKey<AuthToken>("UserId")
            .HasConstraintName("FK_AuthToken_UserId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

    }
}