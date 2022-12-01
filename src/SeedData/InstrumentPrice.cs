using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SeedData
{
    [Keyless]
    [Table("InstrumentPrice")]
    public class InstrumentPrice
    {
        public InstrumentPrice()
        {
            var instrumentIds = new List<int>() { 32865 };
            InstrumentId = instrumentIds[Random.Shared.Next(0, instrumentIds.Count)];
            Bid = Random.Shared.Next(1, 10);
            Ask = Random.Shared.Next(100, 120);
            Open = Random.Shared.Next(100, 120);
            Last = Random.Shared.Next(100, 120);
            High = Random.Shared.Next(100, 120);
            Low = Random.Shared.Next(100, 120);
            Volume = Random.Shared.Next(600, 700);
            Mid = Random.Shared.Next(100, 120);
            Date = DateTime.UtcNow;
            PrevClose = Random.Shared.Next(100, 120);
            Change = Random.Shared.Next(100, 120);
            LastRowChange =  DateTime.UtcNow;
            ChangePercentage = Random.Shared.Next(100, 120);
            TodayTurnover = Random.Shared.Next(100, 120);
            VWAP = Random.Shared.Next(100, 120);
            BidSize = Random.Shared.Next(100, 120);
            AskSize = Random.Shared.Next(100, 120);
            OfficialClose = Random.Shared.Next(100, 120);
            OfficialCloseDate =  DateTime.UtcNow;
        }

        public int InstrumentId {get; set;}
        public decimal? Bid {get; set;}
        public decimal? Ask {get; set;}
        public decimal? Open {get; set;}
        public decimal? Last {get; set;}
        public decimal? High {get; set;}
        public decimal? Low {get; set;}
        public long? Volume {get; set;}
        public decimal? Mid {get; set;}
        public DateTime? Date {get; set;}
        public decimal? PrevClose {get; set;}
        public decimal? Change {get; set;}
        public DateTime? LastRowChange {get; set;}
        public decimal? ChangePercentage {get; set;}
        public decimal? TodayTurnover {get; set;}
        public decimal? VWAP {get; set;}
        public int BidSize {get; set;}
        public int AskSize {get; set;}
        public decimal? OfficialClose {get; set;}
        public DateTime? OfficialCloseDate {get; set;}
    }
}