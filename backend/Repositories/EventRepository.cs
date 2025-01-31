using backend.Data;
using backend.Extensions;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;
public class EventRepository
{
    private readonly EventHubDbContext _context;

    public EventRepository(EventHubDbContext context)
        => _context = context;

    public async Task<List<Event>> GetAllASync()
        => await _context
            .Events
            .Include(x => x.Creator)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Event?> GetByNameAsync(string eventName)
        => await _context
            .Events
            .Include(x => x.Creator)
            .Include(x => x.Users)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name.Equals(eventName, StringComparison.OrdinalIgnoreCase));

    public async Task<Event?> UpdateByNameAsync(string eventName, Event model)
    {
        var @event = await _context
            .Events
            .FirstOrDefaultAsync(x => x.Name.Equals(eventName, StringComparison.OrdinalIgnoreCase));

        if (@event is null)
            return null;

        @event.CopyFrom(model);
        _context.Update(@event);
        await _context.SaveChangesAsync();
        return @event;
    }

    public async Task<bool> DeleteAsync(int modelId)
    {
        throw new NotImplementedException();
    }
}