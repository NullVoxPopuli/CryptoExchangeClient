namespace CryptoApi.Exchanges.Cobinhood

open System
open PureWebSockets
open System.Net.WebSockets
open System.Threading
open FSharp.Json
open WebsocketClientLite.PCL
open ISocketLite.PCL.Model


open CryptoApi.AsyncUtils
open CryptoApi.BaseExchange.Client
open CryptoApi.Exchanges.Cobinhood.Parameters.SocketParams
open CryptoApi.Exchanges.Cobinhood.Data.Providers
open CryptoApi.Exchanges.Cobinhood.WebSocket
open System.Collections.Generic
open IWebsocketClientLite.PCL

exception UnknownChannelType of string
exception UnknownMessageType of string


type WebSocketClient() =
    inherit AbstractWebSocketClient("wss://ws.cobinhood.com/v2/ws")

    let minReconnectIntervalMs = 20 * 60 * 1000 // 20 Minutes
    let pingInterval = 60 * 1000 // 1 Minute

    let mutable client: MessageWebSocketRx = null
    let mutable clientMessageObserver: IObservable<string> = null
    let mutable cancelTokenSource: CancellationTokenSource = null;

    member __.Send (payload: string) =
        printfn "sending: %A" payload
        client.Send payload
    member __.PingPonger () = __.Send """{"action":"ping"}""" |> ignore

    member __.OnClose (reason: WebSocketCloseStatus): unit =
        reason
        |> printfn "%A"

    member __.OnMessage (value: string): unit =
        printfn "OnMessage: %A" value
        MessageHandler.HandleMessage value

    member __.OnOpen () =
        RunPeriodically (__.PingPonger, pingInterval, cancelTokenSource.Token)
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



    override __.Connect (tokenSource: CancellationTokenSource) =
        client <- new MessageWebSocketRx()
        cancelTokenSource <- tokenSource
        
        let headers = dict [
                        "Pragma", "no-cache"
                        "Cache-Control", "no-cache" ]
        async {
            let onNextStatus (status: ConnectionStatus) =
                printfn "Status Change"
                if status.Equals ConnectionStatus.Disconnected
                   || status.Equals ConnectionStatus.Aborted
                   || status.Equals ConnectionStatus.ConnectionFailed
                then
                    tokenSource.Cancel()

            let onError (ex: Exception) =
                printfn "Connection Error"
                tokenSource.Cancel()
            let onCompleted () =
                printfn "Connection Complete"
                tokenSource.Cancel()
            let onMessage (msg: string) =
                printfn "Received Message"
                printfn "%A" msg
            let onReceiveError (ex: Exception) =
                printfn "%A" ex
                tokenSource.Cancel()
            let onSubscriptionComplete () =
                printfn "Subscription Completed"
                tokenSource.Cancel()

            
            let! messageObserver =
                client.CreateObservableMessageReceiver(
                    new Uri(__.url),
                    headers = headers,
                    subProtocols = null, // ex: "soap", "json"
                    ignoreServerCertificateErrors = true,
                    tlsProtocolType = TlsProtocolVersion.Tls12
                ) |> Async.AwaitTask

            clientMessageObserver <- messageObserver


            client.ObserveConnectionStatus.Subscribe(onNextStatus, onError, onCompleted)
            |> ignore

            messageObserver.Subscribe(onMessage, onReceiveError, onSubscriptionComplete)
            |> ignore

            
        }


