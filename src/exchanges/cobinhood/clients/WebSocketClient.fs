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
open CryptoApi.Exchanges.Cobinhood.Data.Providers
open CryptoApi.Exchanges.Cobinhood.Data.Transformers

exception UnknownChannelType of string
exception UnknownMessageType of string

type WebSocketClient() =
    inherit AbstractWebSocketClient("wss://ws.cobinhood.com/v2/ws")

    member __.OnMessage (value: string): unit =
        let payload = value |> WebSocketV2.Payload.Parse
        let channelName = payload.H.[0]
        let messageType = payload.H.[2]


        match messageType with
        // these are likely going to be caught by the above matches
        | "subscribed" -> ()
        | "unsubscribed" -> ()
        | "pong" -> ()

        // other messages
        | "error" ->
            payload
            |> printfn "message error: %A"
            ()
        | _ -> // raise (UnknownMessageType(value))
            match channelName with
            | "order" -> MessageHandler.HandleOrderUpdate(value)
            | WebSocketV2.IsOrderBook (_, pair, _precision) -> MessageHandler.HandleOrderBookUpdate(value, pair, __.didReceiveOrderBook)
            | WebSocketV2.IsTrade (_, pair) -> MessageHandler.HandleTradeUpdate(value, pair, __.didReceiveTrade)
            | WebSocketV2.IsTicker (_, pair) -> MessageHandler.HandleTickerUpdate(value, pair, __.didReceiveTicker)
            | WebSocketV2.IsCandle (_, pair) -> MessageHandler.HandleCandleUpdate(value, pair, __.didReceiveCandle)
            | _ -> raise (UnknownMessageType(value))

    member __.SubscribeTo (socketParams: SocketOrderBookParams): Async<unit> = async {
        {
            action = Action.Subscribe |> ActionToString
            channelType = socketParams.channel |> TypeToString
            tradingPairId = socketParams.symbol
            precision = socketParams.precision
        }
        |> Json.serialize
        |> __.Send
    }

    member __.SubscribeTo (socketParams: SocketTradeParams): Async<unit> = async {
        {
            action = Action.Subscribe |> ActionToString
            channelType = socketParams.channel |> TypeToString
            tradingPairId = socketParams.symbol
        }
        |> Json.serialize
        |> __.Send
    }

    member __.SubscribeTo (socketParams: SocketCandleParams): Async<unit> = async {
        {
            action = Action.Subscribe |> ActionToString;
            channelType = socketParams.channel |> TypeToString;
            tradingPairId = socketParams.symbol
            timeframe = socketParams.timeframe
        }
        |> Json.serialize
        |> __.Send
    }


    member __.Connect (tokenSource: CancellationTokenSource): Async<unit> = async {
        __.Client <- new MessageWebSocketRx()
        __.cancelTokenSource <- tokenSource

        let headers = dict [
                        "Pragma", "no-cache"
                        "Cache-Control", "no-cache" ]

        let! messageObserver =
            __.Client.CreateObservableMessageReceiver(
                new Uri(__.url),
                headers = headers,
                subProtocols = null, // ex: "soap", "json"
                ignoreServerCertificateErrors = true,
                excludeZeroApplicationDataInPong = true
            ) |> Async.AwaitTask

        __.clientMessageObserver <- messageObserver

        __.Client.ObserveConnectionStatus.Subscribe(__.onNextStatus, __.onError, __.onCompleted)
        |> ignore

        messageObserver.Subscribe(__.OnMessage, __.onReceiveError, __.onSubscriptionComplete)
        |> ignore


    }


