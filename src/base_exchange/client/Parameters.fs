namespace CryptoApi.BaseExchange.Client

open System.Collections.Generic

module Parameters =
    [<AbstractClass>]
    type public JsonableParameters() =
        abstract member ToString: string
        default __.ToString = """{}"""
