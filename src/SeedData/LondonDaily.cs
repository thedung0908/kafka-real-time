using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeedData
{
    [Keyless]
    [Table("london_daily_history")]
    public class LondonDaily
    {
        [Column("hIsin")]
        public string Isin { get; set; }

        [Column("hDate")]
        public DateTime? Date { get; set; }

        [Column("hClose")]
        public decimal Close { get; set; }

        [Column("hSize")]
        public long? Size { get; set; }

        public LondonDaily(string isin)
        {
            Isin = isin;
            Date = DateTime.UtcNow;
            Close = Random.Shared.Next(1, 1000);
            Size = Random.Shared.Next(1, 10000);
        }
    }
}
