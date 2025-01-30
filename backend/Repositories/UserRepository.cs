using backend.Data;
using backend.Extensions;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;
public class UserRepository : IRepository<User>
{
    private readonly EventHubDbContext _context;
    public UserRepository(EventHubDbContext context)
        => _context = context;

    public async Task<List<User>> GetAllASync()
        => await _context
            .Users
            .Include(x => x.Roles)
            .AsNoTracking()
            .ToListAsync();

    public async Task<User?> GetAsync(int modelId)
        => await _context
            .Users
            .Include(x => x.AuthToken)
            .Include(x => x.MyEvents)
            .Include(x => x.Roles)
            .Include(x => x.Events)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == modelId);

    public async Task<User> InsertAsync(User model)
    {
        await _context.Users.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<User?> UpdateAsync(int modelId, User model)
    {
        var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.Id == modelId);

        if (user is null) 
            return null;

        user.CopyFrom(model);

        _context.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(int modelId)
    {
        var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.Id == modelId);

        if (user is null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}