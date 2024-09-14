namespace TrueTextTests;

using TrueText;
using V = TrueText.Validator;

public class BuiltInValidatorTests
{
    [Fact]
    public void MinValidatorValidTest()
    {
        var result = V.Min(3)
            .Apply("Francis");

        Assert.True(result.IsValid);
    }

    [Fact]
    public void MinValidatorInvalidTest()
    {
        var result = V.Min(3)
            .Apply("Ox");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void MaxValidatorValidTest()
    {
        var result = V.Max(3)
            .Apply("Ox");

        Assert.True(result.IsValid);
    }

    [Fact]
    public void MaxValidatorInvalidTest()
    {
        var result = V.Max(3)
            .Apply("Francis");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void BetweenValidatorValidTest()
    {
        var result = V.Between(3, 7)
            .Apply("Sarah");

        Assert.True(result.IsValid);
    }

    [Fact]
    public void BetweenValidatorInvalidAboveTest()
    {
        var result = V.Between(3, 7)
            .Apply("Francis");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void BetweenValidatorInvalidBelowTest()
    {
        var result = V.Between(3, 7)
            .Apply("Ox");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void OptionalValidatorEmptyValidTest()
    {
        var result = V.Optional(V.Min(3))
            .Apply("\t");

        Assert.True(result.IsValid);
    }

    [Fact]
    public void OptionalValidatorWithContentValidTest()
    {
        var result = V.Optional(V.Min(3))
            .Apply("Francis");

        Assert.True(result.IsValid);
    }

    [Fact]
    public void OptionalValidatorWithContentInvalidTest()
    {
        var result = V.Optional(V.Min(3))
            .Apply("Ox");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void RequiredValidatorEmptyValidTest()
    {
        var result = V.Required()
            .Apply("F");

        Assert.True(result.IsValid);
    }

    [Fact]
    public void RequiredValidatorWithAdditionalValidatorsValidTest()
    {
        var result = V.Required(V.Min(3))
            .Apply("Francis");

        Assert.True(result.IsValid);
    }

    [Fact]
    public void RequiredValidatorEmptyInvalidTest()
    {
        var result = V.Required()
            .Apply("\t");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void ValidatorIntegerValidTest()
    {
        var result = V.IsInteger()
            .Apply("10_000");

        Assert.True(result.IsValid);
        Assert.Equal("10000", result.Text);
    }

    [Fact]
    public void ValidatorSignedIntegerValidTest()
    {
        var result = V.IsInteger(true)
            .Apply("-10_000");

        Assert.True(result.IsValid);
        Assert.Equal("-10000", result.Text);
    }

    [Fact]
    public void ValidatorNonSignedIntegerInvalidTest()
    {
        var result = V.IsInteger(false)
            .Apply("-10_000");

        Assert.False(result.IsValid);
        Assert.Equal("-10_000", result.Text);
    }

    [Fact]
    public void ValidatorIntegerInvalidTest()
    {
        var result = V.IsInteger()
            .Apply("R 10,000");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void ValidatorDecimalValidTest()
    {
        var result = V.IsDecimal()
            .Apply("123.456");

        Assert.True(result.IsValid);
        Assert.Equal("123.456", result.Text);
    }

    [Fact]
    public void ValidatorNegativeDecimalValidTest()
    {
        var result = V.IsDecimal(true)
            .Apply("-123.456");

        Assert.True(result.IsValid);
        Assert.Equal("-123.456", result.Text);
    }

    [Fact]
    public void ValidatorNegativeDecimalInvalidTest()
    {
        var result = V.IsDecimal(false)
            .Apply("-123.456");

        Assert.False(result.IsValid);
        Assert.Equal("-123.456", result.Text);
    }

    [Fact]
    public void ValidatorDecimalWithDashInvalidTest()
    {
        var result = V.IsDecimal(false)
            .Apply("123.-456");

        Assert.False(result.IsValid);
        Assert.Equal("123.-456", result.Text);
    }

    [Fact]
    public void ValidatorDecimalInvalidExtraDotTest()
    {
        var result = V.IsDecimal()
            .Apply("123.45.6");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void DecimalValidatorInvalidTest()
    {
        var result = V.IsDecimal()
            .Apply("R 123.45");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void RegexValidatorValidTest()
    {
        var result = V.Regex(@"^\d\d\d$")
            .Apply("123");

        Assert.True(result.IsValid);
        Assert.Equal("123", result.Text);
    }

    [Fact]
    public void RegexValidatorInvalidTest()
    {
        var result = V.Regex(@"^\d\d\d$")
            .Apply("R 123");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void EmailValidatorValidTest()
    {
        var result = V.IsEmail()
            .Apply("FBJ@Example.com");

        Assert.True(result.IsValid);
        Assert.Equal("fbj@example.com", result.Text);
    }

    [Fact]
    public void EmailValidatorInvalidTest()
    {
        var result = V.IsEmail()
            .Apply("FBJ@Example");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void PasswordValidatorValidTest()
    {
        var result = V.Password(PasswordPolicy.Weak())
            .Apply("abC4@");

        Assert.True(result.IsValid);
    }

    [Fact]
    public void PasswordValidatorInvalidTest()
    {
        var result = V.Password(PasswordPolicy.Weak())
            .Apply("abc");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void DateTimeBritishFormatValidTest()
    {
        var result = V.IsDateTime("dd/MM/yy").Apply("25/10/65");

        Assert.True(result.IsValid);
        Assert.Equal("25/10/65", result.Text);
        Assert.Equal("1965-10-25T00:00:00", result.AsValid().Value);
    }

    [Fact]
    public void DateTimeUsFormatValidTest()
    {
        var result = V.IsDateTime("MM/dd/yyyy").Apply("10/25/1965");

        Assert.True(result.IsValid);
        Assert.Equal("10/25/1965", result.Text);
        Assert.Equal("1965-10-25T00:00:00", result.AsValid().Value);
    }

    [Fact]
    public void DateTimeFormatInvalidTest()
    {
        var result = V.IsDateTime("dd/MM/yy").Apply("25th Oct 1965");

        Assert.False(result.IsValid);
    }

    [Fact]
    public void DateUsFormatValidTest()
    {
        var result = V.IsDate("MM/dd/yyyy").Apply("10/25/1965");

        Assert.True(result.IsValid);
        Assert.Equal("10/25/1965", result.Text);
        Assert.Equal("1965-10-25", result.AsValid().Value);
    }

    [Fact]
    public void DateBritishFormatValidTest()
    {
        var result = V.IsDate("d/M/yyyy").Apply("25/10/1965");

        Assert.True(result.IsValid);
        Assert.Equal("25/10/1965", result.Text);
        Assert.Equal("1965-10-25", result.AsValid().Value);
    }

    [Fact]
    public void TwelveHourTimeFormatValidTest()
    {
        var result = V.IsTime("hh:mm tt").Apply("12:15 pm");

        Assert.True(result.IsValid);
        Assert.Equal("12:15 pm", result.Text);
        Assert.Equal("12:15:00", result.AsValid().Value);
    }

    [Fact]
    public void TwentyFourHourTimeFormatValidTest()
    {
        var result = V.IsTime("HH:mm:ss").Apply("12:15:54");

        Assert.True(result.IsValid);
        Assert.Equal("12:15:54", result.Text);
        Assert.Equal("12:15:54", result.AsValid().Value);
    }

    [Fact]
    public void GuidValidTest()
    {
        var result = V.IsGuid().Apply("{B70183FC-704E-4027-BC8A-68E7016B9942}");

        Assert.True(result.IsValid);
        Assert.Equal("{B70183FC-704E-4027-BC8A-68E7016B9942}", result.Text);
        Assert.Equal("{B70183FC-704E-4027-BC8A-68E7016B9942}", result.AsValid().Value);
    }

    [Fact]
    public void GuidInvalidTest()
    {
        var result = V.IsGuid().Apply("φ70183FC-704E-4027-BC8A-68E7016B9942");

        Assert.False(result.IsValid);
        Assert.Equal("φ70183FC-704E-4027-BC8A-68E7016B9942", result.Text);
    }

    [Fact]
    public void BoolValidTest()
    {
        var result = V.IsBoolean().Apply("Yes");

        Assert.True(result.IsValid);
        Assert.Equal("Yes", result.Text);
    }

    [Fact]
    public void BoolInvalidTest()
    {
        var result = V.IsBoolean().Apply("Nope");

        Assert.False(result.IsValid);
        Assert.Equal("Nope", result.Text);
    }
}