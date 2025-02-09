using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Data.Mappings;
public class VerificationCodeMap : IEntityTypeConfiguration<VerificationCode>
{
    public void Configure(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.ToTable("VerificationCode");

        builder
            .HasKey(x => x.Id)
            .HasName("PK_VerificationCode_Id");

        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("INT")
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.UserEmail)
            .HasColumnName("UserEmail")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .HasIndex(x => x.UserEmail, "UQ_VerificationCode_UserEmail")
            .IsUnique();

        builder
            .Property(x => x.Code)
            .HasColumnName("Code")
            .HasColumnType("INT")
            .IsRequired();

        builder
            .Property(x => x.Duration)
            .HasColumnName("Duration")
            .HasColumnType("DATETIME2")
            .IsRequired();
    }
}