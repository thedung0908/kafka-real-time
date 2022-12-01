using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomingEquityTickTransformApplication.Schemas
{
    public class Instrument
    {
        public int Id { get; set; }
        public string Isin { get; set; }
        public short MarketId { get; set; }
        public string Ticker { get; set; }
    }
}
