using bot.Entities;
using Microsoft.EntityFrameworkCore;

namespace bot.Services;
public class DbStorageService : IStorageService
{
    private readonly BotDbContext _context;

    public DbStorageService(BotDbContext context)
    {
        _context = context;
    }
    public async Task<bool> ExistsAsync(long chatId)
        => await _context.Users.AnyAsync(u => u.ChatId == chatId);


    public async Task<bool> ExistsAsync(string username)
        => await _context.Users.AnyAsync(u => u.Username == username);

    public async Task<User> GetUserAsync(long chatId)
        => await _context.Users.FirstOrDefaultAsync(u => u.ChatId == chatId);

    public async Task<User> GetUserAsync(string username)
        => await _context.Users.FirstOrDefaultAsync(u => u.Username == username);


    public async Task<(bool IsSuccess, Exception exception)> InsertUserAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return (true, null);
        }
        catch(Exception e)
        {
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception exception, User user)> RemoveAsync(User user)
    {
        try
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return (true, null, user);
        }
        catch(Exception e)
        {
            return (false, e, null);
        }
    }

    public async Task<(bool IsSuccess, Exception exception)> UpdateUserAsync(User user)
    {
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return (true, null);
        }
        catch(Exception e)
        {
            return (false, e);
        }
    }
    public List<long> GetUsers()
        => _context.Users.Select(u => u.ChatId).ToList();
}