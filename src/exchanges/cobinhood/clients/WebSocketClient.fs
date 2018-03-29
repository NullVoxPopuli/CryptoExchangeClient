namespace CryptoApi.Exchanges.Cobinhood

open CryptoApi.BaseExchange.Client

type WebSocketClient() =
    inherit AbstractWebSocketClient("wss://feed.cobinhood.com/ws")
