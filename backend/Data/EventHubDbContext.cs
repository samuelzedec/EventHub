using backend.Data.Mappings;
using backend.Models;
using Microsoft.EntityFrameworkCore;
namespace backend.Data;
public class EventHubDbContext : DbContext
{
    /* =======================================================
     * Inserindo a connectionString pelo próprio Asp.Net Core 
     * ======================================================= */
    public EventHubDbContext(DbContextOptions<EventHubDbContext> options)
        : base(options) {}
    public EventHubDbContext() {}

    public DbSet<AuthToken> AuthTokens { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<VerificationCode> VerificationCodes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthTokenMap());
        modelBuilder.ApplyConfiguration(new EventMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new VerificationCodeMap());

        // Criando uma seeder
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin", CreatedAt = DateTime.Now },
            new Role { Id = 2, Name = "User", CreatedAt = DateTime.Now }
        );
    }
}
