using BankApi.Models;

using FluentAssertions;

namespace BankApi.Tests;

public class CreditCardShould
{
  public static IEnumerable<object[]> GetCardNumbers() => 
      Enumerable.Range(0,10).Select(n => (object[])[n]);

  [Theory]
  [MemberData(nameof(GetCardNumbers))]
  public void ReturnValidCardNumber(uint expected)
  {
    var ch = char.Parse($"{expected}");
    var result = CreditCardExtensions.CharToDigit(ch);

    result.Match(
        r => r.Should().Be(expected),
        _ => throw new Exception("Should not happen"));
  }

    [Theory]
    [InlineData('a')]
    [InlineData('Z')]
    public void ReturnInvalidCardNumber(char ch)
    {
        var result = CreditCardExtensions.CharToDigit(ch);
        result.Match(
            _ => throw new Exception("Should not happen"),
            errs => errs.Should().Contain(e => e.Message.Equals($"Invalid character: {ch}")));
    }

    [Theory]
    [InlineData("1234567890")]
    [InlineData("0000000000")]
    [InlineData("9999999999")]
    public void ReturnValidCardNumbers(string value)
    {
        var result = CreditCardExtensions.ValidateAllDigits(value);
        result.Match(
            r => r.Should().NotBeEmpty(),
            _ => throw new Exception("Should not happen"));
    }

    [Fact]
    public void ReturnInvalidCardNumbers()
    {
        var result = CreditCardExtensions.ValidateAllDigits("xy123");
        result.Match(
            _ => throw new Exception("Should not happen"),
            errs => errs.Should().Contain(e => e.Message.StartsWith("Invalid character")));
    }
}
