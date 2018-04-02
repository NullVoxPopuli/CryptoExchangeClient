namespace CryptoApi.Data


// BASE-QUOTE
type public Market = {
    Symbol: string;
    QuoteCurrency: string;
    BaseCurrency: string;

    Book: OrderBook;
}
