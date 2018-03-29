open System

// #r "../../../../packages/FSharp.Core/lib/net45/FSharp.Core.dll"
#r "../../../../packages/RestSharp/lib/netstandard2.0/RestSharp.dll"
#r "../../../../packages/FSharp.Data/lib/net45/FSharp.Data.dll"


#r "../../src/bin/Debug/netcoreapp2.0/crypto-api.dll"
open CryptoApi.Exchanges


let client = Cobinhood.RestClient()
let time = client.GetSystemTime().Result.Time

printfn "Time: %i" time

let info = client.GetSystemInfo().Result.Info

printfn "Info: %A" info
