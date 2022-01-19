using bot.Builders;
using bot.Entities;
using bot.Services;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace bot.Handlers;
public class BotHandlers
{
    private readonly ILogger<BotHandlers> _logger;
    private readonly GoogleSheetsServices _sheetsServices;
    private readonly IStorageService _storage;
    private CoursesButton coursesButton { get; set; } = new CoursesButton();

    public BotHandlers(ILogger<BotHandlers> logger, GoogleSheetsServices sheetsServices, IStorageService storage)
    {
        _logger = logger;
        _sheetsServices = sheetsServices;
        _storage = storage;
    }
    public Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken ctoken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException => $"Error occured with Telegram Client: {exception.Message}",
            _ => exception.Message
        };

        _logger.LogCritical(errorMessage);

        return Task.CompletedTask;
    }
    public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken ctoken)
    {
        var handler = update.Type switch
        {
            UpdateType.Message => BotOnMessageReceived(client, update.Message),
            UpdateType.EditedMessage => BotOnMessageEdited(client, update.EditedMessage),
            UpdateType.CallbackQuery => BotOnCallbackQueryReceived(client, update.CallbackQuery),
            UpdateType.InlineQuery => BotOnInlineQueryReceived(client, update.InlineQuery),
            UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(client, update.ChosenInlineResult),
            _ => UnknownUpdateHandlerAsync(client, update)
        };

        try
        {
            await handler;
        }
        catch(Exception e)
        {
            _logger.LogWarning(e.Message);
        }
    }

    private async Task UnknownUpdateHandlerAsync(ITelegramBotClient client, Update update)
    {
        throw new NotImplementedException();
    }

    private async Task BotOnChosenInlineResultReceived(ITelegramBotClient client, ChosenInlineResult chosenInlineResult)
    {
        throw new NotImplementedException();
    }

    private async Task BotOnInlineQueryReceived(ITelegramBotClient client, InlineQuery inlineQuery)
    {
        throw new NotImplementedException();
    }

    private async Task BotOnCallbackQueryReceived(ITelegramBotClient client, CallbackQuery callbackQuery)
    {
        var user = await _storage.GetUserAsync(callbackQuery.Message.Chat.Id);
        if(callbackQuery.Data == "uz") user.Language = "uz";

        if(callbackQuery.Data == "ru") user.Language = "ru";

        if(callbackQuery.Data == "en") user.Language = "en";
        await _storage.UpdateUserAsync(user);
        await  client.DeleteMessageAsync(
            callbackQuery.Message.Chat.Id,
            callbackQuery.Message.MessageId);
        await client.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            Language.choosenLan(user.Language));
        await client.SendTextMessageAsync(
            user.ChatId,
            Language.SendNumber(user.Language),
            replyMarkup: MessageBuilder.GetContact(user.Language)
        );
    }

    private async Task BotOnMessageEdited(ITelegramBotClient client, Message editedMessage)
    {
        throw new NotImplementedException();
    }

    private async Task BotOnMessageReceived(ITelegramBotClient client, Message message)
    {
        var user = await _storage.GetUserAsync(message.Chat.Id);    
        if(message.Text == "/start" || Language.Change(user.Language) == message.Text)
        {
            if(await _storage.ExistsAsync(message.Chat.Id) == false)
            {
                await _storage.InsertUserAsync(
                    new Entities.User(
                        message.Chat.Id, message.From.Username, string.Empty, string.Empty, string.Empty
                    )
                );
            }
            await client.SendTextMessageAsync(
                message.Chat.Id,
                "Iltimos, tilni tanlang.\nПожалуйста, выберите язык.\nPlease, choose language.",
                ParseMode.Markdown,
                replyMarkup: MessageBuilder.LanguageButton());
        }
        if(user.InProcess)
        {
            user.Fullname = message.Text;
            user.InProcess = false;
            await _storage.UpdateUserAsync(user);
            await client.SendTextMessageAsync(
                user.ChatId,
                Language.CanUse(user.Language),
                replyMarkup: MessageBuilder.Menu(user.Language)
            );
        }
        if(message.Contact != null)
        {
            user.PhoneNumber = message.Contact.PhoneNumber;
            user.InProcess = true;
            await _storage.UpdateUserAsync(user);
            await client.SendTextMessageAsync(
                user.ChatId,
                Language.getName(user.Language),
                replyMarkup:new ReplyKeyboardMarkup(new KeyboardButton(string.Empty)){}
            );
        }
        if(message.Text == Language.RequestCourse(user.Language))
        {
            await client.SendTextMessageAsync(
                user.ChatId,
                Language.Courses(user.Language),
                replyMarkup: await coursesButton.GetButtonsAsync(user.Language)
            );
        }
        if(CoursesButton.GetCourses().FirstOrDefault(c => c.Title == message.Text) != default)
        {
            var course = CoursesButton.GetCourses().FirstOrDefault(c => c.Title == message.Text);
            using (Stream stream = System.IO.File.OpenRead($"Data/Images/{course.BannerId}"))
            {
                await client.SendPhotoAsync(
                    chatId: user.ChatId,
                    photo: stream,
                    caption: $"<b>{course.Title}</b>\n\n{course.Description}\n\n<b>Kurs davomiyligi:</b> {course.DurationInMonth} oy.\n\n<b>Kurs narhi:</b> {course.CostInUzs} so'm.\n\n@ilmhubot",
                    parseMode:ParseMode.Html,
                    replyMarkup: MessageBuilder.Enroll(user.Language)
                );
            }
            user.InterestedCourse = message.Text;
            await _storage.UpdateUserAsync(user);
        }
        if(Language.Enroll(user.Language) == message.Text)
        {
            var newClient = new NewClient(){
                UserName = user.Username,
                FullName = user.Fullname,
                InterestedCourse = user.InterestedCourse,
                Phone = user.PhoneNumber
            };
            await _sheetsServices.AddNewClient(newClient);
            await client.SendTextMessageAsync(
                user.ChatId,
                "Tez orada administratorlarimiz siz bilan aloqaga chiqishadi",
                replyMarkup: MessageBuilder.Menu(user.Language)
            );
        }
        if(Language.Branches(user.Language) == message.Text)
        {
            var m = await client.SendLocationAsync(
                user.ChatId,
                41.337698,69.356302
            );
            await client.SendTextMessageAsync(
                user.ChatId,
                "Mirzo Ulug'bek tumani filiali",
                replyToMessageId: m.MessageId
            );
        }
        if(Language.BackToCourses(user.Language) == message.Text)
        {
            await client.SendTextMessageAsync(
                user.ChatId,
                Language.Courses(user.Language),
                replyMarkup: await coursesButton.GetButtonsAsync(user.Language)
            );
        }
        if(Language.backToMenu(user.Language) == message.Text)
        {
            await client.SendTextMessageAsync(
                user.ChatId,
                Language.menu(user.Language),
                replyMarkup: MessageBuilder.Menu(user.Language)
                );
        }
        if(Language.settings(user.Language) == message.Text)
        {
            await client.SendTextMessageAsync(
                user.ChatId,
                Language.settingReply(user.Language),
                replyMarkup: MessageBuilder.Settings(user.Language)
            );
        }
        if(Language.Contacts(user.Language) == message.Text)
        {
            await client.SendTextMessageAsync(
                user.ChatId,
                $"📞 +998946715060\n\n<a href=\"https://t.me/ilmhubuz\">🔘 Telegram</a>\n\n<a href=\"https://instagram.com/ilmhubuz\">🔘 Instagram</a>",
                parseMode:ParseMode.Html
            );
        }
        // foreach(var i in _storage.GetUsers())
        // {
        //     try
        //     {
        //         await client.SendTextMessageAsync(
        //             i,
        //             "Bot hozircha ishga tushdi, qani /start ni bosib sinab ko'ringchi"
        //         );
        //     }
        //     catch
        //     {

        //     }
        // }
    }
}