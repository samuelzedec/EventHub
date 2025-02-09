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

    public async Task<List<User>> GetAllAsync()
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

    public async Task<Role> GetRoleByNameAsync(string name)
        => await _context
            .Roles
            .FirstOrDefaultAsync(x => x.Name == name)
            ?? new Role();
    
    public async Task<User?> GetByEmailAsync(string email)
        => await _context
            .Users
            .Include(x => x.AuthToken)
            .Include(x => x.MyEvents)
            .Include(x => x.Roles)
            .Include(x => x.Events)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());


    public async Task<User> InsertAsync(User model)
    {
        await _context.Users.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<User?> RenewUserAccessToken(int modelId, AuthToken token)
    {
        var user = await _context
            .Users
            .Include(x => x.AuthToken)
            .FirstOrDefaultAsync(x => x.Id == modelId);

        if (user is null)
            return null;

        user.AuthToken.CopyFromAccessToken(token);
        _context.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> RenewUserRefreshToken(int modelId, AuthToken token)
    {

        var user = await _context
            .Users
            .Include(x => x.AuthToken)
            .FirstOrDefaultAsync(x => x.Id == modelId);

        if (user is null)
            return null;
        
        user.AuthToken.CopyFromRefreshToken(token);
        _context.Update(user);
        await _context.SaveChangesAsync();
        return user;
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