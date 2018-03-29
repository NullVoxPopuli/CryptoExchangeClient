namespace Examples

//#if INTERACTIVE
//#load "./InteractiveUtils.fsx"
//#endif
open CryptoApi.Exchanges
open Examples.InteractiveUtils

module CobinhoodDemo =

    let client = Cobinhood.RestClient()

    let SystemTime () = client.GetSystemTime().Result.Time |> printfn "Time: %i"
    let SystemInfo () = client.GetSystemInfo().Result.Info |> printfn "%A"
    let Candles () = client.GetCandles("ETH-BTC").Result.Candles |> printfn "%A"
    let Markets () = client.GetMarkets |> printfn "%A"
    let Currencies () = client.GetCurrencies |> printfn "%A"
    let OrderBook () = client.GetOrderBook("ETH-BTC").Result.Orderbook |> printfn "%A"
    let OrderBookPrecision () = client.GetOrderBookPrecisions("ETH-BTC").Result |> printfn "%A"

        


    let optionMap = [
        ("1", "GET system/time", SystemTime);
        ("2", "GET system/info", SystemInfo);
        ("3", "GET chart/candles/ETH-BTC", Candles);
        ("4", "GET market/trading_pairs", Markets);
        ("5", "GET market/currencies", Currencies);
        ("6", "GET market/orderbooks/ETH-BTC", OrderBook);
        ("7", "GET market/orderbook/precisions/ETH-BTC", OrderBookPrecision);

    ]


    let BeginDemo =
        PromptMenu ("The Cobinhood API -- examples", optionMap)

//#if INTERACTIVE
//#else
//CobinhoodDemo.BeginDemo
//#endif


