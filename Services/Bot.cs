using bot.Handlers;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace bot.Services;
public class Bot : BackgroundService
{
    private readonly TelegramBotClient _botClient;
    private readonly ILogger<Bot> _logger;

    public Bot(TelegramBotClient client, ILogger<Bot> logger, BotHandlers handlers)
    {
        _botClient = client;
        _logger = logger;
        _botClient.StartReceiving(new DefaultUpdateHandler(handlers.HandleUpdateAsync, handlers.HandleErrorAsync));
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var me = await _botClient.GetMeAsync();
        _logger.LogInformation($"{me.Username} has connected successfully!");
    }
}