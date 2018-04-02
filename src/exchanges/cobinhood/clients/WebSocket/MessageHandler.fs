namespace CryptoApi.Exchanges.Cobinhood.WebSocket


exception UnknownChannelType of string
exception UnknownMessageType of string

module MessageHandler =
    open CryptoApi.Exchanges.Cobinhood.Data.Providers
    open CryptoApi.Exchanges.Cobinhood
    open Rationals
    open CryptoApi.Data

    let UpdateOrderBook (marketPair: string) (payload: OrderBookUpdate) =
        let market = CobinhoodCache.GetMarket(marketPair)
        let book = market.Book

        let bids = payload.Bids
        let asks = payload.Asks

      //const { price, count } = ask;
      //const oldCount = this.asks[ask.price];

      //const newValue = new Decimal(count || 0);
      //const value = isSnapshot ? newValue : (oldCount || zero).plus(newValue);

      //this.asks[ask.price] = value;

        for bid in bids do
            let price = bid.Price
            let count = bid.AmountAtPrice

            let keyExists = book.Bids.ContainsKey(price)
            let oldAmount = if keyExists then book.Bids.[price] else Rational.Zero


            let newValue = Rational.Parse(count)
            let value = if payload.IsSnapshot then newValue else oldAmount + newValue

            book.Bids.[price] = value

        for ask in asks do


        printfn "%A" marketPair

    let HandleMessage (value: string) =
        let payload = value |> WebSocketV2.Payload.Parse
        let channelName = payload.H.[0]
        let messageType = payload.H.[1]



        match channelName with
        | _ -> raise (UnknownChannelType(value))
        | "order" ->
            value
            |> WebSocketV2.Order.Parse
            |> ignore
        | WebSocketV2.IsOrderBook (_, pair, _precision) ->
            value
            |> WebSocketV2.OrderBook.Parse
            |> UpdateOrderBook pair
        | WebSocketV2.IsTrade (_, pair) ->
            value
            |> WebSocketV2.Trade.Parse
            |> ignore
        | WebSocketV2.IsTicker (_, pair) ->
            value
            |> WebSocketV2.Ticker.Parse
            |> ignore
        | "" ->
            match messageType with
            | _ -> raise (UnknownMessageType(value))
            | "pong" -> ()
            | "error" -> ()
