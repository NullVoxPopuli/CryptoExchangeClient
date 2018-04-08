namespace CryptoApi.Exchanges.Cobinhood.Data.Transformers.WebSocket

//module Order =
    //open CryptoApi.BaseExchange.Client
    //open CryptoApi.Exchanges.Cobinhood.Data.Providers
    //open Rationals
    //open CryptoApi.Data

    //let ExtractOrderUpdateMessage (symbol: string) (data: string[]): TradeUpdate =
    //    {
    //        Symbol = symbol
    //        TradeId = data.[0]
    //        Timestamp = data.[1]
    //        MakerSide = data.[2]
    //        Price = Rational.ParseDecimal data.[3]
    //        Size = Rational.ParseDecimal data.[4]
    //    }



    //let ExtractOrderMessages (symbol: string) (payload: WebSocketV2.Trade.Root): TradeUpdate[] =
    //    payload.D
    //    |> Array.map (ExtractTradeUpdateMessage symbol)


