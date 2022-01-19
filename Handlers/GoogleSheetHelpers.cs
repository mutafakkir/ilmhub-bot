using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace bot.Handlers;
public class GoogleSheetsHelpers
{
    public SheetsService Service { get; set; }
    const string APPLICATION_NAME = "IlmhubCenter";
    static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

    public GoogleSheetsHelpers()
    {
        InitializeService();
    }

    private void InitializeService()
    {
        var credential = GetCredentialsFromFile();
        Service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = APPLICATION_NAME
        });
    }

    private GoogleCredential GetCredentialsFromFile()
    {
        GoogleCredential credential;
        using (var stream = new FileStream("ilmhubot-12345.json", FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
        }

        return credential;
    }
}