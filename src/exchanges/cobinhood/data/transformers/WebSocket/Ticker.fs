namespace CryptoApi.Exchanges.Cobinhood.Data.Transformers.WebSocket

module Ticker =
    open CryptoApi.Exchanges.Cobinhood.Data.Providers
    open CryptoApi.Data
    open Rationals


    let ExtractTickerUpdateMessage (symbol: string) (data: string[]): TickerUpdate =
        {
           Symbol = symbol
           Timestamp = data.[0]
           HighestBid = Rational.ParseDecimal data.[1]
           LowestAsk = Rational.ParseDecimal data.[2]
           Volume24H = Rational.ParseDecimal data.[3]
           High24H = Rational.ParseDecimal data.[4]
           Low24H = Rational.ParseDecimal data.[5]
           Open24H = Rational.ParseDecimal data.[6]
           LastTradePrice = Rational.ParseDecimal data.[7]
        }

    let ExtractTickerUpdateMessages (symbol: string)  (payload: WebSocketV2.Ticker.Root): TickerUpdate =
        payload.D
        |> ExtractTickerUpdateMessage symbol

