namespace CryptoApi.Exchanges.Cobinhood.Data.Transformers

open CryptoApi.Data

open CryptoApi.Exchanges.Cobinhood.Data.Providers.Market
open Rationals

module Currencies =
    let ExtractCurrency (payload: CurrenciesResponse.Currency): Currency =
        let currency = {
            Symbol = payload.Currency;
            Name = payload.Name;
            MinUnit = Rational.Approximate(payload.MinUnit);
            DepositFee = Rational.Parse(payload.DepositFee.ToString());
            WithdrawalFee = Rational.Approximate(payload.WithdrawalFee);
            Type = payload.Type;
            IsActive = payload.IsActive;
            FundingFrozen = payload.FundingFrozen;
        }

        currency

    let ExtractCurrencies (payload: CurrenciesResponse.Root): Currency[] =
        payload.Result.Currencies
        |> Array.map ExtractCurrency
