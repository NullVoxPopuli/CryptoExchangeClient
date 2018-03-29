namespace CryptoApi.BaseExchange.Client

open System.Collections.Generic

open CryptoApi.Data
open SimpleJson
open System

type public IWebSocketClient =
    abstract member connect: unit -> unit

// All Rest Clients should extend this class
[<AbstractClass>]
type public AbstractWebSocketClient(baseUrl: string) =
    member __.connect = raise (NotImplementedException())