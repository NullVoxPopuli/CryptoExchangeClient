namespace CryptoApi.Data

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

type DidReceiveOrderBookHook =
    Option<((OrderBookUpdate * Market) -> unit)>




type TradeUpdate = {
    Symbol: string
    TradeId: string
    Timestamp: string
    Price: Rational
    Size: Rational
    MakerSide: string
}

type DidReceiveTradeHook = Option<(TradeUpdate[] -> unit)>
