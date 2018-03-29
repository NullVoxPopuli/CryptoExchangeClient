namespace CryptoApi.Data


// BASE-QUOTE
type public Market = {
    symbol: string;
    quoteCurrency: string;
    baseCurrency: string;

    book: OrderBook;
}
