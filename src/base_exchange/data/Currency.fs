namespace CryptoApi.Data

open Rationals


type public Currency = {
    Symbol: string;
    Name: string;
    MinUnit: Rational;
    DepositFee: Rational;
    WithdrawalFee: Rational;
    Type: string;
    IsActive: bool;
    FundingFrozen: bool;
}
