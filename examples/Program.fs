namespace Examples

open Examples
open InteractiveUtils

module Program =
     [<EntryPoint>]
     let Main(args) =


         let optionMap = [
            ("1", "Cobinhood", CobinhoodDemo.BeginDemo);

            Spacer;
            ("", "Others Coming Soon", PlaceholderFn);
         ]

         PromptMenu("CryptoExchangeClient", optionMap)
         // main entry point return
         0
