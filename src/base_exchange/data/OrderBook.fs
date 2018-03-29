namespace CryptoApi.Data

type public OrderBookEntry = {
    price: string;
    size: string;
}

type public OrderBook = {
    bids: OrderBookEntry[];
    asks: OrderBookEntry[];
}
