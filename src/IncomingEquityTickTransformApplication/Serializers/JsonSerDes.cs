using Confluent.Kafka;
using Newtonsoft.Json;
using Streamiz.Kafka.Net.SerDes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomingEquityTickTransformApplication.Serializers
{
    public class JsonSerDes<T> : AbstractSerDes<T>
    {
        public override T Deserialize(byte[] data, SerializationContext context)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(data));
        }

        public override byte[] Serialize(T data, SerializationContext context)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
        }
    }
}
