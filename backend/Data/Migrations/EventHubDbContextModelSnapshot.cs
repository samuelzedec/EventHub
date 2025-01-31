﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Data;

#nullable disable

namespace backend.Data.Migrations
{
    [DbContext(typeof(EventHubDbContext))]
    partial class EventHubDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventUser", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("INT");

                    b.Property<int>("UserId")
                        .HasColumnType("INT");

                    b.HasKey("EventId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("EventUser");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("INT");

                    b.Property<int>("UserId")
                        .HasColumnType("INT");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("backend.Models.AuthToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("AccessToken");

                    b.Property<DateTime>("AccessTokenExpiry")
                        .HasColumnType("DATETIME")
                        .HasColumnName("AccessTokenExpiry");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("RefreshToken");

                    b.Property<DateTime>("RefreshTokenExpiry")
                        .HasColumnType("DATETIME")
                        .HasColumnName("RefreshTokenExpiry");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("UpdatedAt");

                    b.Property<int>("UserId")
                        .HasColumnType("INT");

                    b.HasKey("Id")
                        .HasName("PK_AuthToken_Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasIndex(new[] { "AccessToken" }, "IX_AuthToken_Token");

                    b.ToTable("AuthToken", (string)null);
                });

            modelBuilder.Entity("backend.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("CreatedAt");

                    b.Property<int>("CreatorId")
                        .HasColumnType("INT");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DateAndTime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)")
                        .HasColumnName("Description");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Location");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("INT")
                        .HasColumnName("MaxCapacity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("UpdatedAt");

                    b.HasKey("Id")
                        .HasName("PK_Event_Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex(new[] { "Name" }, "UQ_Event_Name")
                        .IsUnique();

                    b.ToTable("Event", (string)null);
                });

            modelBuilder.Entity("backend.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id")
                        .HasName("PK_Role_Id");

                    b.HasIndex(new[] { "Name" }, "UQ_Role_Name")
                        .IsUnique();

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 1, 30, 22, 45, 56, 216, DateTimeKind.Utc).AddTicks(6246),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 1, 30, 22, 45, 56, 216, DateTimeKind.Utc).AddTicks(6249),
                            Name = "User"
                        });
                });

            modelBuilder.Entity("backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Password");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("DATETIME2")
                        .HasColumnName("UpdateAt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Username");

                    b.HasKey("Id")
                        .HasName("PK_User_Id");

                    b.HasIndex(new[] { "Email" }, "IX_User_Email")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("EventUser", b =>
                {
                    b.HasOne("backend.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_EventUser_EventId");

                    b.HasOne("backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_EventUser_UserId");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("backend.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_RoleUser_RoleId");

                    b.HasOne("backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_RoleUser_UserId");
                });

            modelBuilder.Entity("backend.Models.AuthToken", b =>
                {
                    b.HasOne("backend.Models.User", "User")
                        .WithOne("AuthToken")
                        .HasForeignKey("backend.Models.AuthToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_AuthToken_UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Models.Event", b =>
                {
                    b.HasOne("backend.Models.User", "Creator")
                        .WithMany("MyEvents")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Event_Creator");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("backend.Models.User", b =>
                {
                    b.Navigation("AuthToken")
                        .IsRequired();

                    b.Navigation("MyEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
