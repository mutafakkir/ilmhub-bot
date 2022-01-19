using bot.Handlers;
using Telegram.Bot.Types.ReplyMarkups;

namespace bot.Builders;
public class MessageBuilder
{
    public static IReplyMarkup GetContact(string lan)
    => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>()
        {
            new List<KeyboardButton>()
            {
                new KeyboardButton(Language.getContact(lan)){ RequestContact = true}
            }
        })
        {
            ResizeKeyboard =true,
            OneTimeKeyboard = true
        };
    public static IReplyMarkup LanguageButton()
    {
        return new InlineKeyboardMarkup(
            new InlineKeyboardButton[]
            {
                InlineKeyboardButton.WithCallbackData(text: "O'zbek tili🇺🇿", "uz"),
                InlineKeyboardButton.WithCallbackData(text: "Русский🇷🇺", "ru"),
                InlineKeyboardButton.WithCallbackData(text: "English🏴󠁧󠁢󠁥󠁮󠁧󠁿", "en")
            }
        );
    }
    public static ReplyKeyboardMarkup Enroll(string lan)
    => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>()
        {
            new List<KeyboardButton>()
            {
                new KeyboardButton(Language.Enroll(lan)){ },
                new KeyboardButton(Language.BackToCourses(lan)){ }
            }
        })
        {
            ResizeKeyboard =true,
            OneTimeKeyboard = true
        };
    public static ReplyKeyboardMarkup Menu(string lan)
    => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>()
        {
            new List<KeyboardButton>()
            {
                new KeyboardButton(Language.RequestCourse(lan)){}
            },
            new List<KeyboardButton>()
            {
                new KeyboardButton(Language.Branches(lan)){ },
                new KeyboardButton(Language.Contacts(lan)){ }
            },
            new List<KeyboardButton>()
            {
                new KeyboardButton(Language.settings(lan)){ },
            }
        })
        {
            ResizeKeyboard =true,
            OneTimeKeyboard = true
        };
        public static ReplyKeyboardMarkup Settings(string lan)
    => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>()
        {
            new List<KeyboardButton>()
            {
                new KeyboardButton(Language.Change(lan)){}
            },
            new List<KeyboardButton>()
            {
                new KeyboardButton(Language.backToMenu(lan))
            }
        })
        {
            ResizeKeyboard =true,
            OneTimeKeyboard = true
        };
}