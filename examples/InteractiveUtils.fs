namespace Examples

//#if INTERACTIVE
//#r "../packages/RestSharp/lib/netstandard2.0/RestSharp.dll"
//#r "../packages/FSharp.Data/lib/net45/FSharp.Data.dll"
//#r "../src/bin/Debug/netcoreapp2.0/crypto-api.dll"
//#endif


open System
open System.Drawing
open Colorful


module InteractiveUtils =
    let titleColor = Color.BlueViolet
    let optionsColor = Color.DarkSlateGray

    let WriteHeader (text: string) =
        Console.WriteLine ("=============================", titleColor)
        Console.WriteLine (text, titleColor)
        Console.WriteLine ("=============================", titleColor)
        Console.WriteLine ""

    //let PromptToContinue =
    //    Console.Write("Continue? (Y|N)");
    //    let key = Console.ReadLine();
        
    //    match key with
    //    | "n" -> Environment.Exit(0)
    //    | "N" -> Environment.Exit(0)
    //    | _ -> ()

    let NumberEqualsMapEntry (num: string)  (entry: string * string * (unit -> unit)): bool =
        let (number, _title, _fun) = entry

        num.Equals(number)

    let PrintEntry (entry: string * string * (unit -> unit)): unit =
        let (num, title, _fun) = entry

        Console.WriteLine(num + " )    " + title, optionsColor)

    let FindSelected (selected: string, optionMap: (string * string * (unit -> unit)) list) =
        let finder = NumberEqualsMapEntry(selected)

        optionMap
        |> List.find finder

    let rec PromptMenu (title: string, optionMap: (string * string * (unit -> unit)) list) =
        Console.WriteLine ""
        WriteHeader title
        Console.WriteLine ("Choose a number to perform the example", Color.Gray)
        Console.WriteLine ("NOTE: any request requiring auth will require the api key be passed to the RestClient", Color.Gray)
        Console.WriteLine ""
        Console.WriteLine ("type 'exit' to exit", Color.Yellow)
        Console.WriteLine ""


        optionMap |> List.iter PrintEntry
        Console.WriteLine ""

        let key = Console.ReadLine()        

        match key with
        | "exit" -> Environment.Exit(0)
        | _ -> do
            let selected = FindSelected(key, optionMap)

            match selected with
            | (s: string * string * (unit -> unit)) -> do
                let (_num, demoTitle, demo) = s

                WriteHeader demoTitle 
                demo ()

                PromptMenu(title, optionMap)
            | _ -> do
                Console.WriteLine("Invalid Selection")
                PromptMenu(title, optionMap)
                
        
