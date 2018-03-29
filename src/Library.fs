namespace CryptoApi

open CryptoApi.BaseExchange.Client

module Say =
    let hello name =
        printfn "Hello %s" name
