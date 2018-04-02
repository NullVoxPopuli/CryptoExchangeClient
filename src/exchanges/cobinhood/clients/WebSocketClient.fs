namespace CryptoApi.Exchanges.Cobinhood

open System
open PureWebSockets
open System.Net.WebSockets
open System.Threading
open FSharp.Json

open CryptoApi.AsyncUtils
open CryptoApi.BaseExchange.Client
open CryptoApi.Exchanges.Cobinhood.Parameters.SocketParams
open CryptoApi.Exchanges.Cobinhood.Data.Providers
open CryptoApi.Exchanges.Cobinhood.WebSocket

exception UnknownChannelType of string
exception UnknownMessageType of string


type WebSocketClient() =
    inherit AbstractWebSocketClient("wss://ws.cobinhood.com/v2/ws")

    let minReconnectIntervalMs = 20 * 60 * 1000 // 20 Minutes
    let pingInterval = 60 * 1000 // 1 Minute

    let mutable client: PureWebSocket = null
    let mutable pingCanceller = null;

    member __.Send (payload: string) = client.Send payload
    member __.PingPonger () = __.Send """{"action": "ping"}""" |> ignore

    member __.OnClose (reason: WebSocketCloseStatus): unit =
        reason
        |> printfn "%A"

    member __.OnMessage (value: string): unit =
        MessageHandler.HandleMessage value

    member __.OnOpen () =
        pingCanceller <- new CancellationTokenSource()

        RunPeriodically (__.PingPonger, pingInterval, pingCanceller.Token)
        |> ignore


    member __.SubscribeTo (channel: ChannelType, symbol: string, precision: string): unit =
        let data: SubscribeToOrderBook = {
            action = Action.Subscribe |> ActionToString;
            channelType = channel |> TypeToString;
            tradingPairId = symbol;
            precision = precision;
        }

        data
        |> Json.serialize
        |> client.Send
        |> ignore



    override __.Connect =
        let reconnect = new ReconnectStrategy(10000)

        client <- new PureWebSocket(__.url, reconnect)

        client.add_OnClosed (fun x -> __.OnClose(x) )
        client.add_OnOpened (fun () -> __.OnOpen() )

        client.add_OnMessage (fun x -> __.OnMessage(x) )
        //client.add_OnSendFailed (fun () -> __.OnSendFailed() )

        client.Connect()
        |> ignore

        client


