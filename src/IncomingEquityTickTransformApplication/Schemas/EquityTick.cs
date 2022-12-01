using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomingEquityTickTransformApplication.Schemas
{
    public class EquityTick
    {
        public string Isin { get; set; }
        public int MarketId { get; set; }
        public DateTime Date { get; set; }
        public byte[] hClose { get; set; }
        public int? hSize { get; set; }

    }
}
