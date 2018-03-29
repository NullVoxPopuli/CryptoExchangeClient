namespace CryptoApi.Exchanges.Cobinhood.Data.Transformers

open Rationals

open CryptoApi.Data
open CryptoApi.Exchanges.Cobinhood.Data.Providers.Wallet


module Wallets =
    let ExtractBalance (balance: BalancesResponse.Balancis): Balance =
        let total = Rational.Approximate(balance.Total);
        let onOrder = Rational.Approximate(balance.OnOrder);

        

        let available = total - onOrder;
        
        let result = {
            currency = balance.Currency;
            total = total;
            available = available;
            inOrders = onOrder;

            usdValue = Rational.Approximate(balance.UsdValue);
            btcValue = Rational.Approximate(balance.BtcValue);
        }

        result

    let ExtractBalances (payload: BalancesResponse.Root): Balance[] =
        payload.Result.Balances
        |> Array.map ExtractBalance

