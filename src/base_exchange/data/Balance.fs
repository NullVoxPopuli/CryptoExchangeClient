namespace CryptoApi.Data


open Rationals

type public Balance = {
    currency: string;
    total: Rational;
    available: Rational;
    inOrders: Rational;

    usdValue: Rational;
    btcValue: Rational;
}
