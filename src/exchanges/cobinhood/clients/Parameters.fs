namespace CryptoApi.Exchanges.Cobinhood.Parameters

open FSharp.Json
open Rationals

open CryptoApi.BaseExchange.Client.Parameters


type PlaceOrderParameters = 
    inherit JsonableParameters

    member __.tradingPairId: string = ""
    member __.side: string = ""
    member __.typeOfOrder: string = ""
    member __.price: Rational = Rational.Approximate 0.0
    member __.size: Rational = Rational.Approximate 0.0

    override __.ToString =
        Map.ofList [ 
            "trading_pair_id", __.tradingPairId
            "side", __.side
            "type", __.typeOfOrder
            "price", __.price.ToString()
            "size", __.size.ToString()
        ]
        |> Json.serialize


type ModifyOrderParameters =
    inherit JsonableParameters

    member __.price: Rational = Rational.Approximate 0.0
    member __.size: Rational = Rational.Approximate 0.0

    override __.ToString =
        Map.ofList [
            "price", __.price.ToString()
            "size", __.size.ToString()
        ]
        |> Json.serialize
