using System.ComponentModel.DataAnnotations;

namespace bot.Entities;

public class User
{
    [Key]
    public long ChatId { get; set; }

    [MaxLength(255)]
    public string Username { get; set; }

    public string Fullname { get; set; }

    public string PhoneNumber { get; set; }

    public string Language { get; set; }

    public bool InProcess { get; set; }

    public string InterestedCources { get; set; }
    
    [Obsolete("Used only for Entity binding.")]
    public User() {}
    
    public User(long chatId, string username, string fullname, string phoneNumber, string language)
    {
        ChatId = chatId;
        Username = username;
        Fullname = fullname;
        PhoneNumber = phoneNumber;
        Language = language;
        InProcess = false;
        InterestedCources = string.Empty;
    }
}