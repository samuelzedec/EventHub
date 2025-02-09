using backend.Data;
using backend.Extensions;
using backend.Models;
using Microsoft.EntityFrameworkCore;
namespace backend.Repositories;

public class VerificationCodeRepository
{
    private readonly EventHubDbContext _context;
    public VerificationCodeRepository(EventHubDbContext context)
        => _context = context;

    public async Task<VerificationCode?> GetVerificationCodeByUserEmailAsync(string userEmail)
        => await _context
            .VerificationCodes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserEmail.ToLower() == userEmail.ToLower());

    public async Task<VerificationCode> InsertAsync(VerificationCode model)
    {
        await _context.VerificationCodes.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<VerificationCode?> UpdateByUserEmailAsync(string userEmail, VerificationCode model)
    {
        var verificationCode = await _context
            .VerificationCodes
            .FirstOrDefaultAsync(x => x.UserEmail.ToLower() == userEmail.ToLower());

        if (verificationCode is null)
            return null;

        verificationCode.CopyFrom(model);
        await _context.SaveChangesAsync();
        return verificationCode;
    }

    public async Task<bool> DeleteByUserEmailAsync(string userEmail)
    {
        var verificationCode = await _context
            .VerificationCodes
            .FirstOrDefaultAsync(x => x.UserEmail.ToLower() == userEmail.ToLower());

        if (verificationCode is null)
            return false;

        _context.VerificationCodes.Remove(verificationCode);
        await _context.SaveChangesAsync();
        return true;
    }
}