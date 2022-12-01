using Microsoft.EntityFrameworkCore;
using SeedData;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataProvidersDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("FridayDbConnectionString")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataProvidersDbContext>();

    while (true)
    {
        var f1 = new LondonDaily("NL0000009165");
        var l1 = new FrankfurtDaily("DK0010181759");
        var i1 = new InstrumentPrice();
        i1.Last = l1.Close;
        

        dbContext.Database.ExecuteSqlRaw($"INSERT INTO [dbo].[frankfurt_daily_history](hIsin, hDate, hClose, hSize) VALUES ('{l1.Isin}', '{l1.Date}', {l1.Close}, {l1.Size})");
        // dbContext.Database.ExecuteSqlRaw($"INSERT INTO [dbo].[london_daily_history](hIsin, hDate, hClose, hSize) VALUES ('{f1.Isin}', '{f1.Date}', {f1.Close}, {f1.Size})");
        dbContext.Database.ExecuteSqlRaw($"INSERT INTO [dbo].[InstrumentPrice]([InstrumentId] ,[Bid] ,[Ask] ,[Open] ,[Last] ,[High] ,[Low] ,[Volume] ,[Mid] ,[Date] ,[PrevClose] ,[Change] ,[LastRowChange] ,[ChangePercentage] ,[TodayTurnover] ,[VWAP] ,[BidSize] ,[AskSize] ,[OfficialClose] ,[OfficialCloseDate]) VALUES ( {i1.InstrumentId}, {i1.Bid}, {i1.Ask}, {i1.Open}, {i1.Last}, {i1.High}, {i1.Low}, {i1.Volume}, {i1.Mid}, '{i1.Date?.ToString("yyyy-MM-dd HH:mm:ss")}', {i1.PrevClose}, {i1.Change}, '{i1.LastRowChange?.ToString("yyyy-MM-dd HH:mm:ss")}', {i1.ChangePercentage}, {i1.TodayTurnover}, {i1.VWAP}, {i1.BidSize}, {i1.AskSize}, {i1.OfficialClose}, '{i1.OfficialCloseDate?.ToString("yyyy-MM-dd HH:mm:ss")}')");

        dbContext.SaveChanges();

        Thread.Sleep(3000);
    }
}

app.Run();

