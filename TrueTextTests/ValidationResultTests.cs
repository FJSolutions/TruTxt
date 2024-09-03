namespace TrueTextTests;

using TrueText;
using static TrueText.ValidationResult<string>;

public class ValidationResultTests
{
    [Fact]
    public void ValidFactoryMethodTests()
    {
        var result = Valid("FBJ");

        Assert.NotNull(result);
        Assert.NotNull(result.Text);
        Assert.NotEmpty(result.Text);
        Assert.Equal("FBJ", result.Text);
        Assert.True(result.IsValid);
        Assert.IsType<Valid<string>>(result);
        Assert.Equal("FBJ", result.AsValid().Value);
    }

    [Fact]
    public void InvalidFactoryMethodTests()
    {
        var result = Invalid("FBJ", "Invalid");

        Assert.NotNull(result);
        Assert.NotNull(result.Text);
        Assert.NotEmpty(result.Text);
        Assert.Equal("FBJ", result.Text);
        Assert.False(result.IsValid);
        Assert.IsType<Invalid<string>>(result);
        Assert.Equal(1, result.AsInvalid().Errors.Length);
        Assert.Equal("Invalid", result.AsInvalid().Errors[0]);
    }

    [Fact]
    public void CombineValidAndValidResultTest()
    {
        var result = Valid("Nadine")
                     + Valid("FBJ");

        Assert.NotNull(result);
        Assert.NotNull(result.Text);
        Assert.NotEmpty(result.Text);
        Assert.Equal("FBJ", result.Text);
        Assert.True(result.IsValid);
        Assert.IsType<Valid<string>>(result);
        Assert.Equal("FBJ", result.AsValid().Value);
    }

    [Fact]
    public void CombineValidAndInvalidResultTest()
    {
        var result = Valid("Nadine")
                     + Invalid("FBJ", "Invalid");

        Assert.Equal("FBJ", result.Text);
        Assert.False(result.IsValid);
        Assert.IsType<Invalid<string>>(result);
        Assert.Equal("FBJ", result.AsInvalid().Text);
        Assert.Equal("Invalid", result.AsInvalid().Errors[0]);
    }

    [Fact]
    public void CombineInvalidAndValidResultTest()
    {
        var result = Invalid("FBJ", "Invalid")
                     + Valid("Nadine");

        Assert.Equal("FBJ", result.Text);
        Assert.False(result.IsValid);
        Assert.IsType<Invalid<string>>(result);
        Assert.Equal("FBJ", result.AsInvalid().Text);
        Assert.Equal("Invalid", result.AsInvalid().Errors[0]);
    }

    [Fact]
    public void CombineInvalidAndInvalidResultTest()
    {
        var result = Invalid("FBJ", "Invalid")
                     + Invalid("Nadine", "Invalid2");

        Assert.False(result.IsValid);
        Assert.IsType<Invalid<string>>(result);
        Assert.Equal("Nadine", result.AsInvalid().Text);
        Assert.Equal("Invalid", result.AsInvalid().Errors[0]);
        Assert.Equal("Invalid2", result.AsInvalid().Errors[1]);
    }

    [Fact]
    public void MatchValidResultTest()
    {
        var result = Valid("FBJ").Match(
            valid => valid.Text,
            invalid => ""
        );

        Assert.Equal("FBJ", result);
    }

    [Fact]
    public void MatchInvalidResultTest()
    {
        var result = Invalid("FBJ", "Invalid").Match(
            valid => valid.Text,
            invalid => invalid.Errors[0]
        );

        Assert.Equal("Invalid", result);
    }

    [Fact]
    public void ValidationResultPureTest()
    {
        var result = Pure("123");

        Assert.NotNull(result);
        Assert.IsType<Valid<string>>(result);
        Assert.Equal("123", result.AsValid().Value);
    }

    [Fact]
    public void ValidationResultInt32Test()
    {
        var result = Pure("123")
            .AsInt32();

        Assert.NotNull(result);
        Assert.IsType<Valid<int>>(result);
        Assert.Equal(123, result.AsValid().Value);
    }

    [Fact]
    public void ValidateResultReduceValueTest()
    {
        var result = Pure("123")
            .AsInt32()
            .Reduce(0);

        Assert.Equal(123, result);
    }

    [Fact]
    public void ValidateResultReduceFuncTest()
    {
        var result = Invalid("abc", "Invalid number")
            .AsInt32()
            .Reduce(() => 0);

        Assert.Equal(0, result);
    }

    [Fact]
    public void MapValidResultTest()
    {
        var result = Pure("abc")
            .Map(s => s.ToUpperInvariant())
            .Reduce(string.Empty);

        Assert.Equal("ABC", result);
    }

    [Fact]
    public void MapInvalidResultTest()
    {
        var result = Invalid("abc", "Invalid")
            .Map(s => s.ToUpperInvariant())
            .Reduce(string.Empty);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void FilterValidResultSuccessfully()
    {
        var result = Pure("Four")
            .Filter(s => s.Length > 3, "Valid!")
            .Reduce(string.Empty);

        Assert.Equal("Four", result);
    }

    [Fact]
    public void FilterValidResultUnsuccessfully()
    {
        var result = Pure("Four")
            .Filter(s => s.Length > 13, "Invalid")
            .Reduce(string.Empty);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void FilterInvalidResultUnsuccessfully()
    {
        var result = Invalid("Four", "Invalid")
            .Filter(s => s.Length > 13, "Invalid")
            .Reduce(string.Empty);

        Assert.Equal(string.Empty, result);
    }
}