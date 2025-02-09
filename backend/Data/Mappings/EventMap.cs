using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Data.Mappings;
public class EventMap : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Event");

        builder
            .HasKey(x => x.Id)
            .HasName("PK_Event_Id");

        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("INT")
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .HasIndex(x => x.Name, "UQ_Event_Name")
            .IsUnique();

        builder
            .Property(x => x.Description)
            .HasColumnName("Description")
            .HasColumnType("NVARCHAR(MAX)")
            .IsRequired();
        
        builder
            .Property(x => x.Slug)
            .HasColumnName("Slug")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(x => x.DateAndTime)
            .HasColumnName("DateAndTime")
            .HasColumnType("DATETIME2")
            .IsRequired();

        builder
            .Property(x => x.Location)
            .HasColumnName("Location")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(x => x.MaxCapacity)
            .HasColumnName("MaxCapacity")
            .HasColumnType("INT")
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME2")
            .HasDefaultValue(DateTime.Now);

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("DATETIME2")
            .IsRequired(false);

        builder
            .HasOne(x => x.Creator)
            .WithMany(x => x.MyEvents)
            .HasForeignKey("CreatorId")
            .HasConstraintName("FK_Event_Creator")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}