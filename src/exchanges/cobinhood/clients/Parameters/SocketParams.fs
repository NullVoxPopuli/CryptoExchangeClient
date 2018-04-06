namespace CryptoApi.Exchanges.Cobinhood.Parameters

open FSharp.Json

module SocketParams =
    type Action =
        | Subscribe
        | Unsubscribe
        | Ping

    let ActionToString (action: Action): string =
        match action with
        | Subscribe -> "subscribe"
        | Unsubscribe -> "unsubscribe"
        | Ping-> "ping"

    type ChannelType =
        | OrderBook
        | Order
        | Trade
        | Ticker
        | Candle

    let TypeToString (channelType: ChannelType): string =
        match channelType with
        | OrderBook -> "order-book"
        | Order -> "order"
        | Trade -> "trade"
        | Ticker -> "ticker"
        | Candle -> "candle"

    type SocketOrderBookParams = {
        channel: ChannelType
        symbol: string
        precision: string
    }

    type SocketTradeParams = {
        channel: ChannelType
        symbol: string
    }

    type SocketTickerParams = SocketTradeParams

    type SocketCandleParams = {
        channel: ChannelType
        symbol: string
        timeframe: string
    }

    type SubscribeToOrderBook = {
        action: string

        [<JsonField("type")>]
        channelType: string

        [<JsonField("trading_pair_id")>]
        tradingPairId: string

        precision: string
    }

    type SubscribeToTrade = {
        action: string

        [<JsonField("type")>]
        channelType: string

        [<JsonField("trading_pair_id")>]
        tradingPairId: string
    }

    type SubscribeToTicker = SubscribeToTrade

    type SubscribeToCandle = {
        action: string

        [<JsonField("type")>]
        channelType: string

        [<JsonField("trading_pair_id")>]
        tradingPairId: string

        [<JsonField("timeframe")>]
        timeframe: string
    }

    //type Ping = {
    //    action: string
    //}
