# Real-Time Solution

Real-time solution for ShareGraph using Apache Kafka
## ------------------------------------------------------


# Streaming ETL Using Kafka Connect & KSQL DB

This is a demo project for demonstrating data extraction from multiple tables/databases using debezium, joining them on the fly using ksqldb and storing into a different table/database.

## Starting up the architecture

In order to run the infrastructure you should build and run the docker-compose file.

Start every container described in the `docker-compose.yml`,

```bash
> docker-compose up -d
```


## Building the ETL Pipeline

First we should create source connector (debezium) for listening the changes in the ActiveFN, Shark.Instruments schema objects. First things first we should open up a terminal to connect to Ksql Server by running the following command, after that we will be using the terminal of ksqldb client created here.

```bash
> docker-compose exec ksqldb-cli  ksql http://primary-ksqldb-server:8088
```


### Create Source Connector

The script for the source connector is avaliable at [./kafka/connectors/...]

```bash
$> curl -H "Accept:application/json" localhost:8083/connectors/
$> curl -i -X POST -H "Accept:application/json" -H "Content-Type:application/json" http://localhost:8083/connectors/ -d @saturday-connector.json
```


After that you should be able to see the topics for the tables residing in the inventory schema at mysql.

```bash
ksql> show topics;
```

> **_NOTE:_** In order to keep the offset at begining during the demo please run the following command!
>
>```bash
>ksql> SET 'auto.offset.reset' = 'earliest';
>````


### Create Transformations with Streams and Tables
Run the following script which is avaliable in [./kafka/ksql/transform/IncomingEquityTickData.sql] creating stream and tables for the transformation.



### Create Sink Connector
[...]


### Running SG3-APIs
[...]