using bot.Entities;
using bot.Handlers;
using bot.Mappers;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;

namespace bot.Services;
public class GoogleSheetsServices
{
    const string SPREADSHEET_ID = "1YJInMiaaznFoPC-t_uHTT5-u29A3rqOwBFvuOM_9DgU";
    const string SHEET_NAME = "NewUser";
    public readonly SpreadsheetsResource.ValuesResource _googleSheetValues;
    public GoogleSheetsServices(GoogleSheetsHelpers googleSheetsHelper)
    {
        _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;
    }
    public async Task AddNewClient(NewClient newClient)
    {
        var range = $"{SHEET_NAME}!A2:D2";
        var valueRange = new ValueRange
        {
            Values = NewClientMapper.MapToRangeData(newClient)
        };
        var appendRequest = _googleSheetValues.Append(valueRange, SPREADSHEET_ID, range);
        appendRequest.ValueInputOption = AppendRequest.ValueInputOptionEnum.USERENTERED;
        appendRequest.Execute();
    }
    public async Task<List<NewClient>> GetNewClientsAsync()
    {
        var range = $"{SHEET_NAME}!A:D";
        var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
        var response = request.Execute();
        var values = response.Values;
        return NewClientMapper.MapFromRangeData(values);
    }
}