namespace CryptoApi.Exchanges.Cobinhood.Data.Providers

open FSharp.Data

module WebSocketV2 =
    type Payload = JsonProvider<"""{
        "h": ["channel-name", "version", "message-type"],
        "d": []
    }""">

    // [channel_id, version, type]
    type Pong = JsonProvider<"""{
        "h": ["", "2", "pong"],
        "d": []
    }""">

    // [channel_id, version, type]
    type Subscribe = JsonProvider<"""{
        "h": ["trade.ETH-BTC", "2", "unsubscribed"],
        "d": []
    }""">

    // [channel_id, version, type, error_code, msg]
    type Error = JsonProvider<"""{
        "h": ["", "2", "error", "4002", "channel_not_found"],
        "d": []
    }""">

    type Order = JsonProvider<"""{
      "h": ["order", "2", "u", "0"],
      "d": []
    }""">

    // [channel_id, version, type]
    type LimitOrder = JsonProvider<"""{
        "h": ["order", "2", "u", "0"],
        "d": [
            "ORDER_ID",
            "TIMESTAMP",
            "COMPLETED_AT",
            "TRADING_PAIR_ID",
            "STATE",
            "EVENT",
            "SIDE",
            "PRICE",
            "EQ_PRICE",
            "SIZE",
            "PARTIAL_FILLED_SIZE"
        ]
    }""">

    //// [channel_id, version, type]
    type MarketOrder = JsonProvider<"""{
        "h": ["order", "2", "u", "1"],
        "d": [
            "ORDER_ID",
            "TIMESTAMP",
            "COMPLETED_AT",
            "TRADING_PAIR_ID",
            "STATE",
            "EVENT",
            "SIDE",
            "EQ_PRICE",
            "SIZE",
            "PARTIAL_FILLED_SIZE"
        ]
    }""">

    //// [channel_id, version, type]
    type MarketStopOrder = JsonProvider<"""{
        "h": ["order", "2", "u", "3"],
        "d": [
            "ORDER_ID",
            "TIMESTAMP",
            "COMPLETED_AT",
            "TRADING_PAIR_ID",
            "STATE",
            "EVENT",
            "SIDE",
            "EQ_PRICE",
            "SIZE",
            "PARTIAL_FILLED_SIZE",
            "STOP_PRICE"
        ]
    }""">

    //// [channel_id, version, type]
    type LimitStopOrder = JsonProvider<"""{
        "h": ["order", "2", "u", "4"],
        "d": [
            "ORDER_ID",
            "TIMESTAMP",
            "COMPLETED_AT",
            "TRADING_PAIR_ID",
            "STATE",
            "EVENT",
            "SIDE",
            "PRICE",
            "EQ_PRICE",
            "SIZE",
            "PARTIAL_FILLED_SIZE",
            "STOP_PRICE"
        ]
    }""">

    // [channel_id, version, type]
    // [price, size, count]
    type OrderBook = JsonProvider<"""{
        "h": ["order-book.COB-ETH.1E-7", "2", "u"],
        "d": {
            "bids": [
                ["0.268","1","1000"],
                ["0.25","1","6872.221"]
            ],
            "asks": [
                ["0.248","1","1000"],
                ["0.247","1","6872.221"]
            ]
        }
    }""", InferTypesFromValues = false>

    type Trade = JsonProvider<"""{
        "h": ["trade.COB-ETH", "2", "u"],
        "d":
            [
              ["trade-id", "time-stamp", "price", "size", "maker_side"]
            ]
    }""">

    type Ticker = JsonProvider<"""{
        "h": ["ticker.COB-ETH", "2", "u"],
        "d": [
            [
              "TIME_STAMP",
              "HIGHEST_BID",
              "LOWEST_ASK",
              "24H_VOLUME",
              "24H_HIGH",
              "24H_LOW",
              "24H_OPEN",
              "LAST_TRADE_PRICE"
            ]
        ]
    }""">

    type Candle = JsonProvider<"""{
        "channel_id": "CHANNEL_ID",
        "snapshot":
            [
              ["TIME_STAMP", "VOL", "HIGH", "LOW", "OPEN", "CLOSE"]
            ]
    }""">

    let ChannelStartsWith (payloadString: string, startsWith: string): Option<string> =
        let payload = payloadString |> Payload.Parse
        let header = payload.H

        let channel = header.[0]

        if channel.StartsWith(startsWith)
        then Some(channel)
        else None


    let (|IsOrderBook|_|) (payload: string) =
        match ChannelStartsWith(payload, "order-book") with
        | None -> None
        | Some channel ->
            let channelParts = channel.Split [|' '|]
            let channelName = channelParts.[0]
            let pair = channelParts.[1]
            let precision = channelParts.[2]

            Some(channelName, pair, precision)

    let (|IsTrade|_|) (payload: string) =
        match ChannelStartsWith(payload, "trade") with
        | None -> None
        | Some channel ->
            let channelParts = channel.Split [|' '|]
            let channelName = channelParts.[0]
            let pair = channelParts.[1]

            Some(channelName, pair)


    let (|IsTicker|_|) (payload: string) =
        match ChannelStartsWith(payload, "ticker") with
        | None -> None
        | Some channel ->
            let channelParts = channel.Split [|' '|]
            let channelName = channelParts.[0]
            let pair = channelParts.[1]

            Some(channelName, pair)

