namespace CryptoApi.Exchanges.Cobinhood.Data.Providers

open FSharp.Data

module Chart =
    type CandlesResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "candles": [
                {
                    "timestamp": 1507366756,
                    "open": "4378.6",
                    "close": "4379.0",
                    "high": "4379.0",
                    "low": "4378.3",
                    "volume": "23.91460172"
                }
            ]
        }
    }""">
