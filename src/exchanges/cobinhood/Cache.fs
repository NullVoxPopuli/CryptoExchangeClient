namespace CryptoApi.Exchanges.Cobinhood

open CryptoApi.Data

type CobinhoodCache() =
    // Cache for sharing data between the rest client
    // and the websocket client
    static let mutable Currencies: Currency[] = Array.empty<Currency>
    static let mutable KnownMarkets: Market[] = Array.empty<Market>

    static member SetCurrencies (currencies: Currency[]) =
        Currencies <- currencies

    static member SetMarkets (markets: Market[]) =
        KnownMarkets <- markets

    static member GetCurrencies = Currencies
    static member GetMarkets = KnownMarkets

    static member GetMarket (pair: string): Market =
        KnownMarkets
        |> Array.find (fun market -> market.Symbol.Equals(pair))

    static member GetCurrency (pair: string): Currency =
        Currencies
        |> Array.find (fun currency -> currency.Symbol.Equals(pair) )
