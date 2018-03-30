namespace CryptoApi.Exchanges.Cobinhood.Data.Providers

open FSharp.Data

module Trading =
    type GetOrderResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "order": {
                "id": "37f550a202aa6a3fe120f420637c894c",
                "trading_pair_id": "BTC-USDT",
                "state": "open",
                "side": "bid",
                "type": "limit",
                "price": "5000.01",
                "size": "1.0100",
                "filled": "0.59",
                "timestamp": 1504459805123,
                "eq_price": "5000.01"
            }
        }
    }""">

    type GetOrderTradesResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "trades": [
                {
                    "id": "09619448e48a3bd73d493a4194f9020b",
                    "price": "10.00000000",
                    "size": "0.01000000",
                    "maker_side": "bid",
                    "timestamp": 1504459805123
                }
            ]
        }
    }""">

    type GetAllOrdersResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "orders": [
                {
                    "id": "37f550a202aa6a3fe120f420637c894c",
                    "trading_pair_id": "BTC-USDT",
                    "state": "open",
                    "side": "bid",
                    "type": "limit",
                    "price": "5000.01",
                    "size": "1.0100",
                    "filled": "0.59",
                    "timestamp": 1504459805123,
                    "eq_price": "5000.01"
                }
            ]
        }
    }""">

    type PlaceOrderResponse = GetOrderResponse
    //JsonProvider<"""{
    //    "success": true,
    //    "result": {
    //        "order": {
    //            "id": "37f550a202aa6a3fe120f420637c894c",
    //            "trading_pair": "BTC-USDT",
    //            "state": "open",
    //            "side": "bid",
    //            "type": "limit",
    //            "price": "5000.01",
    //            "size": "1.0100",
    //            "filled": "0.59",
    //            "timestamp": 1504459805123,
    //            "eq_price": "5000.01"
    //        }
    //    }
    //}""">

    type ModifyOrderResponse = JsonProvider<"""{
        "success": true
    }""">

    type CancelOrderResponse = JsonProvider<"""{
        "success": true
    }""">

    type OrderHistoryResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "order_history": [
                {
                    "id": "37f550a202aa6a3fe120f420637c894c",
                    "trading_pair_id": "BTC-USDT",
                    "state": "filled",
                    "side": "bid",
                    "type": "limit",
                    "price": "5000.01",
                    "size": "1.0100",
                    "filled": "0.59",
                    "timestamp": 1504459805123,
                    "eq_price": "5000.01"
                }
            ]
        }
    }""">

    type TradeResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "trade": {
                "trading_pair_id": "BTC-USDT",
                "id": "09619448-e48a-3bd7-3d49-3a4194f9020b",
                "maker_side": "bid",
                "price": "10.00000000",
                "size": "0.01000000",
                "timestamp": 1504459805123
            }
        }
    }""">

    type TradeHistoryResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "trades": [
                {
                    "trading_pair_id": "BTC-USDT",
                    "id": "09619448e48a3bd73d493a4194f9020b",
                    "maker_side": "ask",
                    "price": "10.00000000",
                    "size": "0.01000000",
                    "timestamp": 1504459805123
                }
            ]
        }
    }""">