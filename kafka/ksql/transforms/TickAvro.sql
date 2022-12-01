---------------------------------------------------------------------------------------------------
-- Create sources:
---------------------------------------------------------------------------------------------------

-- stream of equity tick data (x-daily-history):
CREATE STREAM tickstream ( 
      hIsin string,
      hDate timestamp,
      hClose decimal(19,4),
      hSize bigint,
      __source_table string) 
      WITH (kafka_topic='daily', value_format='AVRO', key_format='JSON', partitions=1);

-- stream of shark-instruments:
CREATE STREAM instruments(
    Id int,
    Isin string,
    MarketId int,
    Ticker string
) WITH (kafka_topic='instrument', value_format='AVRO', key_format='AVRO');


-- instruments lookup table:
CREATE TABLE instrumentsTbl AS
   SELECT
      struct(K1 := Isin, K2 := MarketId) AS K,
      latest_by_offset(Ticker) AS Symbol,
      latest_by_offset(Id) AS InstrumentId 
   FROM instruments
   GROUP BY STRUCT(K1 := Isin, K2 := MarketId)
   EMIT CHANGES;



---------------------------------------------------------------------------------------------------
-- Build materialized stream views:
---------------------------------------------------------------------------------------------------

-- enrich tick-stream with marketID:
CREATE STREAM tickWithMarketInfo AS    
      SELECT
            hIsin AS ISIN,
            CASE
                  WHEN __source_table = 'frankfurt_daily_history' THEN 19
                  WHEN __source_table = 'london_daily_history' THEN 42
            END AS MarketId,
            hClose,
            hDate,
            hSize
      FROM tickstream emit changes;

      

-- rekey tick-stream
CREATE STREAM ticks AS
   SELECT
        STRUCT(K1 := Isin, K2 := MarketId) AS K,
        hClose,
        hDate,
        hSize
   FROM tickWithMarketInfo
   PARTITION BY STRUCT(K1 := Isin, K2 := MarketId)
   EMIT CHANGES;

-- enrich tick-stream with more instrument information:
CREATE STREAM IncomingEquityTick AS
      select i.K, d.HCLOSE, d.HSIZE, d.HDATE, i.SYMBOL, i.INSTRUMENTID
      from ticks as d
      join instrumentsTbl as i on d.K = i.K
      emit changes;


