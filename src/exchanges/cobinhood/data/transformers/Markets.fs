namespace CryptoApi.Exchanges.Cobinhood.Data.Transformers

open CryptoApi.Data

open CryptoApi.Exchanges.Cobinhood.Data.Providers.Market

module Markets =
    let ExtractMarket (tradingPair: TradingPairsResponse.TradingPair): Market =
        let market = {
            symbol = tradingPair.Id;
            baseCurrency = tradingPair.BaseCurrencyId;
            quoteCurrency = tradingPair.QuoteCurrencyId;

            book = 
            {
                bids = Array.empty<OrderBookEntry>;
                asks = Array.empty<OrderBookEntry>;
            };
        }

        market

    let ExtractMarkets (payload: TradingPairsResponse.Root): Market[] =
        payload.Result.TradingPairs
        |> Array.map ExtractMarket
