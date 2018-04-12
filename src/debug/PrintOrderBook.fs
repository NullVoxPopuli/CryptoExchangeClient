namespace CryptoApi.Debug

module PrintOrderBook =
    open CryptoApi.Data
    open System.Collections.Generic
    open Rationals

    let KeysToArray (dict: Dictionary<'a,'b>) =
        dict.Keys
        |> Set.ofSeq
        |> Array.ofSeq

    let PrintOrderBook(market: Market): unit =
        let bids = market.Book.Bids
        let asks = market.Book.Asks

        let sort = KeysToArray >> Array.sortDescending

        let sortedAskPrices = asks |> sort
        let sortedBidPrices = bids |> sort

        let last10Asks = Array.sub sortedAskPrices (sortedAskPrices.Length - 10) 10
        let first10Bids = Array.sub sortedBidPrices 0 10

        printfn "\n\n\t============ %10s        =============\n" market.Symbol
        printfn "\t\tAsks: %d, Bids: %d" asks.Count bids.Count
        printfn "%20s %20s" "Amount" "Price"


        for askPrice in last10Asks do
            let amount = ((double) asks.[askPrice])
            let baseCur = market.BaseCurrency
            let quoteCur = market.QuoteCurrency

            printfn ("%16f %5s  @ %16s %5s") amount baseCur askPrice quoteCur

        printfn "\n%35s\n" "▲     asks     ▲"


        let bestAsk = last10Asks |> Array.last |> Rational.ParseDecimal
        let bestBid = first10Bids.[0] |> Rational.ParseDecimal

        printfn "\n%30s %11M\n" "Spread:" ((decimal) (bestAsk - bestBid))

        printfn "\n%35s\n" "▼     bids     ▼"

        for bidPrice in first10Bids do
            let amount = ((double) bids.[bidPrice])
            let baseCur = market.BaseCurrency
            let quoteCur = market.QuoteCurrency

            printfn ("%16f %5s  @ %16s %5s") amount baseCur bidPrice quoteCur

    let ToConsole(market: Market): unit =
        let bids = market.Book.Bids
        let asks = market.Book.Asks

        let isOrderBookPopulated = (asks.Count > 0) && (bids.Count > 0)

        match isOrderBookPopulated with
        | true -> PrintOrderBook(market)
        | false -> ()
