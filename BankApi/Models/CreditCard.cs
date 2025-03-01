using System.Reflection.Metadata.Ecma335;

using LanguageExt;
using LanguageExt.Common;

namespace BankApi.Models;

public record CreditCard(CardNumber CardNumber, Expiry Expiry, Cvv Cvv)
{
    public static CreditCard Make(CardNumber cardNumber, Expiry expiry, Cvv cvv) => 
        new(cardNumber, expiry, cvv);
};

public record Cvv(int Number);

public record Expiry(int Month, int Year);

public record CardNumber(Seq<int> Number);

public static class CreditCardExtensions
{
    public static Validation<Error, uint> CharToDigit(char ch) =>
        ch is >= '0' and <= '9'
            ? (uint)(ch - '0')
            : Error.New($"Invalid character: {ch}");

    public static Validation<Error, IEnumerable<uint>> ValidateAllDigits(string value)
    {
        return value.ToCharArray()
            .Map(CharToDigit)
            .Sequence();
    }

    public static Validation<Error, uint> ValidateLength(string value, int length) =>
        value.Length == length ?
            value: 
            Error.New($"Length should be {length}");

    public static Validation<Error, uint> ValidateInt(string value) =>
        ValidateAllDigits(value)
            .Map(_ => uint.Parse(value));

    public static Validation<Error, Cvv> ValidateCVV(string value)
    {
        Func<uint, string, Cvv> createCvv = (n, _) => new Cvv(n);
        var result = createCvv
            .Map(ValidateInt(value))
            .Apply(ValidateLength(value, 3));
    }
}