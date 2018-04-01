namespace CryptoApi


module AsyncUtils =
    open System.Threading
    open System.Threading.Tasks

    let RunPeriodically (f: unit -> unit, interval: int, token: CancellationToken) =
        let doAndDelay = async {
            f()
            do! Async.Sleep interval
        }

        Async.Start (doAndDelay, token)


