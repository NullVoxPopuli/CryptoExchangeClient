namespace CryptoApi.Exchanges.Cobinhood.WebSocket


exception UnknownChannelType of string
exception UnknownMessageType of string

module MessageHandler =
    open CryptoApi.Exchanges.Cobinhood.Data.Providers
    open CryptoApi.Exchanges.Cobinhood.Data.Transformers
    open CryptoApi.Exchanges.Cobinhood
    open Rationals
    open CryptoApi.Data
    open CryptoApi.Debug

    let UpdateOrderBook (marketPair: string) (payload: OrderBookUpdate) =
        let market = CobinhoodCache.GetMarket(marketPair)
        let book = market.Book

        let bids = payload.Bids
        let asks = payload.Asks

        for bid in bids do
            let price = bid.Price
            let amount = bid.AmountAtPrice

            let keyExists = book.Bids.ContainsKey(price)
            let oldAmount = if keyExists then book.Bids.[price] else Rational.Zero


            let value = if payload.IsSnapshot then amount else oldAmount + amount

            if value.Equals(0)
            then book.Bids.Remove(price) |> ignore
            else book.Bids.[price] <- value

        for ask in asks do
            let price = ask.Price
            let amount = ask.AmountAtPrice

            let keyExists = book.Asks.ContainsKey(price)
            let oldAmount = if keyExists then book.Asks.[price] else Rational.Zero


            let value = if payload.IsSnapshot then amount else oldAmount + amount

            if value.Equals(0)
            then book.Asks.Remove(price) |> ignore
            else book.Asks.[price] <- value

        ()

    let HandleOrderBookUpdate(socketMessage: string, pair: string, hook: DidReceiveOrderBookHook) =
        let orderBookMessage =
            socketMessage
            |> WebSocketV2.OrderBook.Parse
            |> WebSocket.OrderBook.ExtractOrderBookMessage

        orderBookMessage |> UpdateOrderBook pair

        let market = CobinhoodCache.GetMarket(pair)

        match hook with
        | Some fn -> fn (orderBookMessage, market)
        | None -> ()

    let HandleTradeUpdate(socketMessage: string, pair: string, hook: DidReceiveTradeHook) =
        let tradeUpdates =
            socketMessage
            |> WebSocketV2.Trade.Parse
            |> WebSocket.Trade.ExtractTrodeUpdateMessages(pair)

        match hook with
        | Some fn -> fn tradeUpdates
        | None -> ()


    let HandleOrderUpdate(socketMessage: string) =
        socketMessage
        |> WebSocketV2.Order.Parse
        |> ignore


    let HandleTickerUpdate(socketMessage: string, symbol: string, hook: DidReceiveTickerHook) =
        let tickerUpdate =
            socketMessage
            |> WebSocketV2.Ticker.Parse
            |> WebSocket.Ticker.ExtractTickerUpdateMessages symbol

        match hook with
        | Some fn -> fn tickerUpdate
        | None -> ()

    let HandleCandleUpdate(socketMessage: string, symbol: string, hook: DidReceiveCandleHook) =
        let candleUpdate =
            socketMessage
            |> WebSocketV2.Candle.Parse
            |> WebSocket.Candle.ExtractCandleUpdate symbol

        match hook with
        | Some fn -> fn candleUpdate
        | None -> ()
