namespace CryptoApi.BaseExchange.Client

open System.Collections.Generic

open CryptoApi.Data
open RestSharp
open FSharp.Data

open CryptoApi.BaseExchange.Client.Parameters

// All Rest Clients should extend this class
[<AbstractClass>]
type public AbstractRestClient(baseUrl: string) =
    member __.baseUrl = baseUrl

    member __.MarketPairsBySymbol = new Dictionary<string, Market>()

    // network requests
    abstract member GetMarkets: Market[]
    abstract member GetOrders: Order[]
    abstract member GetBalances: Balance[]

    // generic REST things
    abstract member MakeRequest: path: string * method: Method * ?query: (string * string) list * ?body: JsonableParameters -> string


    // query local cache
    member __.MarketForSymbol symbol: Market = 
        __.MarketPairsBySymbol.GetValueOrDefault(symbol)

    abstract member Get: string -> string
    default __.Get url =
        __.MakeRequest(url, Method.GET)

    abstract member Post: string * JsonableParameters -> string
    default __.Post (url, body) =
        __.MakeRequest(url, Method.POST, [], body)

    abstract member Put: string * JsonableParameters-> string
    default __.Put (url, body) =
        __.MakeRequest(url, Method.PUT, [], body)

    abstract member Destroy: string-> string
    default __.Destroy url =
        __.MakeRequest(url, Method.DELETE)
