namespace CryptoApi.BaseExchange.Client

open System.Collections.Generic

open CryptoApi.Data
open SimpleJson
open System
open WebsocketClientLite.PCL

// All Rest Clients should extend this class
[<AbstractClass>]
type public AbstractWebSocketClient(url: string) =
    member __.url = url

    //abstract member Connect : MessageWebSocketRx
