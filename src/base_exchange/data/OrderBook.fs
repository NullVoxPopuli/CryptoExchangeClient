namespace CryptoApi.Data

open System.Collections.Generic
open Rationals

type OrderBookEntry = {
    Price: string
    AmountAtPrice: Rational
}

type OrderBookUpdate = {
    IsSnapshot: bool
    Bids: OrderBookEntry[]
    Asks: OrderBookEntry[]
}

type public OrderBook = {
    Bids: Dictionary<string, Rational>
    Asks: Dictionary<string, Rational>
}
