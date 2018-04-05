namespace CryptoApi


module AsyncUtils =
    open System.Threading
    open System.Threading.Tasks

    let PeriodicRunner (f: unit -> Async<unit>, interval: int) = async {
      f() |> ignore

      do! Async.Sleep interval
    }

    let rec RunPeriodically (f: unit -> Async<unit>, interval: int, token: CancellationToken) =
        System.DateTime.UtcNow.ToShortTimeString() |> printfn "Starting Periodic calling... %A"

        let doAndDelay: Async<unit> = async {
            do! PeriodicRunner(f, interval)

            RunPeriodically(f, interval, token)
        }

        Async.Start (doAndDelay, token)


