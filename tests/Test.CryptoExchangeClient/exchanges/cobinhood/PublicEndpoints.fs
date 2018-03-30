module Tests

open Expecto

open CryptoApi.Exchanges.Cobinhood

[<Tests>]
let tests =
  testList "test public endpoints" [
    test "system time" {
        let response = RestClient().GetSystemTime()
        
        response.Result.Time.ToString()
        |> Expect.isNonEmpty "System Time returned"
     }
  ]
  