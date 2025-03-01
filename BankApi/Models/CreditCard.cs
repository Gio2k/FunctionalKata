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
    static Validation<Error, int> CharToDigit(char ch) =>
        ch is >= '0' and <= '9'
            ? (ch - '0')
            : Error.New("Invalid character");
    
    
}