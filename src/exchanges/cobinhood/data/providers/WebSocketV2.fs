namespace CryptoApi.Exchanges.Cobinhood.Data.Providers

open FSharp.Data

module WebSocketV2 =
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

    // TODO: figure out how to represent this type of array data
    // [channel_id, version, type]
    //type LimitOrder = JsonProvider<"""{
    //    "h": ["order", "2", "u", "0"],
    //    "d": [
    //        ORDER_ID,
    //        TIMESTAMP,
    //        COMPLETED_AT,
    //        TRADING_PAIR_ID,
    //        STATE,
    //        EVENT,
    //        SIDE,
    //        PRICE,
    //        EQ_PRICE,
    //        SIZE,
    //        PARTIAL_FILLED_SIZE
    //    ]
    //}""">

    //// [channel_id, version, type]
    //type MarketOrder = JsonProvider<"""{
    //    "h": ["order", "2", "u", "1"],
    //    "d": [
    //        ORDER_ID,
    //        TIMESTAMP,
    //        COMPLETED_AT,
    //        TRADING_PAIR_ID,
    //        STATE,
    //        EVENT,
    //        SIDE,
    //        EQ_PRICE,
    //        SIZE,
    //        PARTIAL_FILLED_SIZE
    //    ]
    //}""">

    //// [channel_id, version, type]
    //type MarketStopOrder = JsonProvider<"""{
    //    "h": ["order", "2", "u", "3"],
    //    "d": [
    //        ORDER_ID,
    //        TIMESTAMP,
    //        COMPLETED_AT,
    //        TRADING_PAIR_ID,
    //        STATE,
    //        EVENT,
    //        SIDE,
    //        EQ_PRICE,
    //        SIZE,
    //        PARTIAL_FILLED_SIZE,
    //        STOP_PRICE
    //    ]
    //}""">

    //// [channel_id, version, type]
    //type LimitStopOrder = JsonProvider<"""{
    //    "h": ["order", "2", "u", "4"],
    //    "d": [
    //        ORDER_ID,
    //        TIMESTAMP,
    //        COMPLETED_AT,
    //        TRADING_PAIR_ID,
    //        STATE,
    //        EVENT,
    //        SIDE,
    //        PRICE,
    //        EQ_PRICE,
    //        SIZE,
    //        PARTIAL_FILLED_SIZE,
    //        STOP_PRICE
    //    ]
    //}""">

    //// [channel_id, version, type]
    //type OrderBook = JsonProvider<"""{
    //    "h": ["order-book.COB-ETH.1E-7", "2", "u"],
    //    "d": {
    //        "bids": [
    //            [ PRICE, SIZE, COUNT ],
    //            [ PRICE, SIZE, COUNT ]
    //        ],
    //        "asks": [
    //            [ PRICE, SIZE, COUNT ],
    //            [ PRICE, SIZE, COUNT ]
    //    }
    //}""">

    //type Trade = JsonProvider<"""{
    //    "h": ["trade.COB-ETH", "2", "u"],
    //    "d":
    //        [
    //          [TRADE_ID, TIME_STAMP, PRICE, SIZE, MAKER_SIDE],
    //          [TRADE_ID, TIME_STAMP, PRICE, SIZE, MAKER_SIDE]
    //        ]
    //}""">

    //type Ticker = JsonProvider<"""{
    //    "h": ["ticker.COB-ETH", "2", "u"],
    //    "d": [
    //        [
    //          TIME_STAMP,
    //          HIGHEST_BID,
    //          LOWEST_ASK,
    //          24H_VOLUME,
    //          24H_HIGH,
    //          24H_LOW,
    //          24H_OPEN,
    //          LAST_TRADE_PRICE
    //        ]
    //    ]
    //}""">

    //type Candle = JsonProvider<"""{
    //    "channel_id": CHANNEL_ID,
    //    "snapshot":
    //        [
    //          [TIME_STAMP, VOL, HIGH, LOW,OPEN, CLOSE]
    //        ]
    //}""">