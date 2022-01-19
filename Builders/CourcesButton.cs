using bot.Entities;
using bot.Handlers;
using Newtonsoft.Json;
using Telegram.Bot.Types.ReplyMarkups;

namespace bot.Builders;
public class CoursesButton
{
    public static string json = File.ReadAllText("Cources.json");
    public static List<Cources> Cources { get; set; } = JsonConvert.DeserializeObject<List<Cources>>(json);
    public async Task<IReplyMarkup> GetButtonsAsync(string lan)
    {
        var button = new List<List<KeyboardButton>>(){};
        foreach(var cources in Cources)
        {
            button.Add(
                new List<KeyboardButton>()
                {
                    new KeyboardButton(cources.Title)
                }
            );
        }
        button.Add(
            new List<KeyboardButton>()
            {
                new KeyboardButton(Language.backToMenu(lan))
            }
        );
        return new ReplyKeyboardMarkup(button);
    }
    public static List<Cources> GetCources() => Cources;
}