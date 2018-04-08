namespace CryptoApi.Data

open System.Collections.Generic
open Rationals

type public OrderBook = {
    Bids: Dictionary<string, Rational>
    Asks: Dictionary<string, Rational>
}
