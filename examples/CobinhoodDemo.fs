namespace Examples

//#if INTERACTIVE
//#load "./InteractiveUtils.fsx"
//#endif
open CryptoApi.Exchanges
open Examples.InteractiveUtils

module CobinhoodDemo =
    open CryptoApi.Exchanges.Cobinhood.Parameters
    open CryptoApi.Exchanges.Cobinhood.Parameters.SocketParams
    open System.Threading
    open CryptoApi
    open CryptoApi.Debug

    let key = System.Environment.GetEnvironmentVariable("COBINHOOD_API_KEY")
    let client = Cobinhood.RestClient(key)

    let socket = Cobinhood.WebSocketClient()

    // Initial things to seed the cache
    ignore client.GetCurrencies
    ignore client.GetMarkets

    // Public Endpoints
    let SystemTime () = client.GetSystemTime().Result.Time |> printfn "Time: %i"
    let SystemInfo () = client.GetSystemInfo().Result.Info |> printfn "%A"
    let Candles () = client.GetCandles("ETH-BTC").Result.Candles |> printfn "%A"
    let Markets () = client.GetMarkets |> printfn "%A"
    let Currencies () = client.GetCurrencies |> printfn "%A"
    let OrderBook () = client.GetOrderBook("ETH-BTC").Result.Orderbook |> printfn "%A"
    let OrderBookPrecision () = client.GetOrderBookPrecisions("ETH-BTC").Result |> printfn "%A"
    let MarketStats () = client.GetTradingStatistics.Result |> printfn "%A"
    let Ticker () = client.GetTicker("ETH-BTC").Result |> printfn "%A"
    let Trades () = client.GetRecentTrades("ETH-BTC").Result |> printfn "%A"

    // Auth Endpoints
    let GetOrders () = client.GetOrders |> printfn "%A"
    let GetOrder () =
        PromptFor("Order ID: ")
        |> client.GetOrder
        |> printfn "%A"

    let GetTrades () =
        PromptFor("Order ID: ")
        |> client.GetTrades
        |> printfn "%A"

    // NOTE: this is a fairly safe buy/bid order to place. (at the time of writing this ETH-BTC is ~ 0.56)
    let PostOrders () =
        RestParams.PlaceOrder("ETH-BTC", "bid", "limit", "0.00001", "100")
        |> client.PlaceOrder
        |> printfn "%A"


    // NOTE: this may error, because the order id may not belong to you
    let PutOrder () =
        RestParams.ModifyOrder("0.00001", "90")
        |> client.ModifyOrder("37f550a202aa6a3fe120f420637c894c")
        |> printfn "%A"

    // NOTE: this may error, because the order id may not belong to you
    let CancelOrder () = client.CancelOrder("37f550a202aa6a3fe120f420637c894c") |> printfn "%A"

    let GetOrderHistory () = client.GetOrderHistory() |> printfn "%A"
    let GetTrade () = client.GetTrade("09619448-e48a-3bd7-3d49-3a4194f9020b") |> printfn "%A"
    let GetTradeHistory () = client.GetTradeHistory() |> printfn "%A"
    let GetBalances () = client.GetBalances |> printfn "%A"
    let GetLedgerHistory () = client.GetLedgerHistory |> printfn "%A"
    let GetDepositAddress () = client.GetDepositAddresses() |> printfn "%A"
    let GetWithdrawalAddress () = client.GetWithdrawalAddresses() |> printfn "%A"
    let GetWithdrawal () = client.GetWithdrawal("62056df2d4cf8fb9b15c7238b89a1438") |> printfn "%A"
    let GetAllWithdrawals () = client.GetAllWithdrawals() |> printfn "%A"
    let GetDeposit () = client.GetDeposit("62056df2d4cf8fb9b15c7238b89a1438") |> printfn "%A"
    let GetAllDEposits () = client.GetAllDeposits() |> printfn "%A"


    let SocketDemo (fn: CancellationTokenSource -> Async<unit>) =
        let token = new CancellationTokenSource()

        Async.Start (fn(token), token.Token)

        PromptToRun "\n\nType 'Y' followed by <enter> to cancel the websocket demo \n\n" (fun () -> token.Cancel() )

        ()

    let SocketTrades () =
        SocketDemo (fun (token) -> async {
            do! socket.Connect token
            do! socket.SubscribeTo({ channel = ChannelType.Trade; symbol = "COB-ETH" })

            socket.SetDidReceiveTrade (fun update -> printfn "%A" update)
        })

    let SocketPingPong () =
        SocketDemo (fun (token) -> async {
            do! socket.Connect token
        })

    let SocketOrderBook () =
        SocketDemo (fun (token) -> async {
            do! socket.Connect token
            // Precision could be retrieved from <#RestClient>.GetOrderBookPrecision
            do! socket.SubscribeTo({ channel = ChannelType.OrderBook; symbol = "COB-ETH"; precision = "1E-7" })

            socket.SetDidReceiveOrderBook (fun (update, market) ->  PrintOrderBook.ToConsole(market))
        })

    let SocketTicker () =
        SocketDemo (fun (token) -> async {
            do! socket.Connect token
            do! socket.SubscribeTo({ channel = ChannelType.Ticker; symbol = "COB-ETH" })

            socket.SetDidReceiveTicker (fun (update) -> printfn "%A" update)
        })

    let SocketCandle () =
        SocketDemo (fun (token) -> async {
            do! socket.Connect token
            do! socket.SubscribeTo({ channel = ChannelType.Candle; symbol = "COB-ETH"; timeframe = "30m" })

            socket.SetDidReceiveTicker (fun (update) -> printfn "%A" update)
        })

    let optionMap = [
        ("", "Public Endpoints", PlaceholderFn )
        Spacer

        ("1", "GET system/time", SystemTime)
        ("2", "GET system/info", SystemInfo)
        ("3", "GET chart/candles/ETH-BTC", Candles)
        ("4", "GET market/trading_pairs", Markets)
        ("5", "GET market/currencies", Currencies)
        ("6", "GET market/orderbooks/ETH-BTC", OrderBook)
        ("7", "GET market/orderbook/precisions/ETH-BTC", OrderBookPrecision)
        ("8", "GET market/stats", MarketStats)
        ("9", "GET market/tickers/ETH-BTC", Ticker)
        ("10", "GET market/trades/ETH-BTC", Trades)

        Spacer
        ("", "Auth Endpoints", PlaceholderFn )
        Spacer

        ("11", "GET trading/orders", GetOrders)
        ("12", "GET trading/orders/<id>", GetOrder)
        ("13", "GET trading/orders/<id>/trades", GetTrades)
        ("14", "POST trading/orders", PostOrders)
        ("15", "PUT trading/orders/<id>", PutOrder)
        ("16", "DELETE trading/orders/<id>", CancelOrder)
        ("17", "GET trading/order_history", GetOrderHistory)
        ("18", "GET trading/trades/<id>", GetTrade)
        ("19", "GET trading/trades", GetTradeHistory)
        ("20", "GET wallet/balances", GetBalances)
        ("21", "GET wallet/ledger", GetLedgerHistory)
        ("22", "GET wallet/deposit_addresses", GetDepositAddress)
        ("23", "GET wallet/withdrawal_addresses", GetWithdrawalAddress)
        ("24", "GET wallet/withdrawals/<id>", GetWithdrawal)
        ("25", "GET wallet/withdrawals", GetAllWithdrawals)
        ("26", "GET wallet/deposits/<id>", GetDeposit)
        ("27", "GET wallet/deposits", GetAllDEposits)

        Spacer
        ("", "WebSocket - Public Stream", PlaceholderFn )
        Spacer

        ("28", "ping/pong", SocketPingPong)
        ("29", "OrderBook for COB-ETH", SocketOrderBook)
        ("30", "Trades for COB-ETH", SocketTrades)
        ("31", "Ticker for COB-ETH", SocketTicker)
        ("32", "Candles for COB-ETH", SocketCandle)

        Spacer
        ("", "WebSocket - Auth", PlaceholderFn )
        Spacer
    ]


    let BeginDemo () =
        PromptMenu ("The Cobinhood API -- examples", optionMap)

//#if INTERACTIVE
//#else
//CobinhoodDemo.BeginDemo
//#endif


