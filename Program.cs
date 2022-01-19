using bot.Entities;
using bot.Handlers;
using bot.Services;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BotDbContext>(options => 
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DbConnection"));
}, ServiceLifetime.Singleton);
builder.Services.AddSingleton<TelegramBotClient>(b => new TelegramBotClient(builder.Configuration.GetConnectionString("Token")));
builder.Services.AddHostedService<Bot>();
builder.Services.AddTransient<BotHandlers>();
builder.Services.AddTransient<IStorageService, DbStorageService>();
builder.Services.AddSingleton<GoogleSheetsHelpers>();
builder.Services.AddSingleton<GoogleSheetsServices>();

var app = builder.Build();

app.Run();
