namespace TruTxtTests;

using TruTxt;
using static TruTxt.ValidationResult;
using V = TruTxt.Validator;

public class ValidatorTests
{
    [Fact]
    public void ManualValidatorCreationTest()
    {
        const string message = "Shorter than 4";
        var validator = new Validator(s => s.Length > 4 ? Valid(s) : Invalid(s, message));
        var result = validator.Apply("Fourteen");

        Assert.True(result.IsValid);
        Assert.Equal("Fourteen", result.AsValid().Value);

        result = validator.Apply("Two");
        Assert.False(result.IsValid);
        Assert.Equal(message, result.AsInvalid().Errors[0]);
    }

    // AndThen
    [Fact]
    public void ValidatorAndThenSuccess()
    {
        var validator = V.Min(3) && V.Max(7);
        var result = validator.Apply("Five");

        Assert.IsType<Valid>(result);
        Assert.True(result.IsValid);
    }

    [Fact]
    public void ValidatorAndThenFirstFailure()
    {
        var validator = V.Min(3) && V.Max(7);
        var result = validator.Apply("22");

        Assert.IsType<Invalid>(result);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void ValidatorAndThenSecondFailure()
    {
        var validator = V.Min(3) && V.Max(7);
        var result = validator.Apply("Seventeen");

        Assert.IsType<Invalid>(result);
        Assert.False(result.IsValid);
    }

    // OrElse

}