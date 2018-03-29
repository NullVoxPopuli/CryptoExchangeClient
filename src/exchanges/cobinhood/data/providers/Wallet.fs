namespace CryptoApi.Exchanges.Cobinhood.Data.Providers

open FSharp.Data

module Wallet =
    type BalancesResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "balances": [
                {
                    "currency": "BTC",
                    "type": "exchange",
                    "total": "1",
                    "on_order": "0.4",
                    "locked": false,
                    "usd_value": "10000.0",
                    "btc_value": "1.0"
                },
                {
                    "currency": "ETH",
                    "type": "exchange",
                    "total": "0.0855175219863032",
                    "on_order": "0.04",
                    "locked": false,
                    "usd_value": "10000.0",
                    "btc_value": "0.008"
                },
                {
                    "currency": "COB",
                    "type":" exchange",
                    "total": "100",
                    "on_order": "20",
                    "locked": false,
                    "usd_value": "1000.0",
                    "btc_value": "0.1"
                }
            ]
        }
    }""">

    type LedgerHistoriesResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "ledger": [
                {
                    "action": "trade",
                    "type": "exchange",
                    "trade_id": "09619448e48a3bd73d493a4194f9020b",
                    "currency": "BTC",
                    "amount": "+635.77",
                    "balance": "2930.33",
                    "timestamp": 1504685599302
                },
                {
                    "action": "deposit",
                    "type": "exchange",
                    "deposit_id": "09619448e48a3bd73d493a4194f9020b",
                    "currency": "BTC",
                    "amount": "+635.77",
                    "balance": "2930.33",
                    "timestamp": 1504685599302
                },
                {
                    "action": "withdraw",
                    "type": "exchange",
                    "withdrawal_id": "09619448e48a3bd73d493a4194f9020b",
                    "currency": "BTC",
                    "amount": "-121.01",
                    "balance": "2194.87",
                    "timestamp": 1504685599302
                }
            ]
        }
    }""">

    type DepositAddressResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "deposit_addresses": [
                {
                    "currency": "BTC",
                    "address": "0xbcd7defe48a19f758a1c1a9706e808072391bc20",
                    "created_at": 1504459805123,
                    "type": "exchange"
                }
            ]
        }
    }""">

    type WithdrawalAddressResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "withdrawal_addresses": [
                {
                    "id": "09619448e48a3bd73d493a4194f9020b",
                    "currency": "BTC",
                    "name": "Kihon's Bitcoin Wallet Address",
                    "type": "exchange",
                    "address": "0xbcd7defe48a19f758a1c1a9706e808072391bc20",
                    "created_at": 1504459805123
                }
            ]
        }
    }""">

    type WithdrawalResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "withdrawal": {
                "withdrawal_id": "62056df2d4cf8fb9b15c7238b89a1438",
                "user_id": "62056df2d4cf8fb9b15c7238b89a1438",
                "status": "pending",
                "confirmations": 25,
                "required_confirmations": 25,
                "created_at": 1504459805123,
                "sent_at": 1504459805123,
                "completed_at": 1504459914233,
                "updated_at": 1504459914233,
                "to_address": "0xbcd7defe48a19f758a1c1a9706e808072391bc20",
                "txhash": "0xf6ca576fb446893432d55ec53e93b7dcfbbf75b548570b2eb8b1853de7aa7233",
                "currency": "BTC",
                "amount": "0.021",
                "fee": "0.0003"
            }
        }
    }""">

    type AllWithdrawalsResponse = JsonProvider<"""{
       "success": true,
        "result": {
            "withdrawals": [
                {
                    "withdrawal_id": "62056df2d4cf8fb9b15c7238b89a1438",
                    "user_id": "62056df2d4cf8fb9b15c7238b89a1438",
                    "status": "pending",
                    "confirmations": 25,
                    "required_confirmations": 25,
                    "created_at": 1504459805123,
                    "sent_at": 1504459805123,
                    "completed_at": 1504459914233,
                    "updated_at": 1504459914233,
                    "to_address": "0xbcd7defe48a19f758a1c1a9706e808072391bc20",
                    "txhash": "0xf6ca576fb446893432d55ec53e93b7dcfbbf75b548570b2eb8b1853de7aa7233",
                    "currency": "BTC",
                    "amount": "0.021",
                    "fee": "0.0003"
                }
            ]
        }
    }""">

    type DepositResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "withdrawals": [
                {
                    "withdrawal_id": "62056df2d4cf8fb9b15c7238b89a1438",
                    "user_id": "62056df2d4cf8fb9b15c7238b89a1438",
                    "status": "pending",
                    "confirmations": 25,
                    "required_confirmations": 25,
                    "created_at": 1504459805123,
                    "sent_at": 1504459805123,
                    "completed_at": 1504459914233,
                    "updated_at": 1504459914233,
                    "to_address": "0xbcd7defe48a19f758a1c1a9706e808072391bc20",
                    "txhash": "0xf6ca576fb446893432d55ec53e93b7dcfbbf75b548570b2eb8b1853de7aa7233",
                    "currency": "BTC",
                    "amount": "0.021",
                    "fee": "0.0003"
                }
            ]
        }
    }""">

    type AllDepositsResponse = JsonProvider<"""{
        "success": true,
        "result": {
            "deposits": [
                {
                    "deposit_id": "62056df2d4cf8fb9b15c7238b89a1438",
                    "user_id": "62056df2d4cf8fb9b15c7238b89a1438",
                    "status": "pending",
                    "confirmations": 25,
                    "required_confirmations": 25,
                    "created_at": 1504459805123,
                    "completed_at": 1504459914233,
                    "from_address": "0xbcd7defe48a19f758a1c1a9706e808072391bc20",
                    "txhash": "0xf6ca576fb446893432d55ec53e93b7dcfbbf75b548570b2eb8b1853de7aa7233",
                    "currency": "BTC",
                    "amount": "0.021",
                    "fee": "0.0003"
                }
            ]
        }
    }""">

