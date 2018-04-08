namespace CryptoApi.Exchanges.Cobinhood.Data.Transformers.WebSocket

module Candle =
    open CryptoApi.Exchanges.Cobinhood.Data.Providers
    open CryptoApi.Data
    open Rationals

    let ExtractCandleUpdateMessage (symbol: string) (data: string[]): CandleUpdate =
        {
            Symbol = symbol
            Timestamp = data.[0]
            Volume = Rational.ParseDecimal data.[1]
            High = Rational.ParseDecimal data.[2]
            Low = Rational.ParseDecimal data.[3]
            Open = Rational.ParseDecimal data.[4]
            Close = Rational.ParseDecimal data.[5]
        }

    let ExtractCandleUpdate (symbol: string) (payload: WebSocketV2.Candle.Root): CandleUpdate[] =
        payload.D
        |> Array.map (ExtractCandleUpdateMessage symbol)

