using backend.Data;
using backend.Extensions;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;
public class EventRepository : IRepository<Event>
{
    private readonly EventHubDbContext _context;
    public EventRepository(EventHubDbContext context)
        => _context = context;

    public async Task<List<Event>> GetAllAsync()
        => await _context
            .Events
            .Include(x => x.Creator)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Event?> GetAsync(int modelId)
        => await _context
            .Events
            .Include(x => x.Creator)
            .Include(x => x.Users)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == modelId);

    public async Task<Event?> GetByNameAsync(string eventName)
        => await _context
            .Events
            .Include(x => x.Creator)
            .Include(x => x.Users)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name.ToLower() == eventName.ToLower());
    public async Task<Event> InsertAsync(Event model)
    {
        await _context.Events.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<Event?> UpdateAsync(int modelId, Event model)
    {
        var @event = await _context
            .Events
            .FirstOrDefaultAsync(x => x.Id == modelId);

        if (@event is null)
            return null;

        @event.CopyFrom(model);
        _context.Update(@event);
        await _context.SaveChangesAsync();
        return @event;
    }

    public async Task<bool> DeleteAsync(int modelId)
    {
        var @event = await _context
            .Events
            .FirstOrDefaultAsync(x => x.Id == modelId);

        if (@event is null)
            return false;
        
        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();
        return true;
    }
}