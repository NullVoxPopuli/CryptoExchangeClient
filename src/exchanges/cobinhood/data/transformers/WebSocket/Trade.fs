namespace CryptoApi.Exchanges.Cobinhood.Data.Transformers.WebSocket

module Trade =
    open CryptoApi.BaseExchange.Client
    open CryptoApi.Exchanges.Cobinhood.Data.Providers
    open Rationals
    open CryptoApi.Data

    let ExtractTradeUpdateMessage (symbol: string) (data: string[]): TradeUpdate =
        {
            Symbol = symbol
            TradeId = data.[0]
            Timestamp = data.[1]
            MakerSide = data.[2]
            Price = Rational.ParseDecimal data.[3]
            Size = Rational.ParseDecimal data.[4]
        }



    let ExtractTrodeUpdateMessages (symbol: string) (payload: WebSocketV2.Trade.Root): TradeUpdate[] =
        payload.D
        |> Array.map (ExtractTradeUpdateMessage symbol)


