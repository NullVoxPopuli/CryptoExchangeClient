namespace CryptoApi.BaseExchange.Client


module Parameters =
    [<AbstractClass>]
    type public JsonableParameters() =
        abstract member ToString: string
        default __.ToString = """{}"""

