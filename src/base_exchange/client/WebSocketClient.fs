namespace CryptoApi.BaseExchange.Client

open System.Collections.Generic

open CryptoApi.Data
open SimpleJson
open System
open WebsocketClientLite.PCL
open System.Threading
open CryptoApi.AsyncUtils
open IWebsocketClientLite.PCL
open Rationals

// All Socket Clients should extend this class
[<AbstractClass>]
type public AbstractWebSocketClient(url: string) =
    // ---------
    // constants
    let pingInterval = 10 * 1000 // 10 seconds

    // ------------------
    // user-setable hooks
    member val didReceiveTicker: DidReceiveTickerHook = None with get, set
    member val didReceiveTrade: DidReceiveTradeHook = None with get, set
    member val didReceiveOrderBook: DidReceiveOrderBookHook = None with get, set
    member val didReceiveCandle: DidReceiveCandleHook = None with get, set

    member __.SetDidReceiveTicker (hook: (TickerUpdate -> unit)) =
       __.didReceiveTicker <- Some(hook)

    member __.SetDidReceiveTrade (hook: (TradeUpdate[] -> unit)) =
        __.didReceiveTrade <- Some(hook)

    member __.SetDidReceiveOrderBook (hook: ((OrderBookUpdate * Market) -> unit)) =
        __.didReceiveOrderBook <- Some(hook)

    member __.SetDidReceiveCandle (hook: (CandleUpdate[] -> unit)) =
        __.didReceiveCandle <- Some(hook)

    // 'public' fields
    member val Client: MessageWebSocketRx = null with get, set

    // 'protected' fields
    member val clientMessageObserver: IObservable<string> = null with get, set
    member val cancelTokenSource: CancellationTokenSource = null with get, set


    // -------
    // fields
    member __.url = url


    // -----------------------
    // local socket management

    // ------------------------------------------------
    // connection management and socket-client helpers
    member __.Send (payload: string) =
        __.Client.SendTextAsync payload
        |> Async.AwaitTask
        |> Async.RunSynchronously  // maybe? do we care if this blocks?

    member __.Disconnect () =
        printfn "Disconnecting...."
        __.cancelTokenSource.Cancel()


    member __.onNextStatus (status: ConnectionStatus) =
        let isDisconnected =
            status.Equals ConnectionStatus.Disconnected
            || status.Equals ConnectionStatus.Aborted
            || status.Equals ConnectionStatus.ConnectionFailed

        let hasBecomeOpen = status.Equals ConnectionStatus.Connected

        if isDisconnected then __.Disconnect()
        else if hasBecomeOpen then __.OnOpen()

    member __.onError (ex: Exception) =
        printfn "Connection Error"
        __.Disconnect()

    member __.onCompleted () =
        printfn "Connection Complete"
        __.Disconnect()

    member __.onReceiveError (ex: Exception) =
        printfn "receive error: %A" ex
        __.Disconnect()

    member __.onSubscriptionComplete () =
        printfn "Subscription Completed"
        __.Disconnect()


    member __.PingPonger () = __.Send """{"action":"ping"}"""

    member __.OnOpen () =
        RunPeriodically (__.PingPonger, pingInterval, __.cancelTokenSource.Token)
