namespace CryptoApi.Exchanges.Cobinhood

open System

open FSharp.Data
open FSharp.Data.HttpRequestHeaders

open CryptoApi.Data

open CryptoApi.BaseExchange.Client
open CryptoApi.Exchanges.Cobinhood.Parameters
open CryptoApi.Exchanges.Cobinhood.Data.Providers.System
open CryptoApi.Exchanges.Cobinhood.Data.Providers.Market
open CryptoApi.Exchanges.Cobinhood.Data.Providers.Chart
open CryptoApi.Exchanges.Cobinhood.Data.Providers.Wallet
open CryptoApi.Exchanges.Cobinhood.Data.Providers.Trading

open CryptoApi.Exchanges.Cobinhood.Data.Transformers.Currencies
open CryptoApi.Exchanges.Cobinhood.Data.Transformers.Markets
open CryptoApi.Exchanges.Cobinhood.Data.Transformers.Orders
open CryptoApi.Exchanges.Cobinhood.Data.Transformers.Wallets
open CryptoApi.BaseExchange.Client.Parameters


type RestClient(?apiKey: string) =
    inherit AbstractRestClient("https://api.cobinhood.com/v1/")

    let ApiKey = match apiKey with
                 | Some a -> a
                 | None -> ""

    let nonceIncrement = int64 1
    let mutable lastNonce: int64 = int64 0

    ////////////////////////////
    // System
    ////////////////////////////
    member __.GetSystemTime() = __.Get "system/time" |> SystemTimeResponse.Parse
    member __.GetSystemInfo() = __.Get "system/info" |> SystemInfoResponse.Parse

    ////////////////////////////
    // Charts
    ////////////////////////////
    member __.GetCandles (tradingPairId : string) =
        __.Get ("chart/candles/" + tradingPairId)
        |> CandlesResponse.Parse

    ////////////////////////////
    // Market
    ////////////////////////////
    override __.GetMarkets =
        let markets = __.Get "market/trading_pairs"
                        |> TradingPairsResponse.Parse
                        |> ExtractMarkets

        CobinhoodCache.SetMarkets markets

        markets

    member __.GetCurrencies =
        let currencies = __.Get "market/currencies"
                         |> CurrenciesResponse.Parse
                         |> ExtractCurrencies

        CobinhoodCache.SetCurrencies currencies

        currencies

    member __.GetOrderBook (tradingPairId: string) =
        __.Get ("market/orderbooks/" + tradingPairId)
        |> OrderBookResponse.Parse

    member __.GetOrderBookPrecisions (tradingPairId: string) =
        __.Get ("market/orderbook/precisions/" + tradingPairId)
        |> OrderBookPrecisions.Parse

    member __.GetTradingStatistics =
        __.Get "market/stats"
        |> TradingStatsResponse.Parse


    member __.GetTicker (tradingPairId: string) =
        __.Get ("market/tickers/" + tradingPairId)
        |> TickerResponse.Parse

    member __.GetRecentTrades (tradingPairId: string) =
        __.Get ("market/trades/" + tradingPairId)
        |> RecentTradesResponse.Parse

    ////////////////////////////
    // Trading (Auth)
    ////////////////////////////
    override __.GetOrders =
        __.Get "trading/orders"
        |> GetAllOrdersResponse.Parse
        |> ExtractOrders(CobinhoodCache.GetMarkets)

    member __.GetOrder (id: string) =
        __.Get ("trading/orders/" + id)
        |> GetOrderResponse.Parse
        |> ExtractOrder(CobinhoodCache.GetMarkets)

    member __.GetTrades (id: string) =
        __.Get ("trading/orders/" + id + "/trades")
        |> GetOrderTradesResponse.Parse

    member __.PlaceOrder (body: RestParams.PlaceOrder) =
        __.Post ("trading/orders", body)
        |> PlaceOrderResponse.Parse
        |> ExtractOrder(CobinhoodCache.GetMarkets)

    member __.ModifyOrder (id: string) (body: RestParams.ModifyOrder) =
        __.Put ("trading/orders/" + id, body)
        |> ModifyOrderResponse.Parse

    member __.CancelOrder (id: string) =
        __.Destroy ("trading/orders/" + id)
        |> CancelOrderResponse.Parse

    member __.GetOrderHistory (?tradingPair: string, ?limit: int) =
        __.Get "trading/order_history"
        |> OrderHistoryResponse.Parse

    member __.GetTrade (id: string) =
        __.Get ("trading/trades/" + id)
        |> TradeResponse.Parse

    member __.GetTradeHistory (?tradingPair: string, ?limit: int) =
        __.Get "trading/trades"
        |> TradeHistoryResponse.Parse


    ////////////////////////////
    // Wallet (Auth)
    ////////////////////////////
    override __.GetBalances =
        __.Get "wallet/balances"
        |> BalancesResponse.Parse
        |> ExtractBalances

    member __.GetLedgerHistory =
        __.Get "wallet/ledger"
        |> LedgerHistoriesResponse.Parse

    member __.GetDepositAddresses (?currency: string) =
        let url = match currency with
                    | Some cur -> "wallet/deposit_addresses?currency=" + cur
                    | None -> "wallet/deposit_addresses"

        __.Get url
        |> DepositAddressResponse.Parse


    member __.GetWithdrawalAddresses (?currency: string) =
        let url = match currency with
                    | Some cur -> "wallet/withdrawal_addresses?currency=" + cur
                    | None -> "wallet/withdrawal_addresses"

        __.Get url
        |> WithdrawalAddressResponse.Parse


    member __.GetWithdrawal (id: string) =
        __.Get ("wallet/withdrawals/" + id)
        |> WithdrawalResponse.Parse

    member __.GetAllWithdrawals (?id: string) =
        __.Get "wallet/withdrawals"
        |> AllWithdrawalsResponse.Parse


    member __.GetDeposit (id: string) =
        __.Get ("wallet/deposits/" + id)
        |> DepositResponse.Parse

    member __.GetAllDeposits (?id: string) =
        __.Get "wallet/deposits"
        |> AllDepositsResponse.Parse

    //type Body =
    //        | JsonableParameters
    //        | None

    ////////////////////////////////
    // HTTP / How to make a request
    ////////////////////////////////
    member __.GetNonce =
        let previousNonce = lastNonce

        lastNonce <- DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()

        if lastNonce <= previousNonce
        then lastNonce <- lastNonce + nonceIncrement

        lastNonce



    // TODO: determine offset automatically
    //       for when client machine's time is out of sync with server
    override __.MakeRequest (path, method, ?query: (string * string) list, ?body: JsonableParameters) =
        let url = __.baseUrl + path

        let q = match query with
                | Some queryParams -> queryParams
                | None -> []

        let nonce = __.GetNonce

        let headers = [
            Accept HttpContentTypes.Json;
            ContentType HttpContentTypes.Json;
            "authorization", ApiKey;
            "nonce", nonce.ToString();
        ]

        let result =
            match body with
            | Some b ->
                let bodyString = b.ToString

                Http.RequestString (url,
                                    query = q,
                                    body = TextRequest bodyString,
                                    headers = headers,
                                    httpMethod = method.ToString())
            | None -> Http.RequestString (url,
                                          query = q,
                                          headers = headers,
                                          httpMethod = method.ToString())

        result
