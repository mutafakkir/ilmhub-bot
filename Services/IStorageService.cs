using bot.Entities;

namespace bot.Services;
public interface IStorageService
{
    Task<(bool IsSuccess, Exception exception)> InsertUserAsync(User user);
    Task<(bool IsSuccess, Exception exception)> UpdateUserAsync(User user);
    Task<bool> ExistsAsync(long chatId);
    Task<bool> ExistsAsync(string username);
    Task<User> GetUserAsync(long chatId);
    Task<User> GetUserAsync(string username);
    List<long> GetUsers();
}