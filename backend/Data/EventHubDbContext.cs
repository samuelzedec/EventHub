using Microsoft.EntityFrameworkCore;
namespace backend.Mappings;
public class EventHubDbContext : DbContext
{
    /* =======================================================
     * Inserindo a connectionString pelo próprio Asp.Net Core 
     * ======================================================= */
    public EventHubDbContext(DbContextOptions<EventHubDbContext> options)
        : base(options) {} 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
