# Crypto Exchange Client

This Package's goal is to provide a common interface to exchanges' REST and Websocket apis.

Currently only supports Windows.
 - Waiting on `FSharp.Data` and `FSharp.Json` to support dotnetcore before this library can be cross platform

## Demo

1. Clone this repo
2. Open the `CryptoExchangeClient.sln` file
3. Run the `CryptoExchangeClient.examples` project (default)


### Current Supported Exchanges

 - [Cobinhood](https://cobinhood.com) - [Docs](https://cobinhood.github.io/api-public)
   - Full Rest API


### Coming Soon:

 - GDAX
 - Binance


## Development

### Recommended Visual Studio Extensions

[Paket](https://marketplace.visualstudio.com/items?itemName=SteffenForkmann.PaketforVisualStudio)

### Docs
#### Packages Used

[Rationals](https://github.com/tompazourek/Rationals)


### Getting Started

Once the Visual Studio Extensions are installed, open the solution
- Tools -> Paket -> Restore All Packages