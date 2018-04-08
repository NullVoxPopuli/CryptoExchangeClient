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


