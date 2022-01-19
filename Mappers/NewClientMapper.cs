using bot.Entities;

namespace bot.Mappers;
public static class NewClientMapper
{
    public static List<NewClient> MapFromRangeData(IList<IList<object>> values)
    {
        var clients = new List<NewClient>();

        foreach (var value in values)
        {
            NewClient client = new()
            {
                UserName = value[0].ToString(),
                FullName = value[1].ToString(),
                InterestedCources = value[2].ToString(),
                Phone = value[3].ToString()
            };

            clients.Add(client);
        }

        return clients;
    }
    public static IList<IList<object>> MapToRangeData(NewClient client)
    {
        var objectList = new List<object>() { client.UserName, client.FullName, client.InterestedCources, client.Phone };
        var rangeData = new List<IList<object>> { objectList };
        return rangeData;
    }
}