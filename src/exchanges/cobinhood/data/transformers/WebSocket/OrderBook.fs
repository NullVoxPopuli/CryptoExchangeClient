namespace CryptoApi.Exchanges.Cobinhood.Data.Transformers.WebSocket


module OrderBook =
    open Rationals
    open CryptoApi.Data
    open CryptoApi.Exchanges.Cobinhood.Data.Providers
    open System.Collections.Generic

    let IsSnapshot (payload: WebSocketV2.OrderBook.Root): bool =
        payload.H.[2].Equals("u")

    let ExtractOrderBookMessage (payload: WebSocketV2.OrderBook.Root): OrderBookUpdate =
        let payloadAsks = payload.D.Asks
        let payloadBids = payload.D.Bids

        let bids = new List<OrderBookEntry>()
        let asks = new List<OrderBookEntry>()

        for payloadAsk in payloadAsks do
            asks.Add {
                Price = payloadAsk.[0]
                AmountAtPrice = Rational.ParseDecimal(payloadAsk.[2])
            }

        for payloadBid in payloadBids do
            bids.Add {
                Price = payloadBid.[0]
                AmountAtPrice = Rational.ParseDecimal(payloadBid.[2])
            }


        {
            IsSnapshot = IsSnapshot(payload)
            Bids = bids.ToArray()
            Asks = asks.ToArray()
        }
