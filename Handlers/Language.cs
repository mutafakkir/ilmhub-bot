namespace bot.Handlers;
public class Language
{
    public static string SendNumber(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Iltimos, raqamingizni yuboring📞";

        if(lan == "ru")
        result = "Пожалуйста, пришлите свой номер📞";

        if(lan == "en")
        result = "Please, send your number📞";

        return result;
    }
    public static string Change(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Tilni va ma'lumotlarni o'zgartirish";

        if(lan == "ru")
        result = "Изменить язык и данные";

        if(lan == "en")
        result = "Change language and data";

        return result;
    }
    public static string Courses(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Kurslar💻";

        if(lan == "ru")
        result = "Курсы💻";

        if(lan == "en")
        result = "Courses💻";

        return result;
    }
    public static string BackToCourses(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Kurslarga qaytish";

        if(lan == "ru")
        result = "Назад в курсы";

        if(lan == "en")
        result = "Back to courses";

        return result;
    }
    public static string Enroll(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Yozilish";

        if(lan == "ru")
        result = "Записаться";

        if(lan == "en")
        result = "Enroll";

        return result;
    }
    public static string CanUse(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Botdan foydalanishingiz mumkin";

        if(lan == "ru")
        result = "Теперь вы можете использовать этого бота";

        if(lan == "en")
        result = "Now you can use this bot";

        return result;
    }
    public static string Contacts(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "☎️Kontaktlar";

        if(lan == "ru")
        result = "☎️Контакты";

        if(lan == "en")
        result = "☎️Contacts";

        return result;
    }
    public static string Branches(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "📍Filiallar";

        if(lan == "ru")
        result = "📍Филиалы";

        if(lan == "en")
        result = "📍Branches";

        return result;
    }
    public static string RequestCourse(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "👨🏻‍💻Kursga yozilish";

        if(lan == "ru")
        result = "👨🏻‍💻Записаться на курс";

        if(lan == "en")
        result = "👨🏻‍💻Enroll in a course";

        return result;
    }
    public static string getName(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Ism-familyangizni kiriting✍️";

        if(lan == "ru")
        result = "Введите свое имя и фамилию✍️";

        if(lan == "en")
        result = "Enter firstname and lastname✍️";

        return result;
    }
    public static string getContact(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Telefon raqamni jo'natish📞";

        if(lan == "ru")
        result = "Отправить телефонный номер📞";

        if(lan == "en")
        result = "Send phonenumber📞";

        return result;
    }
    public static string choosenLan(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "O'zbek tili tanlandi🇺🇿";

        if(lan == "ru")
        result = "Русский язык выбран🇷🇺";

        if(lan == "en")
        result = "English was chosen🏴󠁧󠁢󠁥󠁮󠁧󠁿";

        return result;
    }
    public static string settings(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "⚙️Sozlamalar";

        if(lan == "ru")
        result = "⚙️Настройки";

        if(lan == "en")
        result = "⚙️Settings";

        return result;
    }
    public static string changeLanguage(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Tilni o'zgartirish";

        if(lan == "ru")
        result = "Изменить язык";

        if(lan == "en")
        result = "Change language";

        return result;
    }
    public static string backToMenu(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Menyuga qaytish";

        if(lan == "ru")
        result = "Назад к меню";

        if(lan == "en")
        result = "Back to menu";

        return result;
    }
    public static string settingReply(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Sozlamalar menyusi";

        if(lan == "ru")
        result = "Меню настроек";

        if(lan == "en")
        result = "Settings menu";

        return result;
    }
    public static string menu(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Menyu";

        if(lan == "ru")
        result = "Меню";

        if(lan == "en")
        result = "Menu";

        return result;
    }
    public static string chooseLan(string lan)
    {
        string result = null;
        if(lan == "uz")
        result = "Tilni tanlang";

        if(lan == "ru")
        result = "Выберите язык";

        if(lan == "en")
        result = "Choose language";

        return result;
    }
}
