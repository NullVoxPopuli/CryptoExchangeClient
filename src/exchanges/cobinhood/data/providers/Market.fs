namespace CryptoApi.Exchanges.Cobinhood.Data.Providers

open FSharp.Data

module Market =
    type CurrenciesResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "currencies": [
                {
                    "currency": "BTC",
                    "name": "Bitcoin",
                    "min_unit": "0.00000001",
                    "deposit_fee": "0",
                    "withdrawal_fee": "22.6",
                    "type": "native",
                    "is_active": true,
                    "funding_frozen": false
                }
            ]
        }
    }""">

    type TradingPairsResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "trading_pairs": [
                {
                    "id": "BTC-USDT",
                    "base_currency_id": "BTC",
                    "quote_currency_id": "USDT",
                    "base_min_size": "0.004",
                    "base_max_size": "10000",
                    "quote_increment": "0.1"
                }
            ]
        }
    }""">

    type OrderBookResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "orderbook": {
                "sequence": 1938572,
                "bids": [
                    [ 0.1136, 2, 3.45 ]
                ],
                "asks": [
                    [ 0.1135, 2, 2.22 ]
                ]
            }
        }
    }""">

    type OrderBookPrecisions = JsonProvider<"""{
        "success": true,
        "result": []
    }""">

    type TradingStatsResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "BTC-USDT": {
                "id": "BTC-USDT",
                "last_price": "10005",
                "lowest_ask": "10005",
                "highest_bid": "15200.1",
                "base_volume": "0.36255776",
                "quote_volume": "4197.431917146",
                "is_frozen": false,
                "high_24hr": "16999.9",
                "low_24hr": "10000",
                "percent_changed_24hr": "-0.3417806461799593"
            }
        }
    }""">


    type TickerResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "ticker": {
                "trading_pair_id": "COB-BTC",
                "timestamp": 1504459805123,
                "24h_high": "23.456",
                "24h_low": "10.123",
                "24h_open": "15.764",
                "24h_volume": "7842.11542563",
                "last_trade_price":"244.82",
                "highest_bid":"244.75",
                "lowest_ask":"244.76"
            }
        }
    }""">

    type RecentTradesResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "trades": [
                {
                    "id": "09619448e48a3bd73d493a4194f9020b",
                    "price": "10.00000000",
                    "size": "0.01000000",
                    "maker_side": "buy",
                    "timestamp": 1504459805123
                }
            ]
        }
    }""">
