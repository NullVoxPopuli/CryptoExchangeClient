# Crypto Exchange Client


[![NuGet](https://img.shields.io/nuget/v/CryptoExchangeClient.svg)](https://www.nuget.org/packages/CryptoExchangeClient/)

This Package's goal is to provide a common interface to exchanges' REST and Websocket apis.

Currently only supports Windows.
 - Waiting on `FSharp.Data` and `FSharp.Json` to support dotnetcore before this library can be cross platform

## Install

<table>
 <tr>
  <th><a href="https://github.com/dotnet/cli">dotnet cli</a>
  </th><td>
  
```bash
dotnet add package CryptoExchangeClient
```
  </td>
 </tr>
 <tr>
 <th><a href="https://fsprojects.github.io/Paket/">paket</a></th><td>
 
```bash
paket add CryptoExchangeClient
```
  </td>
 </tr>
</table>

## Demo

1. Clone this repo
2. Open the `CryptoExchangeClient.sln` file
3. Run the `Examples` project (default)

![The Cobinhood Demo App](https://github.com/NullVoxPopuli/CryptoExchangeClient/blob/master/docs/images/CobinhoodDemo.png?raw=true)


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
