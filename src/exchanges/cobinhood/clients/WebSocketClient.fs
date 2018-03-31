namespace CryptoApi.Exchanges.Cobinhood

open CryptoApi.BaseExchange.Client
open System
open PureWebSockets
open System.Net.WebSockets

type WebSocketClient() =
    inherit AbstractWebSocketClient("wss://feed.cobinhood.com/ws")

    let minReconnectIntervalMs = 20 * 60 * 1000 // 20 Minutes

    let mutable client: PureWebSocket = null

    member __.OnClose (reason: WebSocketCloseStatus): unit =
        reason
        |> printfn "%A"

    member __.OnMessage (value: string): unit =
        value |> printfn "%A"


    member __.OnSendFailed (data: string, ex: Exception): unit =
        data
        |> printfn "%A"


    member __.Send (payload: string) =
        client.Send payload


    override __.Connect =
        let reconnect = new ReconnectStrategy(10000)

        client <- new PureWebSocket(__.url, reconnect)


        client.add_OnClosed (fun x -> __.OnClose(x) )

        client.add_OnMessage (fun x -> __.OnMessage(x) )
        client.add_OnSendFailed (fun (x, e) -> __.OnSendFailed(x, e) )

        client.Connect()
        |> ignore

        client


