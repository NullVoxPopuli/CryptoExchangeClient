namespace CryptoApi.Exchanges.Cobinhood.Data.Transformers.WebSocket


module OrderBook =
    open CryptoApi.Data
    open CryptoApi.Exchanges.Cobinhood.Data.Providers

    let IsSnapshot (payload: WebSocketV2.OrderBook.Root): bool =
        payload.H.Strings.[2].Equals("u")

    let ExtractOrderBookMessage (payload: WebSocketV2.OrderBook.Root): OrderBookUpdate =
        let payloadAsks = payload.D.Asks
        let payloadBids = payload.D.Bids

        let bids = Array.empty<OrderBookEntry>
        let asks = Array.empty<OrderBookEntry>

        {
            IsSnapshot = IsSnapshot(payload)
            Bids = bids
            Asks = asks
        }
