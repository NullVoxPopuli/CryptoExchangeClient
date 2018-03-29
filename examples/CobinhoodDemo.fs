namespace Examples

//#if INTERACTIVE
//#load "./InteractiveUtils.fsx"
//#endif
open CryptoApi.Exchanges
open Examples.InteractiveUtils

module CobinhoodDemo =

    let client = Cobinhood.RestClient()

    let SystemTime () = 
        WriteHeader "GET system/time"
        let time = client.GetSystemTime().Result.Time
        printfn "Time: %i" time

    let SystemInfo () =
        WriteHeader "GET system/info"
        let info = client.GetSystemInfo().Result.Info
        printfn "Info: %A" info


    let optionMap = [
        ("1", "GET system/time", SystemTime);
        ("2", "GET system/info", SystemInfo);
    ]

    let BeginDemo =
        PromptMenu ("The Cobinhood API -- examples", optionMap)

//#if INTERACTIVE
//#else
//CobinhoodDemo.BeginDemo
//#endif


