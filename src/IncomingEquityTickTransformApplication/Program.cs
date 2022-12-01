// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using IncomingEquityTickTransformApplication.Schemas;
using IncomingEquityTickTransformApplication.Serializers;
using Newtonsoft.Json.Serialization;
using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SchemaRegistry.SerDes.Avro;
using Streamiz.Kafka.Net.SchemaRegistry.SerDes.Json;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.Stream;
using Streamiz.Kafka.Net.Table;
using System;

Console.WriteLine("Hello, World!");


/**
 * Demonstrates how to perform joins between  KStreams and GlobalKTables, i.e. joins that
 * don't require re-partitioning of the input streams.
 * 
 */

string EQUITYTICK_TOPIC = "EQUITYTICKDATA";
string INSTRUMENT_TOPIC = "instrument";
string INSTRUMENT_STORE = "instrument-store";
string INCOMING_EQUITYTICK_TOPIC = "incoming-equity-tick-data";


//var config = new StreamConfig<StringSerDes, StringSerDes>();
//config.ApplicationId = "incoming-equity-tick-app";
//config.BootstrapServers = "localhost:9092";
//config.AutoOffsetReset = AutoOffsetReset.Earliest;
//config.StateDir = Path.Combine(".", Guid.NewGuid().ToString());

var config = new StreamConfig<StringSerDes, StringSerDes>
{
    ApplicationId = "test-app",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var builder = new StreamBuilder();

var stringSerdes = new StringSerDes();
var equitickSerdes = new JsonSerDes<EquityTick>();
var instrumentSerdes = new JsonSerDes<Instrument>();

//var equityTickKStream = builder.Stream<string, EquityTick>("EQUITYTICKDATA", stringSerdes, equitickSerdes);
//var instrumentGlobalKTable = builder.GlobalTable<string, Instrument>("instrument", stringSerdes, instrumentSerdes);

var ticks = builder.Stream<string, EquityTick>("EQUITYTICKDATA", stringSerdes, equitickSerdes)
    .SelectKey((k, v) => v.Isin + v.MarketId);

var instruments = builder.Stream<string, Instrument>("instrument", stringSerdes, instrumentSerdes)
    .SelectKey((k, v) => v.Isin + v.MarketId);
    //.ToTable();

//ticks.Join(instruments, (t, i) => 
//    new IncomingEquityTick { 
//        Isin = t.Isin,
//        MarketId = t.MarketId,
//        Date = t.Date, 
//        hClose = t.hClose,
//        hSize = t.hSize,
//        InstrumentId = i.Id,
//        Symbol = i.Ticker
//    })
//    .To(INCOMING_EQUITYTICK_TOPIC);




ticks.To("haha-output");
instruments.To("instrument-output");

//var instrument_globaltable = builder.GlobalTable<string, Instrument>(INSTRUMENT_TOPIC, InMemory<string, Instrument>.As(INSTRUMENT_STORE));

//equitytick_stream.Join(instrument_globaltable,
//    (k, v) => k.ToUpper(),
//    (v1, v2) => $"{v1}-{v2}")
//    .To(INCOMING_EQUITYTICK_TOPIC);

Topology t = builder.Build();
KafkaStream stream = new KafkaStream(t, config);

await stream.StartAsync();


// -------------------------------------------------------------------------
//// Stream configuration
//var config = new StreamConfig<StringSerDes, StringSerDes>();
//config.ApplicationId = "test-app";
//config.BootstrapServers = "localhost:9092";

//StreamBuilder builder = new StreamBuilder();

//// Stream "test" topic with filterNot condition and persist in "test-output" topic.
//builder.Stream<string, string>("daily")
//    //.FilterNot((k, v) => v.Contains("haha"))
//    .To("test-output");

//// Create a table with "test-ktable" topic, and materialize this with in memory store named "test-store"
////builder.Table("test-ktable", InMemory<string, string>.As("test-store"));

//// Build topology
//Topology t = builder.Build();

//// Create a stream instance with toology and configuration
//KafkaStream stream = new KafkaStream(t, config);

//// Subscribe CTRL + C to quit stream application
//Console.CancelKeyPress += (o, e) =>
//{
//    stream.Dispose();
//};

//// Start stream instance with cancellable token
//await stream.StartAsync();
