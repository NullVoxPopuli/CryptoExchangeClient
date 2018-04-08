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



type TradeUpdate = {
    Symbol: string
    TradeId: string
    Timestamp: string
    Price: Rational
    Size: Rational
    MakerSide: string
}


type TickerUpdate = {
    Symbol: string
    Timestamp: string
    HighestBid: Rational
    LowestAsk: Rational
    Volume24H: Rational
    High24H: Rational
    Low24H: Rational
    Open24H: Rational
    LastTradePrice: Rational
}

type CandleUpdate = {
    Symbol: string
    Timestamp: string
    Volume: Rational
    High: Rational
    Low: Rational
    Open: Rational
    Close: Rational
}

type DidReceiveTickerHook = Option<(TickerUpdate -> unit)>
type DidReceiveTradeHook = Option<(TradeUpdate[] -> unit)>
type DidReceiveOrderBookHook = Option<((OrderBookUpdate * Market) -> unit)>
type DidReceiveCandleHook = Option<(CandleUpdate[] -> unit)>
