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

        PrintOrderBook.ToConsole(market)


    let HandleMessage (value: string) =
        let payload = value |> WebSocketV2.Payload.Parse
        let channelName = payload.H.[0]
        let messageType = payload.H.[2]



        match channelName with
        | "order" ->
            value
            |> WebSocketV2.Order.Parse
            |> ignore
        | WebSocketV2.IsOrderBook (_, pair, _precision) ->
            value
            |> WebSocketV2.OrderBook.Parse
            |> WebSocket.OrderBook.ExtractOrderBookMessage
            |> UpdateOrderBook pair
        | WebSocketV2.IsTrade (_, pair) ->
            value
            |> WebSocketV2.Trade.Parse
            |> ignore
        | WebSocketV2.IsTicker (_, pair) ->
            value
            |> WebSocketV2.Ticker.Parse
            |> ignore
        | _ ->
            match messageType with
            | "pong" -> ()
            | "error" ->
                payload
                |> printfn "message error: %A"
                ()
            | _ -> raise (UnknownMessageType(value))
