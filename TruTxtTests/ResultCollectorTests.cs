namespace TruTxtTests;

using TruTxt;
using V = TruTxt.Validator;

public class ResultCollectorTests
{
    [Fact]
    public void ResultCollectorCreationTest()
    {
        var result = V.Min(3).Apply("FBJ");
        var results = ResultsCollector.Create("Test", result);

        Assert.NotNull(results);
        Assert.NotEmpty(results);
        Assert.True(result.IsValid);
        Assert.True(results.IsValid);
    }

    [Fact]
    public void ResultCollectorAddWithKeyTest()
    {
        var result = V.Min(3).Apply("FBJ");
        var results = ResultsCollector.Create("Francis", result)
                      + V.Min(2).Apply("NJ").WithKey("Nadine");

        Assert.NotNull(results);
        Assert.NotEmpty(results);
        Assert.Equal(2, results.Count());
        Assert.True(result.IsValid);
        Assert.True(results.IsValid);
    }

    [Fact]
    public void ResultCollectorGetByKeyTest()
    {
        var result = V.Min(3).Apply("FBJ");
        var results = ResultsCollector.Create("Francis", result)
                      + V.Min(2).Apply("NJ").WithKey("Nadine");

        var name = results.Get("Francis").Reduce();
        Assert.Equal("FBJ", name);
        name = results.Get("Nadine").Reduce();
        Assert.Equal("NJ", name);
        name = results.Get("Jarrod").Reduce();
        Assert.Equal("", name);
    }

    [Fact]
    public void ComparisonValidTest()
    {
        var newPassword = PasswordPolicy.Medium().NewPassword();
        var results = ResultsCollector.Create()
                      + V.Password(PasswordPolicy.Medium()).Apply(newPassword).WithKey("NewPassword")
                      + V.Password(PasswordPolicy.Medium()).Apply(newPassword).WithKey("ConfirmPassword");

        Assert.True(results.IsValid);

        results.CompareResults("NewPassword", "ConfirmPassword", "The passwords don't match");

        Assert.True(results.IsValid);
    }

    [Fact]
    public void ComparisonInvalidTest()
    {
        var policy = PasswordPolicy.Medium();
        var results = ResultsCollector.Create()
                      + V.Password(PasswordPolicy.Medium()).Apply(policy.NewPassword()).WithKey("NewPassword")
                      + V.Password(PasswordPolicy.Medium()).Apply(policy.NewPassword()).WithKey("ConfirmPassword");

        Assert.True(results.IsValid);

        results = results.CompareResults("NewPassword", "ConfirmPassword", "The passwords don't match");

        Assert.False(results.IsValid);
    }
}