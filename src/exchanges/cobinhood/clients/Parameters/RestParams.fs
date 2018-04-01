namespace CryptoApi.Exchanges.Cobinhood.Parameters

open FSharp.Json
open Rationals

open CryptoApi.BaseExchange.Client.Parameters

module RestParams =
    type PlaceOrder(id, side, typeOfOrder, price, size) =
        inherit JsonableParameters()

        member __.tradingPairId: string = id
        member __.side: string = side
        member __.typeOfOrder: string = typeOfOrder
        member __.price: string = price
        member __.size: string = size

        override __.ToString =
            Map.ofList [
                "trading_pair_id", __.tradingPairId
                "side", __.side
                "type", __.typeOfOrder
                "price", __.price
                "size", __.size
            ]
            |> Json.serialize


    type ModifyOrder(price, size) =
        inherit JsonableParameters()

        member __.price: string = price
        member __.size: string = size

        override __.ToString =
            Map.ofList [
                "price", __.price
                "size", __.size
            ]
            |> Json.serialize
