namespace CryptoApi.Exchange.Cobinhood.Parameters

open FSharp.Json

module SocketParams =
    open System.Net.NetworkInformation

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

    type SubscribeToOrderBook = {
        action: string

        [<JsonField("type")>]
        channelType: string

        [<JsonField("trading_pair_id")>]
        tradingPairId: string

        precision: string
    }
