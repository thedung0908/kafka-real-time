CREATE STREAM StreamDaily ( 
      hIsin STRING,
      hDate TIMESTAMP,
      hClose BYTES,
      hSize bigint,
      __source_table STRING) 
      WITH (KAFKA_TOPIC='daily', value_format='JSON', key_format='JSON', PARTITIONS=1);

CREATE STREAM StreamTick AS    
      SELECT
            hIsin AS ISIN,
            CASE
                  WHEN __source_table = 'frankfurt_daily_history' THEN 19
                  WHEN __source_table = 'london_daily_history' THEN 42
            END AS MarketId,
            hClose,
            hSize
      FROM StreamDaily emit changes;

CREATE STREAM RpStreamTick AS
   SELECT
        STRUCT(K1 := Isin, K2 := MarketId) AS K,
        hClose,
        hSize
   FROM StreamTick
   PARTITION BY STRUCT(K1 := Isin, K2 := MarketId)
   EMIT CHANGES;



----------------------------------------------------

CREATE STREAM StreamInstrument(
    Id int,
    Isin STRING,
    MarketId int,
    Ticker STRING
) WITH (KAFKA_TOPIC='instrument', value_format='JSON', key_format='JSON');

CREATE TABLE TblInstrument AS
   SELECT
      STRUCT(K1 := Isin, K2 := MarketId) AS K,
      latest_by_offset(Ticker) AS Symbol,
      latest_by_offset(Id) AS InstrumentId 
   FROM StreamInstrument
   GROUP BY STRUCT(K1 := Isin, K2 := MarketId)
   EMIT CHANGES;
   

----------------------------------------------------


CREATE STREAM IncomingEquityTick AS
      select i.K, d.HCLOSE, d.HSIZE, i.SYMBOL, i.INSTRUMENTID
      from RpStreamTick as d
      join TblInstrument as i on d.K = i.K
      emit changes;