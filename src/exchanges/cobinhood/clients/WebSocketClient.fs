namespace CryptoApi.Exchanges.Cobinhood

open System
open System.Net.WebSockets
open System.Threading
open FSharp.Json
open WebsocketClientLite.PCL

open CryptoApi.AsyncUtils
open CryptoApi.BaseExchange.Client
open CryptoApi.Exchanges.Cobinhood.Parameters.SocketParams
open CryptoApi.Exchanges.Cobinhood.WebSocket
open IWebsocketClientLite.PCL

exception UnknownChannelType of string
exception UnknownMessageType of string


type WebSocketClient() =
    inherit AbstractWebSocketClient("wss://ws.cobinhood.com/v2/ws")

    let minReconnectIntervalMs = 20 * 60 * 1000 // 20 Minutes
    let pingInterval = 10 * 1000 // 30 seconds

    let mutable client: MessageWebSocketRx = null
    let mutable clientMessageObserver: IObservable<string> = null
    let mutable cancelTokenSource: CancellationTokenSource = null;

    member __.GetClient = client

    member __.Send (payload: string) =
        client.SendTextAsync payload
        |> Async.AwaitTask
        |> Async.RunSynchronously  // maybe? do we care if this block?

    member __.Disconnect () =
        printfn "Disconnecting...."
        cancelTokenSource.Cancel()

    member __.PingPonger () = __.Send """{"action":"ping"}"""



    member __.OnMessage (value: string): unit =
        MessageHandler.HandleMessage value

    member __.OnOpen () =
        RunPeriodically (__.PingPonger, pingInterval, cancelTokenSource.Token)


    member __.SubscribeTo (channel: ChannelType, symbol: string, precision: string): Async<unit> = async {
        let data: SubscribeToOrderBook = {
            action = Action.Subscribe |> ActionToString;
            channelType = channel |> TypeToString;
            tradingPairId = symbol;
            precision = precision;
        }

        data
        |> Json.serialize
        |> __.Send
    }



    member __.Connect (tokenSource: CancellationTokenSource): Async<unit> = async {
        client <- new MessageWebSocketRx()
        cancelTokenSource <- tokenSource

        let headers = dict [
                        "Pragma", "no-cache"
                        "Cache-Control", "no-cache" ]

        let onNextStatus (status: ConnectionStatus) =
            if status.Equals ConnectionStatus.Disconnected
                || status.Equals ConnectionStatus.Aborted
                || status.Equals ConnectionStatus.ConnectionFailed
            then
                __.Disconnect()

            if status.Equals ConnectionStatus.Connected
            then __.OnOpen()

        let onError (ex: Exception) =
            printfn "Connection Error"
            __.Disconnect()

        let onCompleted () =
            printfn "Connection Complete"
            __.Disconnect()

        let onReceiveError (ex: Exception) =
            printfn "receive error: %A" ex
            __.Disconnect()

        let onSubscriptionComplete () =
            printfn "Subscription Completed"
            __.Disconnect()


        let! messageObserver =
            client.CreateObservableMessageReceiver(
                new Uri(__.url),
                headers = headers,
                subProtocols = null, // ex: "soap", "json"
                ignoreServerCertificateErrors = true,
                excludeZeroApplicationDataInPong = true
            ) |> Async.AwaitTask

        clientMessageObserver <- messageObserver


        client.ObserveConnectionStatus.Subscribe(onNextStatus, onError, onCompleted)
        |> ignore

        messageObserver.Subscribe(__.OnMessage, onReceiveError, onSubscriptionComplete)
        |> ignore


    }


