namespace CryptoApi


module AsyncUtils =
    open System.Threading
    open System.Threading.Tasks

    let PeriodicRunner (f: unit -> 'a, interval: int) = async {
      f() |> ignore

      do! Async.Sleep interval
    }

    let rec RunPeriodically (f: unit -> 'a, interval: int, token: CancellationToken) =
        let doAndDelay: Async<unit> = async {
            do! PeriodicRunner(f, interval)

            RunPeriodically(f, interval, token)
        }

        Async.Start (doAndDelay, token)


