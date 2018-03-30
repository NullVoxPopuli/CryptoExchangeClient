namespace Examples

//#if INTERACTIVE
//#load "./InteractiveUtils.fsx"
//#endif
open CryptoApi.Exchanges
open Examples.InteractiveUtils

module CobinhoodDemo =

    let client = Cobinhood.RestClient()

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
    let GetOrder () = client.GetOrder("37f550a202aa6a3fe120f420637c894c") |> printfn "%A"
    let GetTrades () = client.GetTrades("37f550a202aa6a3fe120f420637c894c") |> printfn "%A"

    let GetOrderHistory () = client.GetOrderHistory() |> printfn "%A"
    let GetTrade () = client.GetTrade("09619448-e48a-3bd7-3d49-3a4194f9020b") |> printfn "%A"
    let GetTradeHistory () = client.GetTradeHistory() |> printfn "%A"
    

    let optionMap = [
        ("", "Public Endpoints", (fun () -> ()) );
        ("", "", (fun () -> ()) );
        
        ("1", "GET system/time", SystemTime);
        ("2", "GET system/info", SystemInfo);
        ("3", "GET chart/candles/ETH-BTC", Candles);
        ("4", "GET market/trading_pairs", Markets);
        ("5", "GET market/currencies", Currencies);
        ("6", "GET market/orderbooks/ETH-BTC", OrderBook);
        ("7", "GET market/orderbook/precisions/ETH-BTC", OrderBookPrecision);
        ("8", "GET market/stats", MarketStats);
        ("9", "GET market/tickers/ETH-BTC", Ticker);
        ("10", "GET market/trades/ETH-BTC", Trades);
        
        ("", "", (fun () -> ()) );
        ("", "Auth Endpoints", (fun () -> ()) );
        ("", "", (fun () -> ()) );

        ("11", "GET trading/orders", GetOrders);
        ("12", "GET trading/orders/<id>", GetOrder);
        ("13", "GET trading/orders/<id>/trades", GetTrades);
        // ("14", "POST trading/orders", PostOrders);
        // ("15", "PUT trading/orders/<id>", PutOrder);
        // ("16", "DELETE trading/orders/<id>", CancelOrder);
        ("17", "GET trading/order_history", GetOrderHistory);
        ("18", "GET trading/trades/<id>", GetTrade);
        ("19", "GET trading/trades", GetTradeHistory);
    ]


    let BeginDemo =
        PromptMenu ("The Cobinhood API -- examples", optionMap)

//#if INTERACTIVE
//#else
//CobinhoodDemo.BeginDemo
//#endif


