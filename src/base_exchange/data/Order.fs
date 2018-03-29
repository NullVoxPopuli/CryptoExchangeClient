namespace CryptoApi.Data

open Rationals
open System


type public Order = {
    Id: Guid;

    Market: Market;

    State: string;
    Side: string;
    Type: string;
    Price: Rational;
    Size: Rational;
    Filled: Rational;

    Timestamp: int64;
}
