namespace CryptoApi

open Rationals

module NumberUtils =
    let DecimalToRational (num: decimal) =
        Rational.ParseDecimal(num.ToString())
