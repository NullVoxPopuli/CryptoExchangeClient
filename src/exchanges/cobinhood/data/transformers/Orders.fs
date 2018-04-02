namespace CryptoApi.Exchanges.Cobinhood.Data.Transformers

open CryptoApi.Data

open CryptoApi.Exchanges.Cobinhood.Data.Providers.Trading

module Orders =
    open Rationals

    type TypeOfOrderResponse =
        | OrdersOrder of GetAllOrdersResponse.Order
        | OrderOrder of GetOrderResponse.Order

    let ExtractOrderFromPayload (knownMarkets: Market[]) (payload: TypeOfOrderResponse): Order =

        let symbolFromOrder = match payload with
                              | OrdersOrder o -> o.TradingPairId
                              | OrderOrder o -> o.TradingPairId

        let marketEqualsSymbol = fun km -> km.Symbol.Equals(symbolFromOrder)
        let market = knownMarkets |> Array.find marketEqualsSymbol

        let order = {
            Market = market;

            Id = match payload with
                     | OrdersOrder o -> o.Id
                     | OrderOrder o -> o.Id;
            State = match payload with
                     | OrdersOrder o -> o.State
                     | OrderOrder o -> o.State;
            Side = match payload with
                     | OrdersOrder o -> o.Side
                     | OrderOrder o -> o.Side;
            Type = match payload with
                     | OrdersOrder o -> o.Type
                     | OrderOrder o -> o.Type;
            Price = match payload with
                     | OrdersOrder o -> Rational.Approximate(o.Price)
                     | OrderOrder o -> Rational.Approximate(o.Price);
            Size = match payload with
                     | OrdersOrder o -> Rational.Approximate(o.Size)
                     | OrderOrder o -> Rational.Approximate(o.Size);
            Filled = match payload with
                     | OrdersOrder o -> Rational.Approximate(o.Filled)
                     | OrderOrder o -> Rational.Approximate(o.Filled);
            Timestamp = match payload with
                         | OrdersOrder o -> o.Timestamp
                         | OrderOrder o -> o.Timestamp;
        }

        order

    let ExtractOrder (knownMarkets: Market[]) (payload: GetOrderResponse.Root): Order =
        (OrderOrder payload.Result.Order)
        |> ExtractOrderFromPayload knownMarkets

    let ExtractOrders (knownMarkets: Market[]) (payload: GetAllOrdersResponse.Root): Order[] =
        payload.Result.Orders
        |> Array.map (fun o -> ExtractOrderFromPayload knownMarkets (OrdersOrder o))
