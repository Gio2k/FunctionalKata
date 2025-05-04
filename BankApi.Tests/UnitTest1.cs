using LanguageExt;
using LanguageExt.Common;

// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

namespace BankApi.Tests;

public class ValidationExercises
{
    [Fact]
    public void CreateCardNumber_ValidCardNumber_ReturnsCardNumber()
    {
        var cardNumber = "1234567890123456";
        var result = CreateCardNumber(cardNumber);
        result.Match(
            cn => Assert.Equal(cardNumber, cn.Value),
            _ => Assert.Fail("Expected success but got failure"));
    }

    [Fact]
    public void CreateCardNumber_InvalidCardNumber_ReturnsErrors()
    {
        var cardNumber = "123456E";
        var result = CreateCardNumber(cardNumber);
        result.Match(
            _ => Assert.Fail("Expected failure but got success"),
            errors =>
            {
                Assert.NotEmpty(errors);
                Assert.Contains(errors, e => e.Message == "Must be numeric");
                Assert.Contains(errors, e => e.Message == "Length must be 16");
            }
        );
    }
    
    [Fact]
    public void CreateCvv_ValidCvv_ReturnsCvv()
    {
        var cvv = "123";
        var result = CreateCvv(cvv);
        result.Match(
            c => Assert.Equal(cvv, c.Value),
            _ => Assert.Fail("Expected success but got failure"));
    }
    
    [Fact]
    public void CreateCvv_InvalidCvv_ReturnsErrors()
    {
        var cvv = "1A";
        var result = CreateCvv(cvv);
        result.Match(
            _ => Assert.Fail("Expected failure but got success"),
            errors =>
            {
                Assert.NotEmpty(errors);
                Assert.Contains(errors, e => e.Message == "Must be numeric");
                Assert.Contains(errors, e => e.Message == "Length must be 3");
            }
        );
    }

    private static Validation<Error, CardNumber> CreateCardNumber(string cardNumber)
    {
        var validateLength = ValidateLength(cardNumber,16);
        var vslidateNumeric = ValidateNumeric(cardNumber);
        return (validateLength, vslidateNumeric).Apply((n, _) => new CardNumber(n));
    }

    private static Validation<Error, string> ValidateNumeric(string value)
    {
        if (!value.All(char.IsDigit))
            return Error.New("Must be numeric");
        return value;
    }

    private static Validation<Error, string> ValidateLength(string text, int length)
    {
        if (text.Length != length)
            return Error.New($"Length must be {length}");
        return text;
    }

    private Validation<Error, Cvv> CreateCvv(string cvv)
    {
        var validateLength = ValidateLength(cvv, 3);
        var validateNumeric = ValidateNumeric(cvv);
        return (validateLength, validateNumeric).Apply((_, _) => new Cvv(cvv));
    }
}

public record CardNumber(string Value);

public record Cvv(string Value);
