namespace CryptoApi.Exchanges.Cobinhood.Data.Transformers

open CryptoApi.Data

open CryptoApi.Exchanges.Cobinhood.Data.Providers.Market

module Markets =
    open System.Collections.Generic
    open Rationals

    let ExtractMarket (tradingPair: TradingPairsResponse.TradingPair): Market =
        let market = {
            Symbol = tradingPair.Id;
            BaseCurrency = tradingPair.BaseCurrencyId;
            QuoteCurrency = tradingPair.QuoteCurrencyId;

            Book =
            {
                Bids = new Dictionary<string, Rational>()
                Asks = new Dictionary<string, Rational>()
            };
        }

        market

    let ExtractMarkets (payload: TradingPairsResponse.Root): Market[] =
        payload.Result.TradingPairs
        |> Array.map ExtractMarket
