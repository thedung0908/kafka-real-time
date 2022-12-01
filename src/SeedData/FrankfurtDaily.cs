using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeedData
{
    [Keyless]
    [Table("frankfurt_daily_history")]
    public class FrankfurtDaily
    {
        [Column("hIsin")]
        public string Isin { get; set; }

        [Column("hDate")]
        public DateTime? Date { get; set; }

        [Column("hClose")]
        public decimal Close { get; set; }

        [Column("hSize")]
        public long? Size { get; set; }

        public FrankfurtDaily(string isin)
        {
            Isin = isin;
            Date = DateTime.UtcNow;
            Close = Random.Shared.Next(916, 921);
            Size = Random.Shared.Next(2000, 2500);
        }
    }
}
