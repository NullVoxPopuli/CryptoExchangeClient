namespace CryptoApi.Exchanges.Cobinhood.Data.Providers

open FSharp.Data

module System =
    type SystemTimeResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "time": 1505204498376
        }
    }""">

    type SystemInfoResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "info": {
                "phase": "production",
                "revision": "480bbd"
            }
        }
    }""">
